using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class CardData : ScriptableObject {

    public string CardName;
    [SerializeField] private int StartCost;
    [SerializeField] private int StartLife;
    [SerializeField] private int StartAttack;
    private static int cardCounter;
    public int cardIndex;
    public Faction CardFaction;
    public Rarity CardRarity;
    public bool IsHeroCard;
    public Sprite CardImage;
    public LaneType playableLane;

    public string NameToShow {
        get { return CardName.ToUpper(); }
    }

    public Highlight Higlight { get; internal set; }

    [HideInInspector]
    public int Cost, Attack, Life;

    private void Awake() {
        cardCounter++;
        cardIndex = cardCounter;
        ResetOriginalLife();
    }
    
    public bool CompareIndex(int _index)
    {
        return cardIndex == _index;
    }

    public enum Rarity { common, uncommon, rare, legendary}

    public enum Faction { Hipster, Alcool }

    public enum Highlight { NoHighlight, Highlighted, Lowlight }

    internal void ResetOriginalLife() {
        Cost = StartCost;
        Life = StartLife;
        Attack = StartAttack;
    }
}
