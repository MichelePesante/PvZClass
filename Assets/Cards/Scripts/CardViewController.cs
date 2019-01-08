using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewController : MonoBehaviour {

    [Header("UI References")]
    public Text Name;
    public Text Attack;
    public Text Life;
    public Text Cost;
    public Image Image;

    public CardController Controller;

    private void OnEnable() {
        Setup(Controller);
    }

    public void Setup(CardController _cardController) {
        Controller = _cardController;
        if (Controller)
            Controller.OnDataChanged += onDataChanged;
    }

    private void onDataChanged(CardData _data) {
        Name.text = _data.NameToShow;
        Attack.text = _data.Attack.ToString();
        Life.text = _data.Life.ToString();
        Cost.text = _data.Cost.ToString();
        Image.sprite = _data.CardImage;
    }

}
