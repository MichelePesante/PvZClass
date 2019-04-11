using UnityEngine;
using System.Collections.Generic;
using System;

namespace StateMachine.Card
{

    public class Card_Drag_State : Card_Base_State
    {
        bool isDragging;
        Vector3 startScale;
        Vector3 startPosition;

        public override void Enter()
        {
            startPosition = context.cardController.transform.position;
            context.cardController.OnCardPointerUp += HandleOnPointerUp;
            startScale = context.cardController.transform.localScale;
            context.cardController.transform.localScale *= 1.5f;
            isDragging = true;
        }

        /// <summary>
        /// Handles card drag release.
        /// </summary>
        /// <param name="_cardCtrl"></param>
        private void HandleOnPointerUp(CardViewController _cardCtrl)
        {
            List<IDetectable> detectedObjects = _cardCtrl.GetDetectedObjects();
            LaneViewController lane = null;

            bool lanePlaced = false;
            for (int i = 0; i < detectedObjects.Count; i++)
            {
                lane = detectedObjects[i] as LaneViewController;
                if (lane)
                {
                    lane.ToggleHighlight(LaneViewController.Highlight.off);
                    if (LaneController.CheckCardPlayability(lane.Data, _cardCtrl.Data))
                    {
                        lane.PlaceCard(context.cardController);
                        lanePlaced = true;
                        context.cardController.CurrentState = CardViewController.State.Played;
                    }
                }               
            }

            if (!lanePlaced)
                ResetPosition();

            isDragging = false;
        }

        private void ResetPosition()
        {
            context.cardController.transform.position = startPosition;
            context.cardController.CurrentState = CardViewController.State.Idle;
        }

        public override void Tick()
        {
            if (isDragging)
            {
                context.cardController.Detect();
                context.cardController.transform.position = Input.mousePosition;
            }
        }

        public override void Exit()
        {
            context.cardController.OnCardPointerUp -= HandleOnPointerUp;
            context.cardController.transform.localScale = startScale;
        }
    }
}