using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_SetupTurn_State : GP_BaseState {

        public override void Enter()
        {
            context.UICanvas.EnableMenu(PanelType.Board);
            context.CurrentPlayer.MaxEnergy++;
            context.CurrentPlayer.CurrentEnergy = context.CurrentPlayer.MaxEnergy;
            context.CurrentPlayer.Draw();
        }
    }

}