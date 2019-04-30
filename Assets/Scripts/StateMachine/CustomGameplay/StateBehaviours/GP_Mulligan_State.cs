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
            context.P1mulliganCtrl.Init(context.PlayerOne.Hand.Data);
            context.P2mulliganCtrl.Init(context.PlayerTwo.Hand.Data);
        }

        int mulliganPlayerCount;
        private void MulliganEndP1(List<CardData> _chosenCards, List<CardData> _notSelectedCards) {
            context.PlayerOne.Hand.Setup(new DeckData(_chosenCards, 8));
            context.PlayerOne.Hand.Data.Player = context.PlayerOne;
            context.P1firstHandCardDrawn = _chosenCards;
            for (int i = 0; i < _notSelectedCards.Count; i++)
            {
                DeckController.AddCard(context.PlayerOne.Deck.Data, _notSelectedCards[i]);
            }
            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        private void MulliganEndP2(List<CardData> _chosenCards, List<CardData> _notSelectedCards) {
            context.PlayerTwo.Hand.Setup(new DeckData(_chosenCards, 8));
            context.PlayerTwo.Hand.Data.Player = context.PlayerTwo;
            context.P2firstHandCardDrawn = _chosenCards;
            for (int i = 0; i < _notSelectedCards.Count; i++)
            {
                DeckController.AddCard(context.PlayerTwo.Deck.Data, _notSelectedCards[i]);
            }            
            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        public override void Exit() {
            context.P1mulliganCtrl.OnMulliganEnd -= MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd -= MulliganEndP2;

            context.UICanvas.DisableAllPanels();
        }

    }

}