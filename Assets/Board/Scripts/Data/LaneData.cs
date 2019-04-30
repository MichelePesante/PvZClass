using UnityEngine;

[CreateAssetMenu(fileName = "New Lane", menuName = "Board/New Lane")]
public class LaneData : ScriptableObject
{
    public LaneType type;

    [HideInInspector] public DeckData playerAPlacedDeck;
    [HideInInspector] public DeckData playerBPlacedDeck;
}