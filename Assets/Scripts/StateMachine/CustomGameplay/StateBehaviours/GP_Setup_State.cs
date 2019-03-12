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
            CardsManager cm = new CardsManager();

            DeckData playerOneDeck = cm.CreateDeck(20);
            DeckData playerTwoDeck = cm.CreateDeck(20);

            if (context.PlayerOne != null && context.PlayerTwo != null)
            {
                context.CurrentPlayer = context.PlayerOne;
                // Player Setup
                context.PlayerOne.Setup(playerOneDeck);
                context.PlayerTwo.Setup(playerTwoDeck);
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

    }

}