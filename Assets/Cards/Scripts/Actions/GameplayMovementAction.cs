using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayMovementAction : IAction {

    public CardData cardData;
    public DeckData deckDataFrom;
    public DeckData deckDataTo;


    public ActionType Type { get; set; }

    public static GameplayMovementAction CreateMovementAction(CardData _cardData, DeckData _deckDataFrom, DeckData _deckDataTo) {
        GameplayMovementAction returnAction = new GameplayMovementAction() {
            cardData = _cardData,
            deckDataFrom = _deckDataFrom,
            deckDataTo = _deckDataTo,
            Type = ActionType.Movement,
        };

        return returnAction;
    }

    public override string ToString()
    {
        return string.Format("{0} {1} From: {2}, To: {3} ", Type.ToString(), cardData.ToString(), deckDataFrom.ToString(), deckDataTo.ToString());
    }

}

public enum ActionType {
        Movement = 10,
        Attack = 20
}

public interface IAction {
    ActionType Type { get; set; }
}
