using UnityEngine;
using System;

namespace StateMachine.Card
{
    public class Card_Idle_State : Card_Base_State
    {
        public override void Enter()
        {
            initBoardController();

            context.cardViewController.SetHiglight(CardViewController.HighlightState.NoHighlight);
            // TODO : refactoring funzione in controller
            if (context.cardViewController.GetPlayerOwner().Data != TurnManager.GetActivePlayer().Data)
            {
                context.cardViewController.Data.CurrentState = CardState.Inactive;
                return;
            }
            if (context.boardCtrl.CheckCardPlayability(context.cardViewController))
                context.cardViewController.Data.CurrentState = CardState.Playable;
            else
                context.cardViewController.Data.CurrentState = CardState.Unplayable;
        }

        private void initBoardController() {
            BoardController boardController = FindObjectOfType<BoardController>();

            if (!boardController) {
                throw new Exception("Unable to find boardcontroller in scene");
            }

            context.boardCtrl = boardController;
        }
    }
}