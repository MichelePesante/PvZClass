using UnityEngine;
using System.Collections;
using System;

namespace StateMachine.Card {

    public class Card_Drag_State : Card_Base_State
    {
        bool isDragging;
        Vector3 startScale;

        public override void Enter()
        {
            context.cardController.OnCardPointerUp += HandleOnPointerUp;
            startScale = context.cardController.transform.localScale;
            context.cardController.transform.localScale *= 1.5f;
            isDragging = true;
            //context.boardCtrl.ToggleBoardInteractability(true, context.cardController);
        }

        private void HandleOnPointerUp(CardController obj)
        {
            isDragging = false;
        }

        public override void Tick()
        {
            context.cardController.Detect();
            if (isDragging)
                context.cardController.transform.position = Input.mousePosition;
            else
                context.cardController.CurrentState = CardController.State.Idle;
        }

        public override void Exit()
        {
            context.cardController.OnCardPointerUp -= HandleOnPointerUp;
            context.cardController.transform.localScale = startScale;
            context.boardCtrl.ToggleBoardInteractability(false);
        }
    }
}