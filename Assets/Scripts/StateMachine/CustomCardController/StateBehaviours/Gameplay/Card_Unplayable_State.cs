using UnityEngine;
using System.Collections;

namespace StateMachine.Card {

    public class Card_Unplayable_State : Card_Base_State {

        public override void Enter()
        {
            context.cardViewController.SetHiglight(CardViewController.HighlightState.Lowlight);
        }

        public override void Exit()
        {
            context.cardViewController.SetHiglight(CardViewController.HighlightState.NoHighlight);
        }
    }
}