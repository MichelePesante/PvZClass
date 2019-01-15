using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardsManager
{

    public DeckController CreateDeck(int _cardsNumber = 1) {

        DeckController newDeck = new DeckController();

        List<CardData> allCards = Resources.LoadAll<CardData>("Cards/CardsScriptables").ToList();

        for (int i = 0; i < _cardsNumber; i++) {
            newDeck.AddCard(allCards[Random.Range(0, allCards.Count - 1)]);
        }

        return newDeck;
    }


}
