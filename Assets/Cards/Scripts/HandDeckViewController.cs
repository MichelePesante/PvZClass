using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class HandDeckViewController : DeckViewControllerBase
{
    [SerializeField] CardViewController cardPrefab;

    /*public void UpdateInstantiatedCards()
    {
        if (instantiatedCards == null || instantiatedCards.Count == 0)
        {
            InstantiateCards(Data.Cards);
            return;
        }

        bool found = false;
        int removeIndex = -1;
        for (int i = 0; i < Data.Cards.Count; i++)
        {
            for (int j = 0; j < instantiatedCards.Count; j++)
            {
                if (instantiatedCards[j].Data.cardIndex == Data.Cards[i].cardIndex)
                {
                    found = true;
                    break;
                }
                removeIndex = j;
            }

            if (!found && removeIndex != -1)
                instantiatedCards.RemoveAt(removeIndex);

            found = false;
            removeIndex = -1;
        }

        List<CardData> cardsToInstantate = new List<CardData>();
        for (int i = 0; i < instantiatedCards.Count; i++)
        {
            if (Data.Cards.Where(c => c.CompareIndex(instantiatedCards[i].Data.cardIndex)).Count() == 0)
            {
                cardsToInstantate.Add(instantiatedCards[i].Data);
            }
        }

        InstantiateCards(cardsToInstantate);
    }*/
}