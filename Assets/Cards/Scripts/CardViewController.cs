﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using StateMachine.Card;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDetecter
{

    [Header("Data References")]
    [SerializeField]
    private CardData cardDataPrefab;

    [Header("UI References")]
    //public Text Name;
    public ValueDisplayController Attack;
    public ValueDisplayController Life;
    public ValueDisplayController Cost;
    public Image Image;
    public Image Frame;
    public Image HighlightPanel;
    public Image LowlightPanel;

    private Color HeroColor = Color.white;
    private Color StandardColor = Color.white;

    private CardSMController cardSM;

    public enum HighlightState { NoHighlight, Highlighted, Lowlight }
    public HighlightState Higlight { get; internal set; }

    internal void SetHiglight(HighlightState _Higlight)
    {
        Higlight = _Higlight;
        onDataChanged(Data);
    }

    public Action<CardViewController> OnCardPointerDown;
    public Action<CardViewController> OnCardPointerUp;
    private CardData _data;
    public CardData Data
    {
        get { return _data; }
        set
        {
            _data = value;
            onDataChanged(_data);
        }
    }
    private PlayerView playerOwner;

    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    private Camera cam;
    private RectTransform rectTransform;

    public void Setup()
    {
        Data.CurrentState = CardState.Inactive;
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
        eventSystem = FindObjectOfType<EventSystem>();
        if (!eventSystem)
        {
            Debug.LogError(name + " has not found an event system!");
        }
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (!graphicRaycaster)
        {
            Debug.LogError(name + " has not found a graphic raycaster!");
        }
        // Data = Instantiate(cardDataPrefab);
        Interactable(true);
        detectedObjects = new List<IDetectable>();
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }

    public void Setup(CardData _data, PlayerView _player)
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
        eventSystem = FindObjectOfType<EventSystem>();
        if (!eventSystem)
        {
            Debug.LogError(name + " has not found an event system!");
        }
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (!graphicRaycaster)
        {
            Debug.LogError(name + " has not found a graphic raycaster!");
        }
        Data = _data;
        Data.CurrentState = CardState.Inactive;
        playerOwner = _player;
        Interactable(true);
        detectedObjects = new List<IDetectable>();
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }

    private void onDataChanged(CardData _data)
    {
        //Name.text = _data.NameToShow;
        Attack.SetValue(_data.Attack.ToString());
        Life.SetValue(_data.Life.ToString());
        Cost.SetValue(_data.Cost.ToString());
        Image.sprite = _data.CardImage;
        if (_data.IsHeroCard)
        {
            Frame.color = HeroColor;
        }
        else
        {
            Frame.color = StandardColor;
        }
        switch (Higlight)
        {
            case HighlightState.NoHighlight:
                LowlightPanel.enabled = false;
                HighlightPanel.enabled = false;
                break;
            case HighlightState.Highlighted:
                LowlightPanel.enabled = false;
                HighlightPanel.enabled = true;
                break;
            case HighlightState.Lowlight:
                LowlightPanel.enabled = true;
                HighlightPanel.enabled = false;
                break;
            default:
                break;
        }
        _data.OnDataChanged += onDataChanged;
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
        if (interactable)
        {
            if (OnCardPointerUp != null)
            {
                OnCardPointerUp(this);
                detectedObjects.Clear();
            }
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
    public PlayerView GetPlayerOwner()
    {
        return playerOwner;
    }

    private List<IDetectable> detectedObjects = new List<IDetectable>();
    private PointerEventData pointerEventData;
    public void Detect()
    {
        pointerEventData = new PointerEventData(eventSystem);

        pointerEventData.position = rectTransform.position;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count == 0)
        {
            for (int i = 0; i < detectedObjects.Count; i++)
            {
                detectedObjects[i].OnExit(this);
                detectedObjects.RemoveAt(i);
                i--;
            }
        }

        List<IDetectable> newDetectedObjects = new List<IDetectable>();
        foreach (RaycastResult result in results)
        {
            IDetectable _detectedObj = result.gameObject.GetComponent<IDetectable>();
            if (_detectedObj == null)
                continue;
            newDetectedObjects.Add(_detectedObj);
        }

        if (newDetectedObjects.Count == 0)
        {
            for (int i = 0; i < detectedObjects.Count; i++)
            {
                detectedObjects[i].OnExit(this);
                detectedObjects.RemoveAt(i);
                i--;
            }
        }
        else
        {
            foreach (IDetectable detectedObject in newDetectedObjects)
            {
                if (!detectedObjects.Contains(detectedObject))
                {
                    detectedObject.OnEnter(this);
                    detectedObjects.Add(detectedObject);
                }

                for (int i = 0; i < detectedObjects.Count; i++)
                {
                    if (detectedObjects[i] != detectedObject)
                    {
                        detectedObjects[i].OnExit(this);
                        detectedObjects.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    public List<IDetectable> GetDetectedObjects()
    {
        return detectedObjects;
    }

}