using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Gameplay
{

    public class GP_PreLoop_State : GP_BaseState
    {

        public override void Enter()
        {
            context.BoardCtrl.InstantiateBoard();
            context.GenericForwardCallBack();
            context.GameFlowButton.Setup(context.GenericForwardCallBack);
            DeckData deckFromTo = null;
            context.PlayerOne.Hand.Moves(ref deckFromTo, ref context.P1firstHandCardDrawn);
            context.PlayerTwo.Hand.Moves(ref deckFromTo, ref context.P2firstHandCardDrawn);
        }

        public override void Exit()
        {
            context.P1firstHandCardDrawn = null;
            context.P2firstHandCardDrawn = null;
        }
    }
}
