using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LaneViewController : MonoBehaviour, IDetectable
{
    #region Serialized Fields
    LaneData _data;
    public LaneData Data
    {
        get { return _data; }
        private set { _data = value; }
    }
    public Image LaneColourImage;
    public Image HighlightImage;
    [SerializeField] RectTransform SlotPrefab;
    [SerializeField] RectTransform playerAZone;
    [SerializeField] RectTransform playerBZone;
    #endregion

    public enum Highlight { playable, unplayable, off }

    CardSlot[] playerASlots, playerBSlots;

    #region API

    public LaneViewController SetUp(LaneData _data, int _cardSlotsCount)
    {
        Data = Instantiate(_data);
        Data.playerAFreeSlots = _cardSlotsCount;
        Data.playerBFreeSlots = _cardSlotsCount;
        CardSlotsSetup(ref playerASlots, _cardSlotsCount, 0);
        CardSlotsSetup(ref playerBSlots, _cardSlotsCount, 1);
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

    public void PlaceCard(CardViewController _cardToPlace)
    {
        switch (TurnManager.GetActivePlayer().CurrentType)
        {
            case Player.Type.one:
                for (int i = playerASlots.Length - 1; i >= 0; i--)
                {
                    if (!playerASlots[i].card)
                    {
                        playerASlots[i].card = _cardToPlace;
                        _cardToPlace.transform.position = playerASlots[i].slot.position;
                        Data.playerAFreeSlots--;
                        break;
                    }
                }
                break;
            case Player.Type.two:
                for (int i = 0; i < playerBSlots.Length; i++)
                {
                    if (!playerBSlots[i].card)
                    {
                        playerBSlots[i].card = _cardToPlace;
                        _cardToPlace.transform.position = playerBSlots[i].slot.position;
                        Data.playerBFreeSlots--;
                        break;
                    }
                }
                break;
        }
    }

    public void OnEnter(IDetecter _detecter)
    {
        CardViewController _card = _detecter as CardViewController;
        if (_card)
        {
            if (LaneController.CheckCardPlayability(Data, _card.Data))
            {
                ToggleHighlight(Highlight.playable);
            }
            else
            {
                ToggleHighlight(Highlight.unplayable);
            }
        }
    }

    public void OnExit(IDetecter _detecter)
    {
        CardViewController _card = _detecter as CardViewController;
        if (_card)
        {
            ToggleHighlight(Highlight.off);
        }
    }

    #endregion

    void CardSlotsSetup(ref CardSlot[] _slotsToSetup, int _slotCount, int _player)
    {
        _slotsToSetup = new CardSlot[_slotCount];

        RectTransform slotsParent = _player == 0 ? playerAZone : playerBZone;

        for (int i = 0; i < _slotsToSetup.Length; i++)
        {
            RectTransform t = Instantiate(SlotPrefab, slotsParent);
            _slotsToSetup[i].slot = t;
        }
    }

    struct CardSlot
    {
        public CardViewController card;
        public RectTransform slot;
    }
}
