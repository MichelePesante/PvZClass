using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_InitialCardSelection_State : GP_BaseState {

        public override void Enter() {

            mulliganPlayerCount = 0;
            context.P1mulliganCtrl.OnMulliganEnd += MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd += MulliganEndP2;
            context.UICanvas.EnableMenu(PanelType.Mulligan);
            context.P1mulliganCtrl.Init(context.PlayerOne.Hand);
            context.P2mulliganCtrl.Init(context.PlayerTwo.Hand);
        }

        public override void Tick() {
            base.Tick();

            //Simulazione dell'"Ok" dei player
            if (Input.GetKeyDown(KeyCode.Space)) {
                context.GenericForwardCallBack();
            }
        }

        public override void Exit() {
            context.P1mulliganCtrl.OnMulliganEnd -= MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd -= MulliganEndP2;

            context.UICanvas.DisableAllPanels();
            context.BoardCtrl.InstantiateBoard();
        }

        int mulliganPlayerCount;
        private void MulliganEndP1(List<CardData> _chosenCards) {
            context.PlayerOne.Hand = new DeckController(_chosenCards);
            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        private void MulliganEndP2(List<CardData> _chosenCards) {
            context.PlayerTwo.Hand = new DeckController(_chosenCards);
            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }
    }

}