using UnityEngine;
using System;

namespace StateMachine.Card
{
    public class Card_Idle_State : Card_Base_State
    {
        public override void Enter()
        {
            initBoardController();

            context.cardController.SetHiglight(CardViewController.HighlightState.NoHighlight);
            // TODO : refactoring funzione in controller
            if (context.cardController.GetPlayerOwner() != TurnManager.GetActivePlayer().Data)
            {
                context.cardController.Data.CurrentState = CardState.Inactive;
                return;
            }
            if (context.boardCtrl.CheckCardPlayability(context.cardController))
                context.cardController.Data.CurrentState = CardState.Playable;
            else
                context.cardController.Data.CurrentState = CardState.Unplayable;
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