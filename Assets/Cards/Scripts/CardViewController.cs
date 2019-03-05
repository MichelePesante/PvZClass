using System.Collections;
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

    public enum State
    {
        Inactive = -1,
        Idle = 0,
        Playable = 1,
        Unplayable = 2,
        Drag = 3,
        Played = 4,
    }

    public Action<State> OnCurrentStateChanged;

    private State _currentState;
    public State CurrentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            if (OnCurrentStateChanged != null)
            {
                OnCurrentStateChanged(_currentState);
            }
        }
    }

    private CardSMController cardSM;

    internal void SetHiglight(CardData.Highlight _Higlight)
    {
        Data.Higlight = _Higlight;
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
    private IPlayer playerOwner;

    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    private Camera cam;
    private RectTransform rectTransform;

    public void Setup()
    {
        CurrentState = State.Inactive;
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
        Data = Instantiate(cardDataPrefab);
        Interactable(true);
        detectedObjects = new List<IDetectable>();
        cardSM = GetComponent<CardSMController>();
        cardSM.Setup();
    }

    public void Setup(CardData _data, IPlayer _player)
    {
        CurrentState = State.Inactive;
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
        Data = Instantiate(_data);
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
        switch (_data.Higlight)
        {
            case CardData.Highlight.NoHighlight:
                LowlightPanel.enabled = false;
                HighlightPanel.enabled = false;
                break;
            case CardData.Highlight.Highlighted:
                LowlightPanel.enabled = false;
                HighlightPanel.enabled = true;
                break;
            case CardData.Highlight.Lowlight:
                LowlightPanel.enabled = true;
                HighlightPanel.enabled = false;
                break;
            default:
                break;
        }
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
    public IPlayer GetPlayerOwner()
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