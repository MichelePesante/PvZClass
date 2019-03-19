using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_PreLoop_State : GP_BaseState {

        public override void Enter() {
            context.BoardCtrl.InstantiateBoard();
            context.GenericForwardCallBack();
            context.PlayerOne.Hand.InstantiateCards();
            context.PlayerTwo.Hand.InstantiateCards();
            context.GameFlowButton.Setup(context.GenericForwardCallBack);
        }

    }
}
