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
            startPosition = context.cardViewController.transform.position;
            context.cardViewController.OnCardPointerUp += HandleOnPointerUp;
            startScale = context.cardViewController.transform.localScale;
            context.cardViewController.transform.localScale *= 1.5f;
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
                        lane.PlaceCard(context.cardViewController);
                        lanePlaced = true;
                        context.cardViewController.Data.CurrentState = CardState.Played;
                        DeckController.ResetCardsState(context.cardViewController.GetPlayerOwner().HandDeck);
                    }
                }               
            }

            if (!lanePlaced)
                ResetPosition();

            isDragging = false;
        }

        private void ResetPosition()
        {
            context.cardViewController.transform.position = startPosition;
            context.cardViewController.transform.localScale = startScale;
            context.cardViewController.Data.CurrentState = CardState.Idle;
        }

        public override void Tick()
        {
            if (isDragging)
            {
                context.cardViewController.Detect();
                context.cardViewController.transform.position = Input.mousePosition;
            }
        }

        public override void Exit()
        {
            context.cardViewController.OnCardPointerUp -= HandleOnPointerUp;            
        }
    }
}