using UnityEngine;
using System.Collections;
using StateMachine;

namespace PvZClone.AppFlowSM {

    public class Intro : AppFlowBaseState {

        public override void Enter() {
            Debug.LogFormat("Entrato stato con contesto {0}: {1}", ctx.ContextName, "Intro");
            ctx.MainCamera = Camera.main.gameObject;
        }

        public override void Tick() {
            Debug.Log("Tick stato: " + "Intro");
        }

        public override void Exit() {
            Debug.Log("Uscito stato: " + "Intro");
        }

    }
}