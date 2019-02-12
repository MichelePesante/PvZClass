using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_Fight_State : GP_BaseState {

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();
        }
    }
}