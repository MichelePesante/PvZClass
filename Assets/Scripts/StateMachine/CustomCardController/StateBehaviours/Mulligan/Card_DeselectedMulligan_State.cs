using System;
using UnityEngine;

namespace StateMachine.Card
{
    public class Card_DeselectedMulligan_State : Card_Base_State
    {
        public override void Enter()
        {
            initMulliganController();

            context.mulliganCtrl.OnCardChanged += HandleCardChanged;
            context.cardController.OnCardPointerDown += HandleOnpointerDown;
            context.cardController.SetHiglight(CardViewController.HighlightState.NoHighlight);
        }

        private void initMulliganController() {
            if (context.mulliganCtrl != null)
                return;
            MulliganController[] mulligans = FindObjectsOfType<MulliganController>();
            for (int i = 0; i < mulligans.Length; i++) {
                if (mulligans[i].GetPlayer() == context.cardController.GetPlayerOwner())
                    context.mulliganCtrl = mulligans[i];
            }
            if (!context.mulliganCtrl)
                throw new Exception("Unable to find mulligancontroller in scene");
        }

        private void HandleCardChanged()
        {
            context.OnCardMulliganChanged();
        }

        private void HandleOnpointerDown(CardViewController obj)
        {
            if (obj != context.cardController)
                return;
            context.mulliganCtrl.CardCliked(obj);
            context.OnCardMulliganSelected();
        }

        public override void Exit()
        {
            context.mulliganCtrl.OnCardChanged -= HandleCardChanged;
            context.cardController.OnCardPointerDown -= HandleOnpointerDown;
        }
    }
}