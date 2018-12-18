using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay {

    [RequireComponent(typeof(Animator))]

    public class GameplaySMController : StateMachineBase {

        protected Animator SM;

        // Use this for initialization
        void Start() {

            SM = GetComponent<Animator>();

            currentContext = new GameplaySMContext() {
                ContextName = "stringa GameplaySMContext",
                InitialCardSelectionCallBack = GoToNext,
            };

            foreach (StateMachineBehaviour smB in SM.GetBehaviours<StateMachineBehaviour>()) {
                StateBase state = smB as StateBase;
                if(state)
                    state.Setup(currentContext);
            }

        }

        /// <summary>
        /// Chiamata quando lo stato avrà terminato il suo lavoro
        /// </summary>
        private void GoToNext() {
            SM.SetTrigger("GoToNext");
        }

    }

    public class GameplaySMContext : IStateMachineContext {
        public string ContextName;
        public Canvas UICanvas;
        public Action InitialCardSelectionCallBack;
    }

}
