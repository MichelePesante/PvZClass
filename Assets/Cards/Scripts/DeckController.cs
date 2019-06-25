using PvZ.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DeckController
{
    public static DeckData CreateDeck(PlayerData _playerData, int _cardsNumber = 1)
    {
        List<CardData> allCards = Resources.LoadAll<CardData>("CardsScriptables")
            .Where(c => c.CardFaction == _playerData.Faction).ToList();
        DeckData newDeck = new DeckData();

        for (int i = 0; i < _cardsNumber; i++)
        {
            newDeck.Cards.Add(GameObject.Instantiate(allCards.GetRandomElement()));
        }
        return newDeck;
    }

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

    private static void AddCard(ref DeckData _deck, ref CardData _cardToAdd)
    {
        _deck.Cards.Add(_cardToAdd);
        _cardToAdd.CurrentDeck = _deck;
    }

    private static void RemoveCard(ref DeckData _deck, ref CardData cardToRemove)
    {
        _deck.Cards.Remove(cardToRemove);
        cardToRemove.CurrentDeck = null;
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

    public static DeckViewController ResetCardsState(DeckViewController _deck)
    {
        foreach (CardData card in _deck.Data.Cards)
        {
            card.CurrentState = CardState.Idle;
        }
        return _deck;
    }

    /// <summary>
    /// Aggiunge cardsToDraw carte dal deck alla hand e la rimuove dal deck, ritorna hand.
    /// </summary>
    public static List<GameplayMovementAction> Draw(ref DeckData _deckToAddTo, ref DeckData _deckToRemoveFrom, int cardsToDraw = 1)
    {
        List<GameplayMovementAction> actions = new List<GameplayMovementAction>();
        for (int i = 0; i < cardsToDraw; i++)
        {
            CardData cardToMove = _deckToRemoveFrom.Cards[0];
            actions.Add(Move(ref _deckToAddTo, ref _deckToRemoveFrom, ref cardToMove));
        }

        return actions;
    }

    public static List<CardData> GetCards(DeckData _deckFrom, int _cardsNumber)
    {
        List<CardData> cardsToReturn = new List<CardData>();
        for (int i = 0; i < _cardsNumber; i++)
        {
            cardsToReturn.Add(_deckFrom.Cards[i]);
        }

        return cardsToReturn;
    }

    public static GameplayMovementAction Move(ref DeckData _deckToAddTo, ref DeckData _deckToRemoveFrom, ref CardData _cardToMove)
    {
        AddCard(ref _deckToAddTo, ref _cardToMove);
        if (_deckToRemoveFrom != null)
            RemoveCard(ref _deckToRemoveFrom, ref _cardToMove);

        return GameplayMovementAction.CreateMovementAction(_cardToMove, _deckToRemoveFrom, _deckToAddTo);
    }
}