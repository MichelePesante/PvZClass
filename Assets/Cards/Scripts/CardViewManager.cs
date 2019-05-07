using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewManager : MonoBehaviour
{
    [SerializeField]
    private CardViewController cardPrefab;
    [SerializeField]
    private DeckViewController p1DeckView;
    [SerializeField]
    private DeckViewController p2DeckView;
    [SerializeField]
    private DeckViewController p1HandView;
    [SerializeField]
    private DeckViewController p2HandView;

    public List<CardViewController> instantiatedCards = new List<CardViewController>();

    public void Init()
    {
        DeckViewController.OnCardMoved += HandleOnCardMoved;
        DeckViewController.OnCardsMoved += HandleOnCardsMoved;
    }

    private void HandleOnCardsMoved(List<GameplayAction> actions)
    {
        foreach (GameplayAction action in actions)
        {
            HandleOnCardMoved(action);
        }
    }

    public DeckViewController GetHandDeck(PlayerData.Type currentType) {
        switch (currentType) {
            case PlayerData.Type.one:
                return p1HandView;
            case PlayerData.Type.two:
                return p2HandView;
        }
        return null;
    }

    public DeckViewController GetPlayerDeck(PlayerData.Type currentType) {
        switch (currentType) {
            case PlayerData.Type.one:
                return p1DeckView;
            case PlayerData.Type.two:
                return p2DeckView;
        }
        return null;
    }

    private void HandleOnCardMoved(GameplayAction action)
    {
        //TODO controllare il deck a cui va aggiunto
        DeckViewController deckFrom = GetDeckViewControllerByDeckData(action.deckDataFrom);
        DeckViewController deckTo = GetDeckViewControllerByDeckData(action.deckDataTo);
        CardData changedCard = action.cardData;

        //Se non c'è un deck da cui la carta deriva la istanzio a prescindere
        if (deckFrom == null)
        {
            switch (deckTo.CurrentViewType) {
                case DeckViewController.ViewType.covered:
                    break;
                case DeckViewController.ViewType.visible:
                    AddCardToDeck(deckTo, changedCard);
                    break;
                case DeckViewController.ViewType.none:
                    break;
            }
            return;
        }

        //Se non c'è un deck destinatario la distruggo a prescindere
        if (deckTo == null || deckTo.CurrentViewType == DeckViewController.ViewType.none)
        {
            foreach (CardViewController card in instantiatedCards)
            {
                if (changedCard.CompareIndex(card.Data.CardIndex))
                {
                    Destroy(changedCard);
                    return;
                }
            }
            return;
        }

        //Se c'è da spostare una carta da un deck ad un altro
        for (int i = 0; i < instantiatedCards.Count; i++)
        {
            if (changedCard.CompareIndex(instantiatedCards[i].Data.CardIndex))
            {
                //Se la carta esiste gìà la sposto
                instantiatedCards[i].transform.position = deckTo.transform.position;
                return;
            }
        }

        //Se la carta non esisteva ancora la istanzio
        AddCardToDeck(deckTo, changedCard);
    }

    public DeckViewController GetDeckViewControllerByDeckData(DeckData _data)
    {
        if (_data == null)
            return null;
        if (p1DeckView.Data == _data)
            return p1DeckView;
        if (p2DeckView.Data == _data)
            return p2DeckView;
        if (p1HandView.Data == _data)
            return p1HandView;
        if (p2HandView.Data == _data)
            return p2HandView;

        return null;
    }

    private void AddCardToDeck(DeckViewController _deckToAdd, CardData _cardData)
    {
        CardViewController instantiatedCard = Instantiate(cardPrefab);
        instantiatedCards.Add(instantiatedCard);
        instantiatedCard.transform.parent = _deckToAdd.transform;
        instantiatedCard.transform.position = _deckToAdd.transform.position;
        instantiatedCard.Setup(_cardData, _deckToAdd.Data.Player);
    }

    private void OnDisable()
    {
        DeckViewController.OnCardMoved -= HandleOnCardMoved;
        DeckViewController.OnCardsMoved -= HandleOnCardsMoved;
    }
}
