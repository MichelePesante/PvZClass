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
    public Image HighlightPanel;
    public Image LowlightPanel;

    private Color HeroColor = Color.green;
    private Color StandardColor = Color.gray;

    public CardController Controller;

    private void OnEnable() {
        Setup(Controller);
    }

    private void OnDisable() {
        Controller.OnDataChanged -= onDataChanged;
    }

    public void Setup(CardController _cardController) {
        Controller = _cardController;
        if (!Controller)
            Controller = GetComponent<CardController>();
        if (Controller)
        {
            Controller.OnDataChanged += onDataChanged;
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
        switch (_data.Higlight) {
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

}
