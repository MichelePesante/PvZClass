﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay {

    [RequireComponent(typeof(Animator))]

    public class GameplaySMController : StateMachineBase {

        protected Animator SM;

        [SerializeField]
        private GameplaySceneManager sceneManager;

        private void Start() {
            Setup();
        }

        public void Setup() {

            SM = GetComponent<Animator>();

            currentContext = new GameplaySMContext() {
                ContextName = "stringa GameplaySMContext",
                PlayerOne = sceneManager.player1,
                PlayerTwo = sceneManager.player2,
                P1mulliganCtrl = sceneManager.mulliganP1,
                P2mulliganCtrl = sceneManager.mulliganP2,
                BoardCtrl = sceneManager.BoardCtrl,
                // SM flow callback
                GenericForwardCallBack = GoToForward,
                GenericBackwardCallBack = GoToBack,
                // --- Reactive properties callback
                OnWinnerCondChanged = onWinConditionChanged,
                UICanvas = sceneManager.GlobalUI,
            };

            foreach (StateBase smB in SM.GetBehaviours<StateBase>()) {
                StateBase state = smB;
                if (state)
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

        //private void Update() {
        //    if (Input.GetKeyDown(KeyCode.Space)) {
        //        GoToForward();
        //    }
        //}

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

        public IPlayer PlayerOne, PlayerTwo;
        public IPlayer CurrentPlayer;

        public Action<bool> OnWinnerCondChanged;

        public BoardController BoardCtrl;

        public MulliganController P1mulliganCtrl;
        public MulliganController P2mulliganCtrl;

        public UIManager UICanvas;
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
