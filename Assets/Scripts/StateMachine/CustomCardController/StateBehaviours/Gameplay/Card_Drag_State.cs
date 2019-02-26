using UnityEngine;
using System.Collections.Generic;
using System;

namespace StateMachine.Card {

    public class Card_Drag_State : Card_Base_State {
        bool isDragging;
        Vector3 startScale;
        Vector3 startPosition;

        public override void Enter() {
            startPosition = context.cardController.transform.position;
            context.cardController.OnCardPointerUp += HandleOnPointerUp;
            startScale = context.cardController.transform.localScale;
            context.cardController.transform.localScale *= 1.5f;
            isDragging = true;
            //context.boardCtrl.ToggleBoardInteractability(true, context.cardController);
        }

        private void HandleOnPointerUp(CardController _cardCtrl) {
            List<IDetectable> detectedObjects = _cardCtrl.GetDetectedObjects();
            LaneUI lane = null;
            for (int i = 0; i < detectedObjects.Count; i++) {
                lane = detectedObjects[i] as LaneUI;
                if (lane) {
                    lane.ToggleHighlight(LaneUI.Highlight.off);
                    if (lane.MyLane.CheckCardPlayability(_cardCtrl)) {
                        context.cardController.CurrentState = CardController.State.Played;
                    } else {
                        context.cardController.transform.position = startPosition;
                    }
                }
            }

            isDragging = false;
        }

        public override void Tick() {
            if (isDragging) {
                context.cardController.Detect();
                context.cardController.transform.position = Input.mousePosition;
            } else
                context.cardController.CurrentState = CardController.State.Idle;
        }

        public override void Exit() {
            context.cardController.OnCardPointerUp -= HandleOnPointerUp;
            context.cardController.transform.localScale = startScale;
            context.boardCtrl.ToggleBoardInteractability(false);
        }
    }
}