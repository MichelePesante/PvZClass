using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public class GP_Plantturn_State : GP_BaseState
    {
        [SerializeField]
        private PlayerData.Type playerTurn;

        public override void Enter()
        {
            PlayerView oldActivePlayer = TurnManager.GetActivePlayer();
            TurnManager.SetActivePlayer(playerTurn);
            if (oldActivePlayer != null)
                DeckController.ResetCardsState(oldActivePlayer.HandDeck);
            DeckController.ResetCardsState(TurnManager.GetActivePlayer().HandDeck);

            context.GameFlowButton.GoToNextPhase();

            // Background
            context.BGManager.ChangeFactionBG(TurnManager.GetActivePlayer().Data.Faction); 
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

        public override void Exit()
        {
            DeckController.ResetCardsState(TurnManager.GetActivePlayer().HandDeck);
        }
    }
}