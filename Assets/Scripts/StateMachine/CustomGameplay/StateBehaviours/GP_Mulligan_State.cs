using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay
{

    public class GP_Mulligan_State : GP_BaseState
    {

        public override void Enter()
        {

            mulliganPlayerCount = 0;
            // Mi iscrivo all'evento di fine scelta carte per ogni mulligun del player
            context.P1mulliganCtrl.OnMulliganEnd += MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd += MulliganEndP2;
            // Abilitiamo la UI del mulligan
            context.UICanvas.EnableMenu(PanelType.Mulligan);
            // Inizializiamo il pannello del mulligan
            context.P1mulliganCtrl.Init(context.PlayerOne);
            context.P2mulliganCtrl.Init(context.PlayerTwo);

            // Background
            context.BGManager.SetNeutralBG();
        }

        int mulliganPlayerCount;

        #region P1
        List<CardData> p1ChosenCards;
        List<CardData> p1NotChosenCards;
        private void MulliganEndP1(List<CardData> _chosenCards, List<CardData> _notChosenCards)
        {
            p1ChosenCards = _chosenCards;
            p1NotChosenCards = _notChosenCards;

            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        private void P1DeckSetupCallback()
        {
            DeckData deckFrom = null;
            // muove le carte dal null alla mano
            context.PlayerOne.HandDeck.DoMoves(ref deckFrom, ref p1ChosenCards);
            // muove le carte dal null al playerDeck
            context.PlayerOne.PlayerDeck.DoMoves(ref deckFrom, ref p1NotChosenCards);

        }
        #endregion

        #region P2
        List<CardData> p2ChosenCards;
        List<CardData> p2NotChosenCards;
        private void MulliganEndP2(List<CardData> _chosenCards, List<CardData> _notChosenCards)
        {
            p2ChosenCards = _chosenCards;
            p2NotChosenCards = _notChosenCards;

            mulliganPlayerCount++;
            if (mulliganPlayerCount == 2)
                context.GenericForwardCallBack();
        }

        private void P2DeckSetupCallback()
        {
            DeckData deckFrom = null;
            // muove le carte dal null alla mano
            context.PlayerTwo.HandDeck.DoMoves(ref deckFrom, ref p2ChosenCards);
            // muove le carte dal null al playerDeck
            context.PlayerTwo.PlayerDeck.DoMoves(ref deckFrom, ref p2NotChosenCards);
        }
        #endregion

        public override void Exit()
        {
            context.P1mulliganCtrl.OnMulliganEnd -= MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd -= MulliganEndP2;

            P1DeckSetupCallback();
            P2DeckSetupCallback();

            context.UICanvas.DisableAllPanels();
        }
    }
}