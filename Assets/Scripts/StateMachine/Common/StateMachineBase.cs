using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine {

    /// <summary>
    /// Contiene le logiche di gestione e flusso degli stati.
    /// </summary>
    public abstract class StateMachineBase : MonoBehaviour {

        #region properties

        protected Animator SM;
        /// <summary>
        /// Lista degli stati totali.
        /// </summary>
        public List<IState> States { get; set; }

        /// <summary>
        /// Stato attivo.
        /// </summary>
        public IState CurrentState { get; private set; }

        /// <summary>
        /// Contesto.
        /// </summary>
        protected IStateMachineContext currentContext;

        #endregion

        protected virtual void Setup() {
            SM = GetComponent<Animator>();

            currentContext = ContextSetup();

            foreach (StateBase smB in SM.GetBehaviours<StateBase>()) {
                StateBase state = smB;
                if (state)
                    state.Setup(currentContext);
            }
        }

        protected abstract IStateMachineContext ContextSetup();

        #region events

        public delegate void StateMachineEventHandler(IState _currentState, IState _oldState);

        /// <summary>
        /// Handler richiamato all'avvenuto cambio di stato.
        /// </summary>
        public StateMachineEventHandler OnStateChanged;

        #endregion



    }
}