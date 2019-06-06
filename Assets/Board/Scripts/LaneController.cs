using UnityEngine;

public static class LaneController
{
    public static LaneData SetType(LaneData _data, LaneType _s)
    {
        if (_s == null)
        {
            Debug.LogError("Lanes do not accept a null string for their type!");
            return null;
        }
        _data.type = _s;
        return _data;
    }

    public static bool CheckCardPlayability(LaneData _lane, CardData _card)
    {
        if (!_card.excludeLanes.Contains(_lane.type))
        {
            switch (TurnManager.GetActivePlayer().Data.CurrentType)
            {
                case PlayerData.Type.one:
                    if (_lane.playerAPlacedDeck.Cards.Count < _lane.playerAPlacedDeck.MaxCards)
                        return true;
                    break;
                case PlayerData.Type.two:
                    if (_lane.playerBPlacedDeck.Cards.Count < _lane.playerBPlacedDeck.MaxCards)
                        return true;
                    break;
            }
        }

        return false;
    }

    public static void SetPlayerSlots(LaneData _dataToSet, DeckData _playerSlots, PlayerData.Type _playerType)
    {
        switch (_playerType)
        {
            case PlayerData.Type.one:
                _dataToSet.playerAPlacedDeck = _playerSlots;
                _dataToSet.playerAPlacedDeck.Player = GameplaySceneManager.GetPlayer(PlayerData.Type.one).Data;
                break;
            case PlayerData.Type.two:
                _dataToSet.playerBPlacedDeck = _playerSlots;
                _dataToSet.playerBPlacedDeck.Player = GameplaySceneManager.GetPlayer(PlayerData.Type.two).Data;
                break;
        }
    }
}