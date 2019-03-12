using UnityEngine;

namespace StateMachine.Card {

    public class Card_Played_State : Card_Base_State {

        public override void Enter()
        {
            if (CardController.OnPlaced != null)
                CardController.OnPlaced(context.cardController.Data);
        }

    }
}