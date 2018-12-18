using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine {

    /// <summary>
    /// Contiene le logiche di gestione e flusso degli stati.
    /// </summary>
    public abstract class StateMachineBase : MonoBehaviour {

        #region properties

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

        #region events

        public delegate void StateMachineEventHandler(IState _currentState, IState _oldState);

        /// <summary>
        /// Handler richiamato all'avvenuto cambio di stato.
        /// </summary>
        public StateMachineEventHandler OnStateChanged;

        #endregion

        #region lifecycle

        protected void changeState(IState _nextState) {
            if (_nextState == null)
                return;
            if (_nextState == CurrentState)
                return;
            IState oldState = CurrentState;
            if (oldState != null)
                oldState.Exit();
            CurrentState = _nextState;
            _nextState.Enter();
            if (OnStateChanged != null)
                OnStateChanged(CurrentState, oldState);
        }

        #endregion

    }
}