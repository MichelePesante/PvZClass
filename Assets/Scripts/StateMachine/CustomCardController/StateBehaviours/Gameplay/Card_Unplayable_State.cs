using UnityEngine;
using System.Collections;

namespace StateMachine.Card {

    public class Card_Unplayable_State : Card_Base_State {

        public override void Enter()
        {
            context.cardController.SetHiglight(CardData.Highlight.Lowlight);
        }

        public override void Exit()
        {
            context.cardController.SetHiglight(CardData.Highlight.NoHighlight);
        }
    }
}