using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaneUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Lane MyLane { get; set; }

    public Image LaneColourImage;
    public Image HighlightImage;

    bool isInteractable = false;
    CardController cardToCheck;

    enum Highlight { playable, unplayable, off }

    public void SetUp(Lane _l)
    {
        MyLane = _l;
        LaneColourImage.color = MyLane.Type.LaneColor;
    }

    /// <summary>
    /// Toggles playable/unplayable graphics.
    /// </summary>
    /// <param name="_value"></param>
    void ToggleHighlight(Highlight _highlight)
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

    /// <summary>
    /// Sets the interactability of the lane, also passes the card in drag in that moment.
    /// </summary>
    /// <param name="_value"></param>
    /// <param name="_cardToCheck"></param>
    public void SetInteractability(bool _value, CardController _cardToCheck = null)
    {
        isInteractable = _value;
        if (_cardToCheck)
            cardToCheck = _cardToCheck;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteractable)
        {
            if (MyLane.CheckCardPlayability(cardToCheck))
            {
                ToggleHighlight(Highlight.playable);
            }
            else
            {
                ToggleHighlight(Highlight.unplayable);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleHighlight(Highlight.off);
    }
}
