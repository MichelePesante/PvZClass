using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_MatchResult_State : GP_BaseState {

        public override void Enter()
        {
            base.Enter();
            Debug.Log(context.Winner);
        }
    }
}