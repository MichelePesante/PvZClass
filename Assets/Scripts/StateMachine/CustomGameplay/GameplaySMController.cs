using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay {

    [RequireComponent(typeof(Animator))]

    public class GameplaySMController : StateMachineBase {

        [SerializeField]
        private GameplaySceneManager sceneManager;

        private void Start() {
            sceneManager.Setup();
            Setup();
        }

        #region reactive properties callback

        private void onMatchEnd() {
            SM.SetTrigger("MatchEnd");
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

        protected override IStateMachineContext ContextSetup() {
            return new GameplaySMContext() {
                SceneManager = sceneManager,
                ContextName = "stringa GameplaySMContext",
                PlayerOne = sceneManager.player1,
                PlayerTwo = sceneManager.player2,
                Winner = null,
                P1mulliganCtrl = sceneManager.mulliganP1,
                P2mulliganCtrl = sceneManager.mulliganP2,
                BoardCtrl = sceneManager.BoardCtrl,
                GameFlowButton = sceneManager.gameFlowButton,
                BGManager = sceneManager.BGManager,
                // SM flow callback
                GenericForwardCallBack = GoToForward,
                GenericBackwardCallBack = GoToBack,
                // --- Reactive properties callback
                OnMatchEnd = onMatchEnd,
                UICanvas = sceneManager.GlobalUI,
            };
        }

        #endregion
    }

    public class GameplaySMContext : IStateMachineContext {
        public string ContextName;

        public GameplaySceneManager SceneManager;

        public PlayerView PlayerOne, PlayerTwo;
        public PlayerView CurrentPlayer;
        public PlayerView Winner;

        public Action OnMatchEnd;
        public BoardController BoardCtrl;

        public GameFlowButtonController GameFlowButton;

        public MulliganController P1mulliganCtrl;
        public MulliganController P2mulliganCtrl;

        public BackgroundManager BGManager;

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
