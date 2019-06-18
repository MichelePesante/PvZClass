using UnityEngine;

[CreateAssetMenu(fileName = "New Draw Effect", menuName = "Effects/Draw")]
public class CardEffect_Draw : BaseCardEffect
{
    [System.Serializable]
    public class EffectData : BaseCardEffectData
    {
        public int cardsAmount;
    }

    [SerializeField] EffectData myData;

    public override void Setup(CardData _card, BaseCardEffectData _data = null)
    {
        myData.Card = _card;
        if (_data != null)
            myData = _data as EffectData;
    }

    public override void Execute()
    {
        DeckData tempDeckTo = myData.Card.CurrentDeck;
        DeckData tempDeckFrom = myData.Card.CurrentDeck.Player.Hand;
        CardViewManager.GetDeckViewControllerByDeckData(tempDeckTo).Draw(ref tempDeckFrom, myData.cardsAmount, true);
    }
}