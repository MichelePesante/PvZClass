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
    private HandDeckViewController p1HandView;
    [SerializeField]
    private HandDeckViewController p2HandView;

    public List<CardViewController> instantiatedCards = new List<CardViewController>();

    public void Init()
    {
        DeckViewControllerBase.OnCardMoved += HandleOnCardMoved;
        DeckViewControllerBase.OnCardsMoved += HandleOnCardsMoved;
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
        if (action.deckDataFrom == null)
        {
            CardViewController instantiatedCard = Instantiate(cardPrefab.gameObject).GetComponent<CardViewController>();
            instantiatedCards.Add(instantiatedCard);
            switch (action.deckDataTo.Player.CurrentType)
            {
                case Player.Type.one:
                    instantiatedCard.transform.parent = p1HandView.transform;
                    instantiatedCard.transform.position = p1HandView.transform.position;                    
                    break;
                case Player.Type.two:
                    instantiatedCard.transform.parent = p2HandView.transform;
                    instantiatedCard.transform.position = p2HandView.transform.position;
                    break;
            }
            instantiatedCard.Setup(action.cardData, action.deckDataTo.Player);
        }
    }

    private void OnDisable()
    {
        DeckViewControllerBase.OnCardMoved -= HandleOnCardMoved;
        DeckViewControllerBase.OnCardsMoved -= HandleOnCardsMoved;
    }
}
