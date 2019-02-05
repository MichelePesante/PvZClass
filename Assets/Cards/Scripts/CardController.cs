using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using StateMachine.Card;

public class CardController : MonoBehaviour, IPointerDownHandler
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
        private set { currentState = value;
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
    public Action<CardController> OnCardClicked;

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

    public void Setup()
    {
        Data = Instantiate(cardDataPrefab);
        Interactable(true);
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }
    public void Setup(CardData _data)
    {
        Data = Instantiate(_data);
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

        if (interactable && OnCardClicked != null) {
            OnCardClicked(this);
        }
    }

    bool interactable;
    public void Interactable(bool _intertactable)
    {
        interactable = _intertactable;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            CurrentState = State.Idle;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            CurrentState = State.Playable;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            CurrentState = State.Unplayable;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            CurrentState = State.Drag;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            CurrentState = State.Played;
        }
    }
}