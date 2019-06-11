using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData
{
    public string Name;
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
    public PlayerData Player;
    public int MaxCards;

    public DeckData(string _name = null) {
        Name = _name;
        Cards = new List<CardData>();
        OldCards = new List<CardData>();
        Player = null;
        MaxCards = -1;
    }

    public DeckData(List<CardData> _cards, int _maxCards, string _name = null) {
        Name = _name;
        Cards = _cards;
        OldCards = new List<CardData>();
        MaxCards = _maxCards;
        Player = null;
    }

    public DeckData(int _maxCards, string _name = null)
    {
        Name = null;
        Cards = new List<CardData>();
        OldCards = new List<CardData>();
        MaxCards = _maxCards;
        Player = null;
    }
}