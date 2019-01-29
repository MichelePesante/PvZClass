using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {

    [Header("UI References")]
    //public Text Name;
    public ValueDisplayController Attack;
    public ValueDisplayController Life;
    public ValueDisplayController Cost;
    public Image Image;
    public Image Frame;
    public Image SelectedPanel; 

    private Color HeroColor = Color.green;
    private Color StandardColor = Color.gray;

    public CardController Controller;

    private void OnEnable() {
        Setup(Controller);
    }

    private void OnDisable() {
        Controller.OnDataChanged -= onDataChanged;
        Controller.OnCardClicked -= CardClicked;
    }

    public void Setup(CardController _cardController) {
        Select(false);
        Controller = _cardController;
        if (!Controller)
            Controller = GetComponent<CardController>();
        if (Controller)
        {
            Controller.OnDataChanged += onDataChanged;
            Controller.OnCardClicked += CardClicked;
        }
    }

    private void onDataChanged(CardData _data) {
        //Name.text = _data.NameToShow;
        Attack.SetValue(_data.Attack.ToString());
        Life.SetValue(_data.Life.ToString());
        Cost.SetValue(_data.Cost.ToString());
        Image.sprite = _data.CardImage;
        if (_data.IsHeroCard) {
            Frame.color = HeroColor;
        }else {
            Frame.color = StandardColor;
        }

        Select(false);
    }

    public void CardClicked(CardController _card)
    {
        if (_card != Controller)
            return;

        if (selected)
            Select(false);
        else
            Select(true);
    }

    bool selected;
    public void Select(bool _select)
    {
        selected = _select;
        SelectedPanel.gameObject.SetActive(_select);
    }

}
