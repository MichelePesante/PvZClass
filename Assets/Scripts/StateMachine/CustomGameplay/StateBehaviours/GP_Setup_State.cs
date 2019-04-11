using System;
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
            if (context.UICanvas == null) {
                uiCanvasInstance = Instantiate(canvasPrefab);
            }
            context.BoardCtrl.SetUp(boardData);
            context.UICanvas.DisableAllPanels();

            DeckSetup();

            if (context.PlayerOne != null && context.PlayerTwo != null)
            {
                context.CurrentPlayer = context.PlayerOne;

                // Player Setup
                context.PlayerOne.Setup();
                context.PlayerTwo.Setup();
            }

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
            DeckViewController playerOneDeck = context.PlayerOne.Deck;
            DeckViewController playerTwoDeck = context.PlayerTwo.Deck;

            playerOneDeck.Setup(playerOneDeck.CreateDeck(20));
            playerTwoDeck.Setup(playerTwoDeck.CreateDeck(20));
        }
    }
}