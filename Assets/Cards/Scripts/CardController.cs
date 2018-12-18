using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay))]
public class CardController : MonoBehaviour {

    public CardData cardData;

    [HideInInspector]
    public string CardName;
    [HideInInspector]
    public Sprite CardImage;
    [HideInInspector]
    public int Cost, Attack, Life;
    [HideInInspector]
    public bool IsHeroCard;
    [HideInInspector]
    public CardData.Rarity Rarity;

    CardDisplay myDisplay;

    private void Awake()
    {
        myDisplay = GetComponent<CardDisplay>();
        SetUp();
    }

    public void SetUp()
    {
        CardName = cardData.CardName;
        CardImage = cardData.CardImage;
        Rarity = cardData.CardRarity;
        Cost = cardData.Cost;
        Attack = cardData.Attack;
        Life = cardData.Life;
        IsHeroCard = cardData.IsHeroCard;
        myDisplay.SetUp(this);
    }
}
