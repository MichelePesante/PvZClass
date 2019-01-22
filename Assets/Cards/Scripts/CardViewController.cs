﻿using UnityEngine;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {

    [Header("UI References")]
    public Text Name;
    public ValueDisplayController Attack;
    public ValueDisplayController Life;
    public ValueDisplayController Cost;
    public Image Image;
    public Image Frame;

    private Color HeroColor = Color.white;
    private Color StandardColor = Color.white;

    public CardController Controller;

    private void OnEnable() {
        Setup(Controller);
    }

    public void Setup(CardController _cardController) {
        Controller = _cardController;
        if (!Controller)
            Controller = GetComponent<CardController>();
        if (Controller)
            Controller.OnDataChanged += onDataChanged;
    }

    private void onDataChanged(CardData _data) {
        Name.text = _data.NameToShow;
        Attack.SetValue(_data.Attack.ToString());
        Life.SetValue(_data.Life.ToString());
        Cost.SetValue(_data.Cost.ToString());
        Image.sprite = _data.CardImage;
        if (_data.IsHeroCard) {
            Frame.color = HeroColor;
        }else {
            Frame.color = StandardColor;
        }
    }

}
