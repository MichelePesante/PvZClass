using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerDownHandler
{

    [Header("Data References")]
    [SerializeField]
    private CardData cardDataPrefab;
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
    }
    public void Setup(CardData _data)
    {
        Data = Instantiate(_data);
        Interactable(true);
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
        if (interactable && OnCardClicked != null)
            OnCardClicked(this);
    }

    bool interactable;
    public void Interactable(bool _intertactable)
    {
        interactable = _intertactable;
    }
}
