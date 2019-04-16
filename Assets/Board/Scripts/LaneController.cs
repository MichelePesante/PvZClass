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
        if (_card.playableLane == _lane.type)
        {
            switch (TurnManager.GetActivePlayer().CurrentType)
            {
                case Player.Type.one:
                    if (_lane.playerAPlacedCards.Cards.Count > 0)
                        return true;
                    break;
                case Player.Type.two:
                    if (_lane.playerAPlacedCards.Cards.Count > 0)
                        return true;
                    break;
            }
        }

        return false;
    }

    public static void SetPlayerSlots(LaneData _dataToSet, DeckData _playerSlots, Player.Type _playerType)
    {
        switch (_playerType)
        {
            case Player.Type.one: _dataToSet.playerAPlacedCards = _playerSlots;
                break;
            case Player.Type.two: _dataToSet.playerBPlacedCards = _playerSlots;
                break;
        }
    }
}