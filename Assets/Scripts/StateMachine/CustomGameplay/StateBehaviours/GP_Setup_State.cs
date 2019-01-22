using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay {

    public class GP_Setup_State : GP_BaseState {

        public Canvas canvasPrefab;
        Canvas uiCanvasInstance;

        public override void Enter() {

            if (context.UICanvas == null) {
                uiCanvasInstance = Instantiate(canvasPrefab);
            }

            CardsManager cm = new CardsManager();

            DeckController xd = cm.CreateDeck(100);
            DeckController playerOneDeck = cm.CreateDeck(20);
            DeckController playerTwoDeck = cm.CreateDeck(20);

            
            if (context.PlayerOne != null && context.PlayerTwo != null)
            {
                context.CurrentPlayer = context.PlayerOne;
                // Player Statistic
                context.PlayerOne.Life = 20;
                context.PlayerOne.MaxEnergy = 0;
                context.PlayerTwo.Life = 20;
                context.PlayerTwo.MaxEnergy = 0;
                // Deck Assignment
                context.PlayerOne.Deck = playerOneDeck;
                context.PlayerTwo.Deck = playerTwoDeck;
                // Create Hand & Draw
                context.PlayerOne.Hand = new DeckController();
                context.PlayerTwo.Hand = new DeckController();
                context.PlayerOne.Draw(8);
                context.PlayerTwo.Draw(8);
            }

            Debug.Log("Canvas = " + uiCanvasInstance);
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

        public override void Exit() {
            Debug.Log("Setup done");
        }
    }

}