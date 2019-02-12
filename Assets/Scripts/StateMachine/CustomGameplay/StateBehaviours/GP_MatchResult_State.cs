using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace StateMachine.Gameplay {
    public class GP_MatchResult_State : GP_BaseState {

        [SerializeField]
        GameObject WinTextPrefab;
        GameObject WinTextGO;

        public override void Enter()
        {
            base.Enter();
            if (WinTextPrefab != null)
            {
                WinTextGO = Instantiate(WinTextPrefab, context.UICanvas.transform);
                WinTextGO.GetComponent<Text>().text = context.Winner.CurrentType + " win";
            }
        }
    }
}