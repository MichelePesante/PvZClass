using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData
{
    public List<CardData> Cards;
    public IPlayer Player;    

    public DeckData() {
        Cards = new List<CardData>();
        Player = null;
    }

    public DeckData(List<CardData> _cards) {
        Cards = _cards;
        Player = null;
    }
}
