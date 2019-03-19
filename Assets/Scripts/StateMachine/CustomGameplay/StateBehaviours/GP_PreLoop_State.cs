using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_PreLoop_State : GP_BaseState {

        public override void Enter() {
            context.BoardCtrl.InstantiateBoard();
            context.GenericForwardCallBack();
            context.PlayerOne.Hand.AddViews(context.P1firstHandCardDrawn);
            context.PlayerTwo.Hand.AddViews(context.P2firstHandCardDrawn);
        }

        public override void Exit()
        {
            context.P1firstHandCardDrawn = null;
            context.P2firstHandCardDrawn = null;
        }
    }
}
