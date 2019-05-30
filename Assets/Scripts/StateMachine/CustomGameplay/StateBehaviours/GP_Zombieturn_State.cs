using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public class GP_Zombieturn_State : GP_BaseState
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
            context.GameFlowButton.ToggleGoNextButton(true);

            // Background
            context.BGManager.ChangeFactionBG(context.CurrentPlayer.Data.Faction);
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();
        }

        public override void Exit()
        {
            DeckController.ResetCardsState(TurnManager.GetActivePlayer().HandDeck);
        }
    }
}