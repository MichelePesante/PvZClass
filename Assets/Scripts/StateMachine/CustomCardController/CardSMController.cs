using UnityEngine;
using System.Collections;

namespace StateMachine.Card {

    public class CardSMController : StateMachineBase {

        protected override IStateMachineContext ContextSetup() {
            return new CardSMContext() {
                cardController = GetComponent<CardController>(),
            };
        }
    }

    public class CardSMContext : IStateMachineContext {
        public CardController cardController;

    }
}
