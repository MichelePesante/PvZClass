using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using StateMachine.Card;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDetecter {

    [Header("Data References")]
    [SerializeField]
    private CardData cardDataPrefab;

    public enum State {
        Inactive = -1,
        Idle = 0,
        Playable = 1,
        Unplayable = 2,
        Drag = 3,
        Played = 4,
    }

    public Action<State> OnCurrentStateChanged;

    private State _currentState;
    public State CurrentState {
        get { return _currentState; }
        set {
            _currentState = value;
            if (OnCurrentStateChanged != null) {
                OnCurrentStateChanged(_currentState);
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
    public CardData Data {
        get { return _data; }
        set {
            _data = value;
            if (OnDataChanged != null)
                OnDataChanged(Data);
        }
    }
    private IPlayer playerOwner;
    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    private Camera cam;
    private RectTransform rectTransform;

    public void Setup() {
        CurrentState = State.Inactive;
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
        eventSystem = FindObjectOfType<EventSystem>();
        if (!eventSystem) {
            Debug.LogError(name + " has not found an event system!");
        }
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (!graphicRaycaster) {
            Debug.LogError(name + " has not found a graphic raycaster!");
        }
        Data = Instantiate(cardDataPrefab);
        Interactable(true);
        detectedObjects = new List<IDetectable>();
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }

    public void Setup(CardData _data, IPlayer _player) {
        CurrentState = State.Inactive;
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
        eventSystem = FindObjectOfType<EventSystem>();
        if (!eventSystem) {
            Debug.LogError(name + " has not found an event system!");
        }
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (!graphicRaycaster) {
            Debug.LogError(name + " has not found a graphic raycaster!");
        }
        Data = Instantiate(_data);
        playerOwner = _player;
        Interactable(true);
        detectedObjects = new List<IDetectable>();
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }

    public void ResetOriginalLife() {
        _data.ResetOriginalLife();
    }

    public void UpdateLife(int _newlife) {
        Data.Life = _newlife;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (interactable && OnCardPointerDown != null) {
            OnCardPointerDown(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (interactable) {
            if (OnCardPointerUp != null) {
                OnCardPointerUp(this);
                detectedObjects.Clear();
            }
        }
    }

    bool interactable;
    public void Interactable(bool _intertactable) {
        interactable = _intertactable;
    }

    /// <summary>
    /// Funzione che ritorna il player
    /// </summary>
    public IPlayer GetPlayerOwner() {
        return playerOwner;
    }

    private List<IDetectable> detectedObjects = new List<IDetectable>();
    private PointerEventData pointerEventData;
    public void Detect() {
        pointerEventData = new PointerEventData(eventSystem);

        pointerEventData.position = rectTransform.position;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results) {
            IDetectable _detectedObj = result.gameObject.GetComponent<IDetectable>();
            if (_detectedObj == null)
                continue;

            if (!detectedObjects.Contains(_detectedObj)) {
                _detectedObj.OnEnter(this);
                detectedObjects.Add(_detectedObj);
            }

            for (int i = 0; i < detectedObjects.Count; i++) {
                if (detectedObjects[i] != _detectedObj) {
                    detectedObjects[i].OnExit(this);
                    detectedObjects.RemoveAt(i);
                    i--;
                }
            }
        }


    }

    public List<IDetectable> GetDetectedObjects() {
        return detectedObjects;
    }
}