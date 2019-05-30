using UnityEngine;
using System.Collections;

namespace StateMachine.Card
{
    public class Card_Inactive_State : Card_Base_State
    {
        public override void Enter()
        {
            base.Enter();
            //context.cardController.HideCard();
        }

        public override void Exit()
        {
            base.Exit();
            //context.cardController.ShowCard();
        }
    }
}