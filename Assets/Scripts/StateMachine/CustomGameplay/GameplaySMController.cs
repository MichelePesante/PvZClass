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
                // SM flow callback
                GenericForwardCallBack = GoToForward,
                GenericBackwardCallBack = GoToBack,
                // --- Reactive properties callback
                OnWinnerCondChanged = onWinConditionChanged,
            };

            foreach (StateMachineBehaviour smB in SM.GetBehaviours<StateMachineBehaviour>()) {
                StateBase state = smB as StateBase;
                if(state)
                    state.Setup(currentContext);
            }

        }

        #region reactive properties callback

        private void onWinConditionChanged(bool winCondition) {
            SM.SetBool("WinCondition", winCondition);
        }

        #endregion

        #region Flow callbacks

        /// <summary>
        /// Chiamata quando lo stato avrà terminato il suo lavoro
        /// </summary>
        private void GoToForward() {
            SM.SetTrigger("GoToForward");
        }

        private void GoToBack() {
            SM.SetTrigger("GoToBack");
        }

        #endregion

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                GoToForward();
            }
        }

    }

    public class GameplaySMContext : IStateMachineContext {
        public string ContextName;
        private bool _isWinCondition;
        public bool IsWinCondition {
            get { return _isWinCondition; }
            set {
                _isWinCondition = value;
                if (OnWinnerCondChanged != null)
                    OnWinnerCondChanged(_isWinCondition);
            }
        }
        public Action<bool> OnWinnerCondChanged;

        public Canvas UICanvas;
        /// <summary>
        /// Callback generica per passaggio forward philosophy.
        /// </summary>
        public Action GenericForwardCallBack;
        /// <summary>
        /// Callback generica per passaggio backward philosophy.
        /// </summary>
        public Action GenericBackwardCallBack;
    }

}
