using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace PvZClone.AppFlowSM {
    [RequireComponent(typeof(Animator))]
    public class AppFlowStateMachine : StateMachineBase {

        protected Animator SM;

        private void Start() {
            currentContext = new AppFlowSMContext() {
                ContextName = "App Flow",
                StateMachine = this,
            };

            SM = GetComponent<Animator>();

            foreach (StateMachineBehaviour smB in SM.GetBehaviours<StateMachineBehaviour>()) {
                (smB as StateBase).Setup(currentContext);
            } 

            //// solo per test... 
            //States = new List<IState>() {
            //        new Intro().Setup(currentContext),
            //        new Run().Setup(currentContext),
            //    };

            


        }

        protected override void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                //changeState(States[0]);
                SM.SetTrigger("GoToIntro");

            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                //changeState(States[1]);
                SM.SetTrigger("GoToRun");
            }

            base.Update();
        }

    }

    public class AppFlowSMContext : IStateMachineContext {
        public string ContextName;
        public GameObject MainCamera;
        public AppFlowStateMachine StateMachine;
        
    }

}