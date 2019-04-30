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
            context.P1mulliganCtrl.Init(context.PlayerOne.Hand.Data);
            context.P2mulliganCtrl.Init(context.PlayerTwo.Hand.Data);
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

        private void P1DeckSetup()
        {
            DeckData deckFrom = null;
            context.PlayerOne.Hand.Moves(ref deckFrom, ref p1ChosenCards);
            context.PlayerOne.Hand.Data.Player = context.PlayerOne;
            for (int i = 0; i < p1NotChosenCards.Count; i++)
            {
                DeckData deckToAdd = context.PlayerOne.Deck.Data;
                CardData cardToMove = p1NotChosenCards[i];
                DeckController.Move(ref deckToAdd, ref deckFrom, ref cardToMove);
            }
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

        private void P2DeckSetup()
        {
            DeckData deckFrom = null;
            context.PlayerTwo.Hand.Moves(ref deckFrom, ref p2ChosenCards);
            context.PlayerTwo.Hand.Data.Player = context.PlayerTwo;
            for (int i = 0; i < p2NotChosenCards.Count; i++)
            {
                DeckData deckToAdd = context.PlayerTwo.Deck.Data;
                CardData cardToMove = p2NotChosenCards[i];
                DeckController.Move(ref deckToAdd, ref deckFrom, ref cardToMove);
            }
        }
        #endregion

        public override void Exit()
        {
            context.P1mulliganCtrl.OnMulliganEnd -= MulliganEndP1;
            context.P2mulliganCtrl.OnMulliganEnd -= MulliganEndP2;

            P1DeckSetup();
            P2DeckSetup();

            context.UICanvas.DisableAllPanels();
        }
    }
}