using UnityEngine;
using System.Collections;
using System;

namespace StateMachine.Card {

    [RequireComponent(typeof (CardController))]
    public class CardSMController : StateMachineBase {

        protected override IStateMachineContext ContextSetup() {
            CardSMContext tmpContext = new CardSMContext() {
                cardController = GetComponent<CardController>(),
            };
            tmpContext.cardController.OnCurrentStateChanged += OnCardStateChanged;
            return tmpContext;
        }

        private void OnCardStateChanged(CardController.State _currentState) {
            SM.SetInteger("CardStateInHand", (int)_currentState);
        }
    }

    public class CardSMContext : IStateMachineContext {
        public CardController cardController;

    }
}