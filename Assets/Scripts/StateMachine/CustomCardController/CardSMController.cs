using UnityEngine;
using System.Collections;
using System;

namespace StateMachine.Card {

    [RequireComponent(typeof (CardController))]
    public class CardSMController : StateMachineBase {
        [SerializeField]
        private RuntimeAnimatorController mulliganSMPrefab;
        [SerializeField]
        private RuntimeAnimatorController gameplaySMPrefab;

        protected override IStateMachineContext ContextSetup() {
            CardSMContext tmpContext = new CardSMContext() {
                OnSetupDoneCallback = HandleOnSetupDone,
                cardController = GetComponent<CardController>(),
                mulliganCtrl = GameplaySceneManager.GetMulliganController(GetComponent<CardController>().GetPlayerOwner().CurrentType),
                boardCtrl = GameplaySceneManager.GetBoardController(),                
            };
            tmpContext.cardController.OnCurrentStateChanged += OnCardStateChanged;
            return tmpContext;
        }

        private void OnCardStateChanged(CardController.State _currentState) {
            SM.SetInteger("CardStateInHand", (int)_currentState);
        }

        #region Handler
        private void HandleOnSetupDone()
        {
            SM.SetTrigger("SetupDone");
        }

        private void HandleCardMulliganSelected()
        {
            if (SM.runtimeAnimatorController == mulliganSMPrefab)
                SM.SetTrigger("Selected");
        }

        private void HandleCardMulliganDeselected()
        {
            if (SM.runtimeAnimatorController == mulliganSMPrefab)
                SM.SetTrigger("Idle");
        }
        #endregion
    }

    public class CardSMContext : IStateMachineContext
    {
        #region Mulligan
        public Action OnCardMulliganSelected;
        public Action OnCardMulliganDeselected;
        public MulliganController mulliganCtrl;
        #endregion

        public Action OnSetupDoneCallback;
        public CardController cardController;
        public BoardController boardCtrl;
    }
}