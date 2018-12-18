using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay {

    public class GP_InitialCardSelection_State : GP_BaseState {
        public override void Enter() {
            base.Enter();
            /// TODO:
            /// - Creazione card visive 
        }

        public override void Tick() {
            base.Tick();

            //Simulazione dell'"Ok" dei player
            if (Input.GetKeyDown(KeyCode.Space)) {
                context.GenericForwardCallBack();
            }
        }

        public override void Exit() {
            base.Exit();
            // disiscrizione da handlers dei bottoni
        }

    }

}