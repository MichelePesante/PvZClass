using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_Plantturn_State : GP_BaseState {
        [SerializeField]
        private Player.Type playerTurn;

        public override void Enter()
        {
            TurnManager.SetActivePlayer(playerTurn);
            context.GameFlowButton.GoToNextPhase();
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();

            // Debug
            if (Input.GetKeyDown(KeyCode.Z))
                context.PlayerOne.Life--;
            if (Input.GetKeyDown(KeyCode.X))
                context.PlayerTwo.Life--;
        }
    }
}