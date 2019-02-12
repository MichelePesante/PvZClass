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
            context.GenericForwardCallBack();
        }

        void setupTurnPlayer(IPlayer _player)
        {
            _player.MaxEnergy++;
            _player.CurrentEnergy = context.CurrentPlayer.MaxEnergy;
            Debug.Log(_player.CurrentEnergy);
            _player.Draw();
        }
    }

}