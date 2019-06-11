using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_SetupTurn_State : GP_BaseState {

        public override void Enter()
        {
            context.UICanvas.EnableMenu(PanelType.Board);
            setupTurnPlayer(context.PlayerOne);
            setupTurnPlayer(context.PlayerTwo);

            CardViewManager.DoMovementActions(handleActionCallback);
        }

        void setupTurnPlayer(PlayerView _player)
        {
            _player.Data.MaxEnergy++;
            _player.Data.CurrentEnergy = context.CurrentPlayer.Data.MaxEnergy;
            DeckData decktoDrawFrom = _player.PlayerDeck.Data;
            _player.HandDeck.Draw(ref decktoDrawFrom, 1, false);
        }

        void handleActionCallback()
        {
            context.GenericForwardCallBack();
        }
    }

}