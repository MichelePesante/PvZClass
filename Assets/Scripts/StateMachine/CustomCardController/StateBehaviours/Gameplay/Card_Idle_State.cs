using UnityEngine;
using System;

namespace StateMachine.Card
{
    public class Card_Idle_State : Card_Base_State
    {
        public override void Enter()
        {
            initBoardController();

            context.cardController.SetHiglight(CardData.Highlight.NoHighlight);
            if (context.boardCtrl.CheckCardPlayability(context.cardController))
                context.cardController.CurrentState = CardViewController.State.Playable;
            else
                context.cardController.CurrentState = CardViewController.State.Unplayable;
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