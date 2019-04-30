using System;
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
    public LaneType playableLane;
    #endregion

    public Action<CardData> OnDataChanged;
 
    private static int cardCounter;
    public Sprite CardImage;

    public string NameToShow {
        get { return CardName.ToUpper(); }
    }

    public Highlight Higlight { get; internal set; }

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

    public enum Highlight { NoHighlight, Highlighted, Lowlight }

    internal void ResetOriginalLife() {
        Cost = StartCost;
        Life = StartLife;
        Attack = StartAttack;

        if(OnDataChanged != null)
            OnDataChanged(this);
    }
}
