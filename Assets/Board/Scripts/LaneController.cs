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
            switch (TurnManager.GetActivePlayer().Data.CurrentType)
            {
                case PlayerData.Type.one:
                    if (_lane.playerAFreeSlots > 0)
                        return true;
                    break;
                case PlayerData.Type.two:
                    if (_lane.playerBFreeSlots > 0)
                        return true;
                    break;
            }
        }

        return false;
    }
}