using UnityEngine;
using System.Collections.Generic;

public class HandDeckViewController : DeckViewControllerBase
{
    [SerializeField] CardViewController cardPrefab;

    public void InstantiateCards()
    {
        foreach (CardData card in Data.Cards)
        {
            instantiatedCards.Add(Instantiate(cardPrefab, transform));
            instantiatedCards[instantiatedCards.Count - 1].Setup(card, Data.Player);
        }
    }

    protected override void OnDataChanged()
    {
        InstantiateCards();//TODO: da controllare il cambiamento avvenutox.
    }
}