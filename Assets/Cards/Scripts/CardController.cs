using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using StateMachine.Card;

public class CardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [Header("Data References")]
    [SerializeField]
    private CardData cardDataPrefab;

    public enum State {
        Idle = 0,
        Playable = 1,
        Unplayable = 2,
        Drag = 3,
        Played = 4
    }

    public Action<State> OnCurrentStateChanged;

    private State currentState;
    public State CurrentState {
        get { return currentState; }
        set { currentState = value;
            if (OnCurrentStateChanged != null) {
                OnCurrentStateChanged(currentState);
            }
        }
    }

    private CardSMController cardSM;

    internal void SetHiglight(CardData.Highlight _Higlight) {
        Data.Higlight = _Higlight;
        if (OnDataChanged != null)
            OnDataChanged(Data);
    }

    public Action<CardData> OnDataChanged;
    public Action<CardController> OnCardPointerDown;
    public Action<CardController> OnCardPointerUp;

    private CardData _data;
    public CardData Data
    {
        get { return _data; }
        private set
        {
            _data = value;
            if (OnDataChanged != null)
                OnDataChanged(Data);
        }
    }
    private IPlayer playerOwner;

    public void Setup()
    {
        Data = Instantiate(cardDataPrefab);
        Interactable(true);
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }
    public void Setup(CardData _data, IPlayer _player)
    {
        Data = Instantiate(_data);
        playerOwner = _player;
        Interactable(true);
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }

    public CardData GetCardData()
    {
        return Data;
    }

    public void ResetOriginalLife()
    {
        _data.ResetOriginalLife();
    }

    public void UpdateLife(int _newlife)
    {
        Data.Life = _newlife;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (interactable && OnCardPointerDown != null)
        {
            OnCardPointerDown(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (interactable && OnCardPointerUp != null)
        {
            OnCardPointerUp(this);
        }
    }

    bool interactable;
    public void Interactable(bool _intertactable)
    {
        interactable = _intertactable;
    }

    /// <summary>
    /// Funzione che ritorna il player
    /// </summary>
    public IPlayer GetPlayerOwner()
    {
        return playerOwner;
    }
}