using UnityEngine;
using System.Collections;

namespace StateMachine.Card {

    public abstract class Card_Base_State : StateBase {

        protected CardSMContext context;

        public override IState Setup(IStateMachineContext _context) {
            context = _context as CardSMContext;
            context.OnSetupDoneCallback();
            return this;
        }
    }
}