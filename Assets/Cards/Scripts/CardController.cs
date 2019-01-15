using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardController : MonoBehaviour {

    [Header("Data References")]
    [SerializeField]
    private CardData cardDataPrefab;

    public Action<CardData> OnDataChanged;

    private CardData _data;
    public CardData Data {
        get { return _data; }
        private set { _data = value;
            if (OnDataChanged != null)
                OnDataChanged(Data);
        }
    }

    private void Start() {
        Setup();
    }

    public void Setup() {
        Data = GameObject.Instantiate(cardDataPrefab);
    }


    public void ResetOriginalLife() {
        _data.ResetOriginalLife();
    }

    public void UpdateLife(int _newlife) {
        Data.Life = _newlife;
    }
}
