using UnityEngine;

[CreateAssetMenu(fileName = "New Lane", menuName = "Board/New Lane")]
public class LaneData : ScriptableObject
{
    public LaneType type;

    [HideInInspector] public DeckData playerAPlacedCards;
    [HideInInspector] public DeckData playerBPlacedCards;
}