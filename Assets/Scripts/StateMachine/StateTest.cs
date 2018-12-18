using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using StateMachine;

namespace StateMachine.Gameplay {

    public class StateTest : StateBase {

        GameplaySMContext context;

        public override IState Setup(IStateMachineContext _context) {
            context = _context as GameplaySMContext;
            return this;
        }
        

    }
}