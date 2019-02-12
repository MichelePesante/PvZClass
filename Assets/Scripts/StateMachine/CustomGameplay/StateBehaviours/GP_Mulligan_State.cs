using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_Mulligan_State : GP_BaseState {

        public override void Enter() {

            mulliganPlayerCount = 0;
            // Mi iscrivo all'evento di fine scelta carte per ogni mulligun del player
            context.P1mulliganCtrl.OnMulliganEnd += MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd += MulliganEndP2;
            // Abilitiamo la UI del mulligan
            context.UICanvas.EnableMenu(PanelType.Mulligan);
            // Inizializiamo il pannello del mulligan
            context.P1mulliganCtrl.Init(context.PlayerOne.Hand);
            context.P2mulliganCtrl.Init(context.PlayerTwo.Hand);
        }

        int mulliganPlayerCount;
        private void MulliganEndP1(List<CardData> _chosenCards, List<CardData> _notSelectedCards) {
            context.PlayerOne.Hand = new DeckController(_chosenCards, context.PlayerOne);
            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        private void MulliganEndP2(List<CardData> _chosenCards, List<CardData> _notSelectedCards) {
            context.PlayerTwo.Hand = new DeckController(_chosenCards, context.PlayerTwo);
            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        public override void Exit() {
            context.P1mulliganCtrl.OnMulliganEnd -= MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd -= MulliganEndP2;

            context.UICanvas.DisableAllPanels();
            context.BoardCtrl.InstantiateBoard();
        }

    }

}