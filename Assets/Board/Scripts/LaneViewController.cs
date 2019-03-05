using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class LaneViewController : MonoBehaviour, IDetectable
{
    LaneData _data;
    public LaneData Data {
        get { return _data; }
        private set { _data = value; }
    }

    public Image LaneColourImage;
    public Image HighlightImage;

    bool isInteractable = false;
    CardViewController cardToCheck;

    public enum Highlight { playable, unplayable, off }

    public LaneViewController SetUp(LaneData _data)
    {
        Data = _data;
        LaneColourImage.color = Data.type.LaneColor;
        return this;
    }

    /// <summary>
    /// Toggles playable/unplayable graphics.
    /// </summary>
    /// <param name="_value"></param>
    public void ToggleHighlight(Highlight _highlight)
    {
        switch (_highlight)
        {
            case Highlight.playable:
                HighlightImage.gameObject.SetActive(true);
                HighlightImage.color = Color.green;
                break;
            case Highlight.unplayable:
                HighlightImage.gameObject.SetActive(true);
                HighlightImage.color = Color.red;
                break;
            case Highlight.off:
                HighlightImage.gameObject.SetActive(false);
                break;
        }
    }

    public void PlaceCard(CardViewController _cardToPlace) {

    }

    public void OnEnter(IDetecter _detecter) {
        CardViewController _card = _detecter as CardViewController;
        if (_card) {
            cardToCheck = _card;
            if (LaneController.CheckCardPlayability(Data, _card.Data)) {
                ToggleHighlight(Highlight.playable);
            } else {
                ToggleHighlight(Highlight.unplayable);
            }
        }
    }

    public void OnExit(IDetecter _detecter) {
        CardViewController _card = _detecter as CardViewController;
        if (_card) {
            ToggleHighlight(Highlight.off);
        }
    }
}
