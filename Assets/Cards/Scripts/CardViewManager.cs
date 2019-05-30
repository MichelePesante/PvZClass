using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewManager : MonoBehaviour
{
    private static CardViewManager instance;

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
    [SerializeField]
    private DeckViewController trashDeckView;
    DeckViewController[] boardDeckViews;

    List<CardViewController> instantiatedCards = new List<CardViewController>();

    public void Init()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DeckViewController.OnCardMoved += HandleOnCardMoved;
        DeckViewController.OnCardsMoved += HandleOnCardsMoved;
    }

    public void Setup(DeckViewController[] _boardDeckViews)
    {
        boardDeckViews = _boardDeckViews;
    }

    private void HandleOnCardsMoved(List<GameplayAction> actions)
    {
        foreach (GameplayAction action in actions)
        {
            HandleOnCardMoved(action);
        }
    }

    private void HandleOnCardMoved(GameplayAction action)
    {
        //TODO controllare il deck a cui va aggiunto
        DeckViewController deckFrom = GetDeckViewControllerByDeckData(action.deckDataFrom);
        DeckViewController deckTo = GetDeckViewControllerByDeckData(action.deckDataTo);
        CardData changedCardData = action.cardData;

        //Se non c'è un deck da cui la carta deriva la istanzio a prescindere
        if (deckFrom == null)
        {
            switch (deckTo.CurrentViewType)
            {
                case DeckViewController.ViewType.covered:
                    AddCardToDeck(deckTo, changedCardData);
                    break;
                case DeckViewController.ViewType.visible:
                    AddCardToDeck(deckTo, changedCardData);
                    break;
                case DeckViewController.ViewType.none:
                    break;
            }
            return;
        }

        //Se non c'è un deck destinatario o il deck è non visibile la distruggo a prescindere
        if (deckTo == null || deckTo.CurrentViewType == DeckViewController.ViewType.none)
        {
            for (int i = 0; i < instantiatedCards.Count; i++)
            {
                if (changedCardData.CompareIndex(instantiatedCards[i].Data.CardIndex))
                {
                    Destroy(instantiatedCards[i].gameObject);
                    instantiatedCards.RemoveAt(i);
                    return;
                }
            }
            return;
        }

        //Se c'è da spostare una carta da un deck ad un altro
        for (int i = 0; i < instantiatedCards.Count; i++)
        {
            if (changedCardData.CompareIndex(instantiatedCards[i].Data.CardIndex))
            {
                //Se la carta esiste gìà la sposto
                instantiatedCards[i].transform.position = deckTo.transform.position;
                instantiatedCards[i].transform.parent = deckTo.transform;
                return;
            }
        }

        //Se la carta non esisteva ancora la istanzio
        AddCardToDeck(deckTo, changedCardData);
    }

    private void AddCardToDeck(DeckViewController _deckToAdd, CardData _cardData)
    {
        CardViewController instantiatedCard = Instantiate(cardPrefab);
        instantiatedCards.Add(instantiatedCard);
        instantiatedCard.transform.parent = _deckToAdd.transform;
        instantiatedCard.transform.position = _deckToAdd.transform.position;
        instantiatedCard.Setup(_cardData, GameplaySceneManager.GetPlayer(_deckToAdd.Data.Player.CurrentType));
    }

    #region Getter
    public static DeckViewController GetHandDeck(PlayerData.Type currentType)
    {
        switch (currentType)
        {
            case PlayerData.Type.one:
                return instance.p1HandView;
            case PlayerData.Type.two:
                return instance.p2HandView;
        }
        return null;
    }

    public static DeckViewController GetPlayerDeck(PlayerData.Type currentType)
    {
        switch (currentType)
        {
            case PlayerData.Type.one:
                return instance.p1DeckView;
            case PlayerData.Type.two:
                return instance.p2DeckView;
        }
        return null;
    }

    public static DeckViewController GetDeckViewControllerByDeckData(DeckData _data)
    {
        if (_data == null)
            return null;
        if (instance.trashDeckView.Data == _data)
            return instance.trashDeckView;
        if (instance.p1DeckView.Data == _data)
            return instance.p1DeckView;
        if (instance.p2DeckView.Data == _data)
            return instance.p2DeckView;
        if (instance.p1HandView.Data == _data)
            return instance.p1HandView;
        if (instance.p2HandView.Data == _data)
            return instance.p2HandView;

        if (instance.boardDeckViews != null)
        {
            foreach (DeckViewController deckView in instance.boardDeckViews)
            {
                if (deckView.Data == _data)
                    return deckView;
            }
        }

        return null;
    }

    public static DeckViewController GetTrashDeckView()
    {
        return instance.trashDeckView;
    }

    public static CardViewController GetCardViewByCardData(CardData _data)
    {
        foreach (CardViewController card in instance.instantiatedCards)
        {
            if (card.Data.CompareIndex(_data.CardIndex))
            {
                return card;
            }
        }

        return null;
    }
    #endregion

    private void OnDisable()
    {
        DeckViewController.OnCardMoved -= HandleOnCardMoved;
        DeckViewController.OnCardsMoved -= HandleOnCardsMoved;
    }
}
