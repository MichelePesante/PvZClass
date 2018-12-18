using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay {

    public class GP_Setup_State : GP_BaseState {

        public Canvas canvasPrefab;
        Canvas uiCanvasInstance;

        public override void Enter() {

            if (context.UICanvas == null) {
                uiCanvasInstance = Instantiate(canvasPrefab);
            }


            Debug.Log("Canvas = " + uiCanvasInstance);
            /// TODO:
            /// - Setup grafica
            /// GameplayUI.Setup();
            /// - setup players
            /// -- vita p1... 
            /// ...
            /// ... 
            /// CALLBACK -> 
            /// 

            context.InitialCardSelectionCallBack();
        }

        public override void Exit() {
            GameObject.Destroy(uiCanvasInstance.gameObject);
            Debug.Log("Setup done");
        }
    }

}