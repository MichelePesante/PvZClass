﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulliganController : MonoBehaviour
{
    #region Events
    public Action<List<CardData>> OnMulliganEnd;
    public Action OnCardChanged;
    #endregion

    [SerializeField]
    private Button changeButton;
    [SerializeField]
    private Button continueButton;

    private List<CardController> cardsOnScreen;
    private List<CardData> cardsToDisplay;
    private IPlayer player;
    int cardsToDisplayLastIndex;
    bool changeDone;

    public void Init(DeckController _hand)
    {
        player = _hand.GetPlayerOwner();
        changeButton.onClick.AddListener(ChangeButtonClicked);
        changeButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(ContinueButtonClicked);
        continueButton.gameObject.SetActive(false);
        cardsOnScreen = GetComponentsInChildren<CardController>().ToList();
        cardsToDisplayLastIndex = 0;
        cardsToDisplay = _hand.GetCards();
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            cardsOnScreen[i].Setup(cardsToDisplay[i], player);
            cardsToDisplayLastIndex = i;
        }
    }

    /// <summary>
    /// Funzione che si occupa dell'evento OnCardClicked
    /// </summary>
    private List<CardController> cardsToChange = new List<CardController>();
    public void CardCliked(CardController _card)
    {
        if (changeDone)
            return;

        if (cardsToChange.Contains(_card))
            cardsToChange.Remove(_card);
        else
            cardsToChange.Add(_card);
    }

    /// <summary>
    /// Funzione che si occupa del click del pulsante Change
    /// </summary>
    public void ChangeButtonClicked()
    {
        if (cardsToChange == null)
            return;

        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            for (int j = 0; j < cardsToChange.Count; j++)
            {
                if (cardsOnScreen[i] == cardsToChange[j])
                {
                    cardsToDisplayLastIndex++;
                    cardsOnScreen[i].Setup(cardsToDisplay[cardsToDisplayLastIndex], player);
                }
            }

            cardsOnScreen[i].Interactable(false);
        }

        changeButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        cardsToChange.Clear();
        if (OnCardChanged != null)
            OnCardChanged();
    }

    /// <summary>
    /// Funzione che si occupa del click del pulsante Continue
    /// </summary>
    public void ContinueButtonClicked()
    {
        List<CardData> choosenCards = new List<CardData>();
        foreach (CardController card in cardsOnScreen)
        {
            choosenCards.Add(card.GetCardData());
        }
        continueButton.gameObject.SetActive(false);
        HandlerMulliganEnd();
        if (OnMulliganEnd != null)
            OnMulliganEnd(choosenCards);
    }

    private void HandlerMulliganEnd()
    {
        changeButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
    }
}
