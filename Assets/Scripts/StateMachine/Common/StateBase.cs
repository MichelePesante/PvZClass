using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace StateMachine {

    public abstract class StateBase : StateMachineBehaviour, IState {

        public abstract IState Setup(IStateMachineContext _context);

        public virtual void Enter() {

        }

        public virtual void Tick() {

        }

        public virtual void Exit() {

        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Enter();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) {
            base.OnStateUpdate(animator, stateInfo, layerIndex, controller);
            Tick();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Exit();
        }

    }

    public interface IState {
        /// <summary>
        /// Funzione chiamata quando lo stato diventa attivo.
        /// </summary>
        void Enter();

        /// <summary>
        /// Funziona richiamata ad ogni ciclo di vita dell'applicazione.
        /// </summary>
        void Tick();

        /// <summary>
        /// Funziona chiamata quando lo stato viene disattivato.
        /// </summary>
        void Exit();
    }

    public interface IStateMachineContext {

    }

}