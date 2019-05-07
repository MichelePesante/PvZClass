using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlayer
{
    DeckViewController Deck { get; set; }
    DeckViewController Hand { get; set; }
    int Life { get; set; }
    int MaxEnergy { get; set; }
    int CurrentEnergy { get; set; }
    int Shield { get; set; }
    Player.Type CurrentType { get; set; }

    void Setup();
    event PlayerEvent.PlayerLost Lost;
}

public class PlayerEvent
{
    public delegate void PlayerLost(PlayerData player);
}
