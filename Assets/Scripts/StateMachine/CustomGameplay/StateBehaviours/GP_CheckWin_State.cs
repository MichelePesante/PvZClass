using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public class GP_CheckWin_State : GP_BaseState
    {
        public override void Enter()
        {
            if (context.PlayerOne.Data.IsAlive && !context.PlayerTwo.Data.IsAlive)
                context.Winner = context.PlayerOne;
            else if (!context.PlayerOne.Data.IsAlive && context.PlayerTwo.Data.IsAlive)
                context.Winner = context.PlayerTwo;

            if (context.Winner == null)
            {
                context.IsWinCondition = false;
                //if (context.GenericForwardCallBack != null)
                //    context.GenericForwardCallBack();
            }
            else
                context.IsWinCondition = true;
        }
    }
}