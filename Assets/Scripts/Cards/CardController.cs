using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {

    public CardData cardData;

    [HideInInspector]
    public Sprite CardImage;
    [HideInInspector]
    public int Cost, Attack, Life;
    [HideInInspector]
    public bool IsHeroCard;
    [HideInInspector]
    public CardData.Rarity Rarity;

    public void SetUP()
    {
        CardImage = cardData.CardImage;
        Rarity = cardData.CardRarity;
        Cost = cardData.Cost;
        Attack = cardData.Attack;
        Life = cardData.Life;
        IsHeroCard = cardData.IsHeroCard;
    }
}
