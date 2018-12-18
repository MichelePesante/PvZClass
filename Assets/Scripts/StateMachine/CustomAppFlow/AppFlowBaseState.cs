using UnityEngine;
using System.Collections;
using StateMachine;

namespace PvZClone.AppFlowSM {

    

    public abstract class AppFlowBaseState : StateBase {

        protected AppFlowSMContext ctx;
        
        public override IState Setup(IStateMachineContext _context) {
            ctx = _context as AppFlowSMContext;
            return this;
        }

    }

    

}