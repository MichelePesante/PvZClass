using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData
{
    private List<CardData> _Cards;
    public List<CardData> Cards
    {
        set
        {
            OldCards = _Cards;
            _Cards = value;
        }
        get
        {
            return _Cards;
        }
    }
    public List<CardData> OldCards;
    public IPlayer Player;    

    public DeckData() {
        Cards = new List<CardData>();
        OldCards = new List<CardData>();
        Player = null;
    }

    public DeckData(List<CardData> _cards) {
        Cards = _cards;
        OldCards = new List<CardData>();
        Player = null;
    }
}
