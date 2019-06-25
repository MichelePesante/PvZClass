using UnityEngine;
using System.Collections;
using System;

namespace StateMachine.Card {

    public class Card_Playable_State : Card_Base_State {

        public override void Enter()
        {
            context.cardViewController.OnCardPointerDown += HandleOnpointerDown;
            context.cardViewController.SetHiglight(CardViewController.HighlightState.Highlighted);
        }

        private void HandleOnpointerDown(CardViewController obj)
        {
            if (obj == context.cardViewController)
                context.cardViewController.Data.CurrentState = CardState.Drag;
        }

        public override void Exit()
        {
            context.cardViewController.OnCardPointerDown -= HandleOnpointerDown;
            context.cardViewController.SetHiglight(CardViewController.HighlightState.NoHighlight);
        }
    }
}