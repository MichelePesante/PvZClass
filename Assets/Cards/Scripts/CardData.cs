﻿using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class CardData : ScriptableObject {

    public string CardName;
    [SerializeField] private int StartCost;
    [SerializeField] private int StartLife;
    [SerializeField] private int StartAttack;
    public Rarity CardRarity;
    public bool IsHeroCard;
    public Sprite CardImage;

    public string NameToShow {
        get { return CardName.ToUpper(); }
    }

    [HideInInspector]
    public int Cost, Attack, Life;

    private void Awake() {
        ResetOriginalLife();
    }
    
    public enum Rarity { common, uncommon, rare, legendary}

    internal void ResetOriginalLife() {
        Cost = StartCost;
        Life = StartLife;
        Attack = StartAttack;
    }
}
