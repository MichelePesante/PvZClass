using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour {

    [SerializeField]
    private List<CardController> Cards;

    public void Shuffle()
    {
        for (int i = 0; i < (Cards.Count * 2); i++) // Mischia 20 volte
        {
            int random = Random.Range(0, Cards.Count);
            CardController tempCard = null;
            tempCard = Cards[i];
            Cards[i] = Cards[random];
            Cards[random] = tempCard;
        }
    }

    public void AddCard(CardController cardToAdd)
    {
        Cards.Add(cardToAdd);
    }

    public void RemoveCard(CardController cardToRemove)
    {
        Cards.Remove(cardToRemove);
    }
}
