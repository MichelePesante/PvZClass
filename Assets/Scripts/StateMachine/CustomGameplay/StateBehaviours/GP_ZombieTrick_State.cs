using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_ZombieTrick_State : GP_BaseState {

        public override void Enter()
        {
            context.GameFlowButton.GoToNextPhase();
        }
    }
}