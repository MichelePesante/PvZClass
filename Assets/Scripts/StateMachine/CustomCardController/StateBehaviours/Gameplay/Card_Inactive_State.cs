using UnityEngine;
using System.Collections;

namespace StateMachine.Card
{
    public class Card_Inactive_State : Card_Base_State
    {
        [SerializeField] Sprite hipsterCoverSprite, alcoolCoverSprite;

        public override void Enter()
        {
            base.Enter();
            if (context.cardController.Data.CardFaction == CardData.Faction.Hipster)
                context.cardController.Cover.sprite = hipsterCoverSprite;
            else if (context.cardController.Data.CardFaction == CardData.Faction.Alcool)
                context.cardController.Cover.sprite = alcoolCoverSprite;

            context.cardController.Cover.enabled = true;
        }

        public override void Exit()
        {
            base.Exit();
            context.cardController.Cover.enabled = false;
        }
    }
}