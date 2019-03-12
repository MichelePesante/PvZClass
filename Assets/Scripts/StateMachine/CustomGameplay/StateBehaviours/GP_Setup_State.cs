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
                setupPlayer(context.PlayerOne, playerOneDeck);
                setupPlayer(context.PlayerTwo, playerTwoDeck);
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

        void setupPlayer(IPlayer _player, DeckData _deck)
        {
            _player.Life = 20;
            _player.MaxEnergy = 0;
            _player.CurrentEnergy = 0;
            _player.Deck = _deck;
            _player.Deck.Player = _player;
            _player.Hand.Setup(new DeckData());
            _player.Hand.Data.Player = _player;
            _player.Draw(8);
            _player.Lost += setWinner;

        }

        void setWinner(IPlayer loser)
        {
            if (loser.CurrentType == Player.Type.one)
            {
                if (context.Winner == null)
                    context.Winner = context.PlayerTwo;
            }
            else
            {
                if (context.Winner == null)
                    context.Winner = context.PlayerOne;
            }
        }
    }

}