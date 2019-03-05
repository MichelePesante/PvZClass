﻿using UnityEngine;
using System.Collections.Generic;

public class HandDeckViewController : DeckViewControllerBase
{
    [SerializeField] CardViewController cardPrefab;

    [HideInInspector]
    public List<CardViewController> instantiatedCards = new List<CardViewController>();

    public override void LateSetup() {
        foreach (CardData card in Data.Cards) {
            instantiatedCards.Add(Instantiate(cardPrefab, transform));
            instantiatedCards[instantiatedCards.Count - 1].Setup(card, Data.Player);
        }
    }
}