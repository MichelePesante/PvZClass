using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulliganController : MonoBehaviour
{
    #region Events
    public Action<List<CardData>, List<CardData>> OnMulliganEnd;
    public Action OnCardChanged;
    #endregion

    [SerializeField]
    private Button changeButton;
    [SerializeField]
    private Button continueButton;

    private List<CardViewController> cardsOnScreen;
    private List<CardData> cardsToDisplay;
    private List<CardData> cardNotSelected;
    private PlayerView player;
    int cardsToDisplayLastIndex;
    bool changeDone;

    public void Init(PlayerView _playerView)
    {
        player = _playerView;
        changeButton.onClick.AddListener(ChangeButtonClicked);
        changeButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(ContinueButtonClicked);
        continueButton.gameObject.SetActive(false);
        cardsOnScreen = GetComponentsInChildren<CardViewController>().ToList();
        cardsToDisplayLastIndex = 0;
        // HACK: Auto select cards init
        DeckData newDeckData = new DeckData();
        DeckData playersDeckData = player.PlayerDeck.Data;
        DeckController.Draw(ref newDeckData, ref playersDeckData, 8);
        cardsToDisplay = newDeckData.Cards;
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            cardsOnScreen[i].Setup(cardsToDisplay[i], player.Data);
            cardsToDisplayLastIndex = i;
        }
        cardNotSelected = new List<CardData>();
        for (int i = cardsOnScreen.Count; i < cardsToDisplay.Count; i++)
        {
            cardNotSelected.Add(cardsToDisplay[i]);
        }
    }

    /// <summary>
    /// Funzione che si occupa dell'evento OnCardClicked
    /// </summary>
    private List<CardViewController> cardsToChange = new List<CardViewController>();


    public void CardCliked(CardViewController _card)
    {
        if (changeDone)
            return;

        if (cardsToChange.Contains(_card))
            cardsToChange.Remove(_card);
        else
            cardsToChange.Add(_card);
    }

    private void HandlerMulliganEnd() {
        changeButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
    }

    #region API

    /// <summary>
    /// Return mulligan owner.
    /// </summary>
    /// <returns></returns>
    public PlayerView GetPlayer() {
        return this.player;
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
                    cardNotSelected.Add(cardsToChange[j].Data);
                    cardsToDisplayLastIndex++;
                    cardsOnScreen[i].Data = cardsToDisplay[cardsToDisplayLastIndex];
                    cardNotSelected.Remove(cardsToDisplay[cardsToDisplayLastIndex]);
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
        foreach (CardViewController card in cardsOnScreen)
        {
            choosenCards.Add(card.Data);
        }
        continueButton.gameObject.SetActive(false);
        HandlerMulliganEnd();
        if (OnMulliganEnd != null)
            OnMulliganEnd(choosenCards, cardNotSelected);
    }

#endregion


}
