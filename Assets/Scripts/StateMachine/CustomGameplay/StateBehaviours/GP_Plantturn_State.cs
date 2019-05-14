using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_Plantturn_State : GP_BaseState {
        [SerializeField]
        private PlayerData.Type playerTurn;

        public override void Enter()
        {
            TurnManager.SetActivePlayer(playerTurn);
            DeckController.ResetCardsState(TurnManager.GetActivePlayer().HandDeck);
            context.GameFlowButton.GoToNextPhase();
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();

            // Debug
            if (Input.GetKeyDown(KeyCode.Z))
                context.PlayerOne.Data.CurrentLife--;
            if (Input.GetKeyDown(KeyCode.X))
                context.PlayerTwo.Data.CurrentLife--;
        }

        public override void Exit() {
            DeckController.ResetCardsState(TurnManager.GetActivePlayer().HandDeck);
        }
    }
}