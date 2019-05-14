using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_Zombieturn_State : GP_BaseState {
        [SerializeField]
        private PlayerData.Type playerTurn;

        public override void Enter()
        {
            TurnManager.SetActivePlayer(playerTurn);
            DeckController.SetCardsState(GameplaySceneManager.GetPlayer(playerTurn).HandDeck, CardState.Idle);
            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(true);
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();
        }

        public override void Exit() {
            DeckController.SetCardsState(GameplaySceneManager.GetPlayer(playerTurn).HandDeck, CardState.Inactive);
        }
    }
}