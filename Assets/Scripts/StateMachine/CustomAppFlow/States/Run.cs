using UnityEngine;
using System.Collections;
using StateMachine;

namespace PvZClone.AppFlowSM {

    public class Run : AppFlowBaseState {

        public override void Enter() {
            ctx.StateMachine.gameObject.name = "CiaoSonoLaSM" + ctx.ContextName;
            Debug.Log("Entrato stato: " + "Run");
        }

        public override void Tick() {
            Debug.Log("Tick stato: " + "Run");
        }

        public override void Exit() {
            Debug.Log("Uscito stato: " + ctx.StateMachine.name);
        }

    }
}