using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_SetupTurn_State : GP_BaseState {

        public override void Enter()
        {
            base.Enter();

            context.CurrentPlayer.MaxEnergy++;
            context.CurrentPlayer.CurrentEnergy = context.CurrentPlayer.MaxEnergy;
            context.CurrentPlayer.Draw();
        }
    }

}