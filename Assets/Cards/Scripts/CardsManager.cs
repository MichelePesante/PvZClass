using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PvZ.Helpers;

public class CardsManager
{

    public DeckController CreateDeck(int _cardsNumber = 1) {

        DeckController newDeck = new DeckController();

        List<CardData> allCards = Resources.LoadAll<CardData>("CardsScriptables").ToList();

        for (int i = 0; i < _cardsNumber; i++) {
            newDeck.AddCard(allCards.GetRandomElement());
        }

        return newDeck;
    }




}
