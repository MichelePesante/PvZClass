using UnityEngine;

namespace StateMachine.Card {

    public class Card_Played_State : Card_Base_State {

        public override void Enter()
        {
            context.cardViewController.Place();
            if (CardController.OnPlaced != null)
                CardController.OnPlaced(context.cardViewController.Data);
        }

    }
}