using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "New Card")]
public class CardData : ScriptableObject {

    public string CardName;
    public int Cost;
    public int Life;
    public int Attack;
    public Rarity CardRarity;
    public bool IsHeroCard;
    public Sprite CardImage; 

    public enum Rarity { common, uncommon, rare, legendary}
}
