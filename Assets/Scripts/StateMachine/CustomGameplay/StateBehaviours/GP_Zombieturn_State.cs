using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_Zombieturn_State : GP_BaseState {
        [SerializeField]
        private Player.Type playerTurn;

        public override void Enter()
        {
            TurnManager.SetActivePlayer(playerTurn);
            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(true);
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();
        }
    }
}