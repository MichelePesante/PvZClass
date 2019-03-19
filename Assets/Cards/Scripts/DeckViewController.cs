using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using PvZ.Helpers;

public class DeckViewController : DeckViewControllerBase
{
    public DeckData CreateDeck(int _cardsNumber = 1)
    {
        List<CardData> allCards = Resources.LoadAll<CardData>("CardsScriptables").ToList();
        DeckData newDeck = new DeckData();

        for (int i = 0; i < _cardsNumber; i++)
        {            
            newDeck.Cards.Add(Instantiate(allCards.GetRandomElement()));
        }
        return newDeck;
    }
}
