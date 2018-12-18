using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public abstract class GP_BaseState : StateBase {

        protected GameplaySMContext context;

        public override IState Setup(IStateMachineContext _context) {
            context = _context as GameplaySMContext;
            return this;
        }

    }
}