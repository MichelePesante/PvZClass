using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Card
{
    public class Card_IdleMulligan_State : Card_Base_State
    {
        public override void Enter()
        {
            context.cardController.OnCardPointerDown += HandleOnpointerDown;
            context.cardController.SetHiglight(CardData.Highlight.NoHighlight);
        }

        private void HandleOnpointerDown(CardController obj)
        {
            if (obj != context.cardController)
                return;
            context.mulliganCtrl.CardCliked(obj);
            context.OnCardMulliganSelected();
        }

        public override void Exit()
        {
            context.cardController.OnCardPointerDown -= HandleOnpointerDown;
        }
    }
}