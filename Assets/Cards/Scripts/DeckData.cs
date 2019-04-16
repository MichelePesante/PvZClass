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
    public int MaxCards;

    public DeckData() {
        Cards = new List<CardData>();
        OldCards = new List<CardData>();
        Player = null;
        MaxCards = -1;
    }

    public DeckData(List<CardData> _cards, int _maxCards) {
        Cards = _cards;
        OldCards = new List<CardData>();
        MaxCards = _maxCards;
        Player = null;
    }

    public DeckData(int _maxCards)
    {
        Cards = new List<CardData>();
        OldCards = new List<CardData>();
        MaxCards = _maxCards;
        Player = null;
    }
}
