using UnityEngine;
using System.Collections.Generic;

public class HandUI : DeckControllerUI
{

    [SerializeField] CardController cardPrefab;

    [HideInInspector]
    public List<CardController> instantiatedCards = new List<CardController>();

    public override void LateSetup() {
        foreach (CardData card in Data.Cards) {
            instantiatedCards.Add(Instantiate(cardPrefab, transform));
            instantiatedCards[instantiatedCards.Count - 1].Setup(card, Data.Player);
        }
    }
}