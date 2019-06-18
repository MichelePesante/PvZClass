using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Card
{
    public class Card_SelectedMulligan_State : Card_Base_State
    {
        public override void Enter()
        {
            context.mulliganCtrl.OnCardChanged += HandleCardChanged;
            context.cardViewController.OnCardPointerDown += HandleOnpointerDown;
            context.cardViewController.SetHiglight(CardViewController.HighlightState.Highlighted);
        }

        private void HandleCardChanged()
        {
            context.OnCardMulliganChanged();
        }

        private void HandleOnpointerDown(CardViewController obj)
        {
            if (obj != context.cardViewController)
                return;
            context.mulliganCtrl.CardCliked(obj);
            context.OnCardMulliganDeselected();
        }

        public override void Exit()
        {
            context.mulliganCtrl.OnCardChanged -= HandleCardChanged;
            context.cardViewController.OnCardPointerDown -= HandleOnpointerDown;
            context.cardViewController.SetHiglight(CardViewController.HighlightState.NoHighlight);
        }
    }
}
