﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay {

    public class GP_Setup_State : GP_BaseState {

        public BoardData boardData;
        public Canvas canvasPrefab;
        Canvas uiCanvasInstance;

        public override void Enter() {
            // player setup
            context.PlayerOne.Init(new PlayerData(PlayerData.Type.one) { Faction = CardData.Faction.Alcool });
            context.PlayerTwo.Init(new PlayerData(PlayerData.Type.two) { Faction = CardData.Faction.Hipster });
            // board setup 
            context.BoardCtrl.SetUp(boardData);
            // deck setup
            DeckSetup();

            // UI
            if (context.UICanvas == null) {
                uiCanvasInstance = Instantiate(canvasPrefab);
            }
            context.UICanvas.DisableAllPanels();


            /// TODO:
            /// - Setup grafica
            /// GameplayUI.Setup();
            /// - setup players
            /// -- vita p1... 
            /// ...
            /// ... 
            /// CALLBACK -> 
            /// 

            context.GenericForwardCallBack();
        }

        private void DeckSetup()
        {
            if (context.PlayerOne != null && context.PlayerTwo != null) {
                context.CurrentPlayer = context.PlayerOne;
                // Player Setup
                // p1 hand 
                DeckViewController p1Hand = CardViewManager.GetHandDeck(context.PlayerOne.Data.CurrentType);
                p1Hand.Data = new DeckData("P1 Hand");
                context.PlayerOne.SetHandDeck(p1Hand);
                // p1 deck 
                DeckViewController p1Deck = CardViewManager.GetPlayerDeck(context.PlayerOne.Data.CurrentType);
                p1Deck.Data = new DeckData("P1 Deck");
                context.PlayerOne.SetPlayerDeck(p1Deck);
                // p2 hand
                DeckViewController p2Hand = CardViewManager.GetHandDeck(context.PlayerTwo.Data.CurrentType);
                p2Hand.Data = new DeckData("P2 Hand");
                context.PlayerTwo.SetHandDeck(p2Hand);
                // p2 deck 
                DeckViewController p2Deck = CardViewManager.GetPlayerDeck(context.PlayerTwo.Data.CurrentType);
                p2Deck.Data = new DeckData("P2 Deck");
                context.PlayerTwo.SetPlayerDeck(p2Deck);
                //Trash Deck
                DeckViewController trahDeck = CardViewManager.GetTrashDeckView();
                trahDeck.Data = new DeckData("Trash");
            }

            context.PlayerOne.PlayerDeck.Setup(DeckController.CreateDeck(context.PlayerOne.Data, 20));
            context.PlayerTwo.PlayerDeck.Setup(DeckController.CreateDeck(context.PlayerTwo.Data, 20));
        }
    }
}