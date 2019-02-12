using UnityEngine;
using System.Collections;

namespace StateMachine.Card
{

    public class Card_Idle_State : Card_Base_State
    {

        public override void Enter()
        {
            context.cardController.SetHiglight(CardData.Highlight.NoHighlight);
            if (context.boardCtrl.CheckCardPlayability(context.cardController.GetCardData().playableLane, Player.Type.one))
                context.cardController.CurrentState = CardController.State.Playable;
            else
                context.cardController.CurrentState = CardController.State.Unplayable;
        }
    }
}