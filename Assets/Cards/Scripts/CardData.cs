﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class CardData : ScriptableObject {

    #region serializable data
    public string CardName;
    public int StartCost;
    public int StartLife;
    public int StartAttack;
    public int CardIndex;
    public Faction CardFaction;
    public Rarity CardRarity;
    public bool IsHeroCard;
    public string CardImageName;
    public List<LaneType> excludeLanes;
    public Sprite CardImage;
    public EffectsContainer Effects;
    #endregion

    #region State
    public Action<CardState> OnCurrentStateChanged;

    private CardState _currentState;
    public CardState CurrentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            if (OnCurrentStateChanged != null)
            {
                OnCurrentStateChanged(_currentState);
            }
        }
    }
    #endregion

    public DeckData CurrentDeck { get; set; }

    public Action<CardData> OnDataChanged;
 
    private static int cardCounter;

    public string NameToShow {
        get { return CardName.ToUpper(); }
    }

    [HideInInspector]
    public int Cost, Attack, Life;

    private void Awake() {
        cardCounter++;
        CardIndex = cardCounter;
        ResetOriginalLife();
    }
    
    public bool CompareIndex(int _index)
    {
        return CardIndex == _index;
    }

    public enum Rarity { common, uncommon, rare, legendary}

    public enum Faction { Hipster = 0, Alcool = 1 }

    internal void ResetOriginalLife() {
        Cost = StartCost;
        Life = StartLife;
        Attack = StartAttack;

        if(OnDataChanged != null)
            OnDataChanged(this);
    }

    public override string ToString()
    {
        return string.Format("{0}({1})", NameToShow, cardCounter);
    }
}

public enum CardState
{
    Inactive = -1,
    Idle = 0,
    Playable = 1,
    Unplayable = 2,
    Drag = 3,
    Played = 4,
}
