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

    [Header("UI Objects")]
    public Image LaneColourImage;
    public Image HighlightImage;
    [SerializeField] RectTransform SlotPrefab;
    [SerializeField] DeckViewController _playerASlotsView;
    [SerializeField] DeckViewController _playerBSlotsView;

    #endregion

    static int LaneIndex = 0;

    public enum Highlight { playable, unplayable, off }

    public DeckViewController PlayerASlotsView
    {
        get
        {
            if (_playerASlotsView.Data == null)
                _playerASlotsView.Setup(Data.playerAPlacedDeck);

            return _playerASlotsView;
        }
        set
        {
            _playerASlotsView = value;
        }
    }

    public DeckViewController PlayerBSlotsView
    {
        get
        {
            if (_playerBSlotsView.Data == null)
                _playerBSlotsView.Setup(Data.playerBPlacedDeck);

            return _playerBSlotsView;
        }
        set
        {
            _playerBSlotsView = value;
        }
    }

    #region API

    public LaneViewController SetUp(LaneData _data, int _cardSlotsCount)
    {
        //instantiate lane data.
        Data = Instantiate(_data);

        //create two deckdata and assing to lane data.
        LaneController.SetPlayerSlots(Data, new DeckData(_cardSlotsCount, string.Format("Lane {0}", LaneIndex)), PlayerData.Type.one);
        LaneController.SetPlayerSlots(Data, new DeckData(_cardSlotsCount, string.Format("Lane {0}", LaneIndex)), PlayerData.Type.two);
        LaneIndex++;

        PlayerASlotsView.Setup(Data.playerAPlacedDeck);
        PlayerBSlotsView.Setup(Data.playerBPlacedDeck);

        //view stuff
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
        DeckData deckTo = TurnManager.GetActivePlayer().GetMyLaneDeck(Data).Data;
        DeckData deckFrom = TurnManager.GetActivePlayer().GetHandView().Data;
        CardData cardToAdd = _cardToPlace.Data;

        DeckViewController deckFromView = CardViewManager.GetDeckViewControllerByDeckData(deckFrom);
        deckFromView.DoMoveFromMe(ref deckTo, ref cardToAdd, true);

        _cardToPlace.GetPlayerOwner().Data.CurrentEnergy -= _cardToPlace.Data.Cost;       
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

    /// <summary>
    /// Initialize Lane Datas and instantiates needed transforms.
    /// </summary>
    /// <param name="_laneDeckToSetup"></param>
    /// <param name="_cardSlotsCount"></param>
    /// <param name="_player"></param>
    void CardSlotsSetup(DeckViewController _laneDeckToSetup, int _cardSlotsCount, int _player)
    {
        //if (_player == 0)
        //{
        //    Data.playerAPlacedDeck.MaxCards = _cardSlotsCount;
        //}
        //else
        //{
        //    Data.playerBPlacedDeck.MaxCards = _cardSlotsCount;
        //}

        //_laneDeckToSetup.Data.Player = _player == 0 ? GameplaySceneManager.GetPlayer(Player.Type.one) : GameplaySceneManager.GetPlayer(Player.Type.one);

        //RectTransform slotsParent = _laneDeckToSetup.Data.Player.CurrentType == Player.Type.one ? PlayerASlotsView.transform : PlayerBSlotsView.transform;

        //for (int i = 0; i < _laneDeckToSetup.Data.MaxCards; i++)
        //{
        //    RectTransform t = Instantiate(SlotPrefab, slotsParent);
        //}
    }
}