using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController {

    [SerializeField]
    private List<CardData> Cards;

    public void Shuffle()
    {
        for (int i = 0; i < (Cards.Count * 2); i++) // Mischia 20 volte
        {
            int random = Random.Range(0, Cards.Count);
            CardData tempCard = null;
            tempCard = Cards[i];
            Cards[i] = Cards[random];
            Cards[random] = tempCard;
        }
    }

    public void AddCard(CardData cardToAdd)
    {
        Cards.Add(cardToAdd);
    }

    public void RemoveCard(CardData cardToRemove)
    {
        Cards.Remove(cardToRemove);
    }
}
