using UnityEngine;
using System.Collections;
using System;

namespace StateMachine.Card {

    public class Card_Playable_State : Card_Base_State {

        public override void Enter()
        {
            context.cardController.OnCardPointerDown += HandleOnpointerDown;
            context.cardController.SetHiglight(CardViewController.HighlightState.Highlighted);
        }

        private void HandleOnpointerDown(CardViewController obj)
        {
            if (obj == context.cardController)
                context.cardController.Data.CurrentState = CardState.Drag;
        }

        public override void Exit()
        {
            context.cardController.OnCardPointerDown -= HandleOnpointerDown;
            context.cardController.SetHiglight(CardViewController.HighlightState.NoHighlight);
        }
    }
}