using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_ZombieTrick_State : GP_BaseState {

        public override void Enter()
        {
            // Background
            context.BGManager.ChangeFactionBG(TurnManager.GetActivePlayer().Data.Faction);
            context.GameFlowButton.GoToNextPhase();
        }
    }
}