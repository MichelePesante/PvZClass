using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckViewController : MonoBehaviour
{
    #region Delegates
    public static Action<List<GameplayAction>> OnCardsMoved;
    public static Action<GameplayAction> OnCardMoved;
    #endregion

    DeckData _data;
    public DeckData Data
    {
        get { return _data; }
        set
        {
            _data = value;
        }
    }

    public enum ViewType { covered, visible, none }
    public ViewType CurrentViewType;

    protected void LateSetup()
    {

    }

    #region API

    /// <summary>
    /// Funzione che rimuove il numero di carte passato come parametro dal deckPassato e se la callback è null la istanzia, altrimenti ritorna i dati aggiornati alla callback
    /// </summary>
    /// <param name="deckToDrawFrom"></param>
    /// <param name="_cardsToDraw"></param>
    /// <param name="_DataUpdatedCallback"></param>
    public void Draw(ref DeckData deckToDrawFrom, int _cardsToDraw, Action<List<CardData>> _DataUpdatedCallback = null)
    {
        DeckData deckFrom = Data;
        DeckData deckTo = deckToDrawFrom;
        List<GameplayAction> actions = DeckController.Draw(ref deckFrom, ref deckTo, _cardsToDraw);

        if (_DataUpdatedCallback != null)
            _DataUpdatedCallback(Data.Cards);
        else if (OnCardsMoved != null)
            OnCardsMoved(actions);
    }

    /// <summary>
    /// Sposta card da un deck all'altro e scatena evento pubblico con action se <paramref name="_DataUpdatedCallback"/> è null.
    /// </summary>
    /// <param name="_deckToMove"></param>
    /// <param name="_cardToMove"></param>
    /// <param name="_DataUpdatedCallback"></param>
    public void DoMove(ref DeckData _deckToMove, ref CardData _cardToMove, Action<List<CardData>> _DataUpdatedCallback = null)
    {
        DeckData deckFrom = Data;
        DeckData deckTo = _deckToMove;
        GameplayAction action = DeckController.Move(ref deckFrom, ref deckTo, ref _cardToMove);

        if (_DataUpdatedCallback != null)
            _DataUpdatedCallback(Data.Cards);
        else if (OnCardMoved != null)
            OnCardMoved(action);
    }

    /// <summary>
    /// Sposta cards da un deck all'altro e scatena evento pubblico con action se <paramref name="_DataUpdatedCallback"/> è null.
    /// </summary>
    /// <param name="_deckToMove"></param>
    /// <param name="_cardToMove"></param>
    /// <param name="_DataUpdatedCallback"></param>
    public void DoMoves(ref DeckData _deckToMove, ref List<CardData> _cardToMove, Action<List<CardData>> _DataUpdatedCallback = null)
    {
        List<GameplayAction> actions = new List<GameplayAction>();
        for (int i = 0; i < _cardToMove.Count; i++)
        {
            DeckData deckFrom = Data;
            DeckData deckTo = _deckToMove;
            CardData cardToMove = _cardToMove[i];
            GameplayAction action = DeckController.Move(ref deckFrom, ref deckTo, ref cardToMove);
            actions.Add(action);
        }
        if (_DataUpdatedCallback != null)
            _DataUpdatedCallback(Data.Cards);
        else if (OnCardsMoved != null)
            OnCardsMoved(actions);
    }

    public DeckViewController Setup(DeckData _deck)
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