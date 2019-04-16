using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay {
    public class GP_Fight_State : GP_BaseState {

        public override void Enter()
        {
            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(false);
            context.BoardCtrl.StartCoroutine(CombatRoutine());
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();
        }

        //Routine
        IEnumerator CombatRoutine()
        {
            //for each lane wait lane
            foreach (LaneViewController lane in context.BoardCtrl.laneUIs)
            {
                yield return context.BoardCtrl.StartCoroutine(LaneRoutine(lane));
            }
        }

        IEnumerator LaneRoutine(LaneViewController _lane)
        {
                //p1 attack
                //if p2 hit go check
                //else card hit view update
                //p2 attack
                //if p2 hit go check
                //else card hit view update
                //card death check
        }

        public override void Exit()
        {
            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(false);
        }
    }
}