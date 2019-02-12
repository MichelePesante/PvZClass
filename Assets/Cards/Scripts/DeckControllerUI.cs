using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckControllerUI : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;
    [SerializeField] Player playerReference;
    private DeckController deckController;

    public void Setup(DeckController _deckController)
    {
        deckController = _deckController;

        foreach (CardData card in deckController.GetCards())
        {
            CardController instCard = Instantiate(cardPrefab, transform);
            instCard.Setup(card, playerReference);
        }
    }
}