using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckController {

    public static DeckData Shuffle(DeckData _deckToShuffle)
    {
        for (int i = 0; i < (_deckToShuffle.Cards.Count * 2); i++) // Mischia 20 volte
        {
            int random = Random.Range(0, _deckToShuffle.Cards.Count);
            CardData tempCard = null;
            tempCard = _deckToShuffle.Cards[i];
            _deckToShuffle.Cards[i] = _deckToShuffle.Cards[random];
            _deckToShuffle.Cards[random] = tempCard;
        }
        return _deckToShuffle;
    }

    public static DeckData AddCard(DeckData _deck, CardData _cardToAdd)
    {
        _deck.Cards.Add(_cardToAdd);
        return _deck;
    }

    /// <summary>
    /// Ritorna la carta a seconda dell'index senza rimuoverla dal deck
    /// </summary>
    /// <param name="indexCard"></param>
    /// <returns></returns>
    public static CardData GetCard(DeckData _deck, int indexCard = 0)
    {
        return _deck.Cards[indexCard];
    }

    public static DeckData RemoveCard(DeckData _deck ,CardData cardToRemove)
    {
        _deck.Cards.Remove(cardToRemove);
        return _deck;
    }

    /// <summary>
    /// Aggiunge cardsToDraw carte dal deck alla hand e la rimuove dal deck
    /// </summary>
    public static void Draw(DeckData _deckToAddTo, DeckData _deckToRemoveFrom, int cardsToDraw = 1)
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            AddCard(_deckToAddTo, _deckToRemoveFrom.Cards[0]);
            RemoveCard(_deckToRemoveFrom, _deckToRemoveFrom.Cards[0]);
        }
    }
}
