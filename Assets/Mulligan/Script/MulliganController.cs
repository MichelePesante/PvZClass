using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulliganController : MonoBehaviour
{
    #region Events
    public Action<List<CardData>> OnMulliganEnd;
    #endregion

    public List<CardData> sus;

    [SerializeField]
    private Button changeButton;
    [SerializeField]
    private Button continueButton;

    private List<CardController> cardsOnScreen;
    private List<CardData> cardsToDisplay;
    int cardsToDisplayLastIndex;
    bool changeDone;

    private void Start()
    {
        Init(sus);
    }

    public void Init(List<CardData> _cardsToDisplay)
    {
        changeButton.onClick.AddListener(ChangeButtonClicked);
        changeButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(ContinueButtonClicked);
        continueButton.gameObject.SetActive(false);

        cardsOnScreen = GetComponentsInChildren<CardController>().ToList();

        cardsToDisplayLastIndex = 0;
        cardsToDisplay = _cardsToDisplay;
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            cardsOnScreen[i].Setup(cardsToDisplay[i]);
            cardsOnScreen[i].OnCardClicked += CardCliked;
            cardsToDisplayLastIndex = i;
        }
    }

    /// <summary>
    /// Funzione che si occupa dell'evento OnCardClicked
    /// </summary>
    private List<CardController> cardsToChange = new List<CardController>();
    private void CardCliked(CardController _card)
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
    private void ChangeButtonClicked()
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
                    cardsOnScreen[i].Setup(cardsToDisplay[cardsToDisplayLastIndex]);
                }
            }

            cardsOnScreen[i].Interactable(false);
        }

        changeButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        cardsToChange.Clear();
    }

    /// <summary>
    /// Funzione che si occupa del click del pulsante Continue
    /// </summary>
    private void ContinueButtonClicked()
    {
        List<CardData> choosenCards = new List<CardData>();
        foreach (CardController card in cardsOnScreen)
        {
            choosenCards.Add(card.GetCardData());
        }

        if (OnMulliganEnd != null)
            OnMulliganEnd(choosenCards);
    }

    private void OnDisable()
    {
        for (int i = 0; i < cardsOnScreen.Count; i++)
        {
            cardsOnScreen[i].OnCardClicked -= CardCliked;
        }

        changeButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
    }
}
