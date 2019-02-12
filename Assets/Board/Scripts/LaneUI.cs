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

    public void SetUp(Lane _l)
    {
        MyLane = _l;
        LaneColourImage.color = MyLane.Type.LaneColor;
    }

    /// <summary>
    /// Toggles playable/unplayable graphics.
    /// </summary>
    /// <param name="_value"></param>
    public void ToggleHighlight(bool _value)
    {
        HighlightImage.gameObject.SetActive(_value);
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
            ToggleHighlight(MyLane.CheckCardPlayability(cardToCheck));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToggleHighlight(false);
    }
}
