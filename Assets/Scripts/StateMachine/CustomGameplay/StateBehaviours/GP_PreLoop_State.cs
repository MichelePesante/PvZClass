using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_PreLoop_State : GP_BaseState {

        public override void Enter() {
            context.BoardCtrl.InstantiateBoard();
            context.SceneManager.playerOneHandUI.Setup(context.PlayerOne.Hand);
            context.SceneManager.playerTwoHandUI.Setup(context.PlayerTwo.Hand);
            context.GenericForwardCallBack();
        }

        public override void Exit() {
            foreach (CardController card in context.SceneManager.playerOneHandUI.instantiatedCards) {
                card.CurrentState = CardController.State.Idle; //TODO: refactoring cardcontrollersz.
            }
        }

    }
}
