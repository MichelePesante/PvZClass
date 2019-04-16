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
 
    }

    private void HandleOnCardMoved(GameplayAction action)
    {
        //TODO controllare il deck a cui va aggiunto
        if (action.deckDataFrom == null)
        {
            CardViewController instantiatedCard = Instantiate(cardPrefab);
            instantiatedCards.Add(instantiatedCard);
            instantiatedCard.Setup(action.cardData, action.deckDataTo.Player);
            switch (action.deckDataTo.Player.CurrentType)
            {
                case Player.Type.one:
                    instantiatedCard.transform.position = p1HandView.transform.position;                    
                    break;
                case Player.Type.two:
                    instantiatedCard.transform.position = p2HandView.transform.position;
                    break;
            }
        }
    }

    private void OnDisable()
    {
        DeckViewControllerBase.OnCardMoved -= HandleOnCardMoved;
        DeckViewControllerBase.OnCardsMoved -= HandleOnCardsMoved;
    }
}
