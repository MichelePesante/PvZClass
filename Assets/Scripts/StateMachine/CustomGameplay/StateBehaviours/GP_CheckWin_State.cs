using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_CheckWin_State : GP_BaseState {

        public override void Enter() {
            // Caso 1
            context.IsWinCondition = true;

            // Caso 2
            //if (context.IsWinCondition) {
            //    if (context.GenericForwardCallBack != null)
            //        context.GenericForwardCallBack();
            //} else {
            //    if (context.GenericBackwardCallBack!= null)
            //        context.GenericBackwardCallBack();
            //}
        }

    }
}