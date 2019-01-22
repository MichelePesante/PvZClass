using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController {

    [SerializeField]
    private List<CardData> Cards = new List<CardData>();

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

    /// <summary>
    /// Ritorna la carta a seconda dell'index senza rimuoverla dal deck
    /// </summary>
    /// <param name="indexCard"></param>
    /// <returns></returns>
    public CardData GetCard(int indexCard = 0)
    {
        return Cards[indexCard];
    }

    public void RemoveCard(CardData cardToRemove)
    {
        Cards.Remove(cardToRemove);
    }

    /// <summary>
    /// Aggiunge cardsToDraw carte dal deck alla hand e la rimuove dal deck
    /// </summary>
    public void Draw(DeckController hand, int cardsToDraw = 1)
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            hand.AddCard(Cards[0]);
            Cards.Remove(Cards[0]);
        }
    }
}
