using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckViewControllerBase : MonoBehaviour
{
    #region Delegates
    public Action<DeckData> OnCardsAdded;
    #endregion

    DeckData _data;
    public DeckData Data
    {
        get { return _data; }
        protected set
        {
            _data = value;
        }
    }

    [HideInInspector]
    public List<CardViewController> instantiatedCards = new List<CardViewController>();

    public virtual void AddViews(List<CardData> _cardsToAdd)
    {
        for (int i = 0; i < _cardsToAdd.Count; i++)
        {
            AddView(_cardsToAdd[i]);
        }
    }

    public virtual void RemoveViews(List<CardData> _cardsToRemove)
    {
        for (int i = 0; i < _cardsToRemove.Count; i++)
        {
            RemoveView(_cardsToRemove[i]);
        }
    }

    public virtual void AddView(CardData _cardToAdd)
    {

    }

    public virtual void RemoveView(CardData _cardToRemove)
    {

    }

    protected virtual void LateSetup()
    {

    }

    #region API
    /// <summary>
    /// Funzione che aggiunge la carta passata come parametro e se la callback è null la istanzia, altrimenti ritorna i dati aggiornati alla callback
    /// </summary>
    /// <param name="_cardToAdd"></param>
    /// <param name="_DataUpdatedCallback"></param>
    public void AddCard(CardData _cardToAdd, Action<List<CardData>> _DataUpdatedCallback = null)
    {
        Data = DeckController.AddCard(Data, _cardToAdd);

        if (_DataUpdatedCallback != null)
            _DataUpdatedCallback(Data.Cards);
        else
            AddView(_cardToAdd);
    }

    /// <summary>
    /// Funzione che rimuove la carta passata come parametro e se la callback è null la istanzia, altrimenti ritorna i dati aggiornati alla callback
    /// </summary>
    /// <param name="_cardToRemove"></param>
    /// <param name="_DataUpdatedCallback"></param>
    public void RemoveCard(CardData _cardToRemove, Action<List<CardData>> _DataUpdatedCallback = null)
    {
        Data = DeckController.RemoveCard(Data, _cardToRemove);

        if (_DataUpdatedCallback != null)
            _DataUpdatedCallback(Data.Cards);
        else
            RemoveView(_cardToRemove);
    }

    /// <summary>
    /// Funzione che rimuove il numero di carte passato come parametro dal deckPassato e se la callback è null la istanzia, altrimenti ritorna i dati aggiornati alla callback
    /// </summary>
    /// <param name="deckToDrawFrom"></param>
    /// <param name="_cardsToDraw"></param>
    /// <param name="_DataUpdatedCallback"></param>
    public void Draw(DeckViewControllerBase deckToDrawFrom, int _cardsToDraw, Action<List<CardData>> _DataUpdatedCallback = null)
    {
        Data = DeckController.Draw(Data, deckToDrawFrom.Data, _cardsToDraw);

        if (_DataUpdatedCallback != null)
            _DataUpdatedCallback(Data.Cards);
        else
        {

        }
    }

    public DeckViewControllerBase Setup(DeckData _deck)
    {
        if (_deck == null)
            return null;

        Data = _deck;

        LateSetup();

        return this;
    }

    public void Shuffle()
    {
        Data = DeckController.Shuffle(Data);
    }

    #endregion

}