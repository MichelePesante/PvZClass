using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PvZ.Helpers;

public class CardsManager
{

    public DeckData CreateDeck(int _cardsNumber = 1) {

        List<CardData> allCards = Resources.LoadAll<CardData>("CardsScriptables").ToList();
        DeckData newDeck = new DeckData();

        for (int i = 0; i < _cardsNumber; i++) {
            newDeck.Cards.Add(allCards.GetRandomElement());
        }
        return newDeck;
    }




}
