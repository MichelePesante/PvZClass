using UnityEngine;
using System.Collections;
using System;

namespace StateMachine.Card {

    [RequireComponent(typeof (CardViewController))]
    public class CardSMController : StateMachineBase {
        [SerializeField]
        private RuntimeAnimatorController mulliganSMPrefab;
        [SerializeField]
        private RuntimeAnimatorController gameplaySMPrefab;

        protected override IStateMachineContext ContextSetup() {

            CardSMContext tmpContext = new CardSMContext() {
                OnSetupDoneCallback = HandleOnSetupDone,
                cardViewController = GetComponent<CardViewController>(),
                mulliganCtrl = null,
                OnCardMulliganSelected = HandleCardMulliganSelected,
                OnCardMulliganDeselected = HandleCardMulliganDeselected,
                OnCardMulliganChanged = HandleCardMulliganChanged,
                boardCtrl = null,                
            };
            tmpContext.cardViewController.Data.OnCurrentStateChanged += OnCardStateChanged;
            return tmpContext;
        }

        private void OnCardStateChanged(CardState _currentState) {
            SM.SetInteger("CardStateInHand", (int)_currentState);
        }

        #region Handler
        private void HandleOnSetupDone()
        {
            SM.SetTrigger("SetupDone");
        }

        #region Mulligan
        private void HandleCardMulliganSelected()
        {
            if (SM.runtimeAnimatorController == mulliganSMPrefab)
                SM.SetTrigger("Selected");
        }

        private void HandleCardMulliganDeselected()
        {
            if (SM.runtimeAnimatorController == mulliganSMPrefab)
                SM.SetTrigger("Deselected");
        }

        private void HandleCardMulliganChanged()
        {
            if (SM.runtimeAnimatorController == mulliganSMPrefab)
                SM.SetTrigger("Changed");
        }
        #endregion

        #endregion

        private void OnDestroy()
        {
            (currentContext as CardSMContext).cardViewController.Data.OnCurrentStateChanged -= OnCardStateChanged;
        }
    }

    public class CardSMContext : IStateMachineContext
    {
        #region Mulligan
        public Action OnCardMulliganSelected;
        public Action OnCardMulliganDeselected;
        public Action OnCardMulliganChanged;
        public MulliganController mulliganCtrl;
        #endregion

        public Action OnSetupDoneCallback;
        public CardViewController cardViewController;
        public BoardController boardCtrl;
    }
}