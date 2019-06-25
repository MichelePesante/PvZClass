using UnityEngine;
using UnityEngine.UI;

namespace StateMachine.Gameplay
{
    public class GP_MatchResult_State : GP_BaseState
    {

        [SerializeField]
        GameObject WinTextPrefab;
        GameObject WinTextGO;

        public override void Enter()
        {
            if (context.PlayerOne.Data.IsAlive && !context.PlayerTwo.Data.IsAlive)
                context.Winner = context.PlayerOne;
            else if (!context.PlayerOne.Data.IsAlive && context.PlayerTwo.Data.IsAlive)
                context.Winner = context.PlayerTwo;
            if (WinTextPrefab != null)
            {
                WinTextGO = Instantiate(WinTextPrefab, context.UICanvas.transform);
                if (context.Winner == context.PlayerOne)
                    WinTextGO.transform.GetChild(0).gameObject.SetActive(false);
                else
                    WinTextGO.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}