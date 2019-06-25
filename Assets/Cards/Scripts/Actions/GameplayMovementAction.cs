using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayMovementAction : IAction {

    public CardData cardData;
    public DeckData deckDataFrom;
    public DeckData deckDataTo;
    public bool instantExecute;

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
        if (deckDataFrom != null)
            return string.Format("{0} {1} From: {2}, To: {3} ", Type.ToString(), cardData.CardName, deckDataFrom.Name, deckDataTo.Name);
        else
            return string.Format("{0} {1} To: {2} ", Type.ToString(), cardData.CardName, deckDataTo.Name);
    }

}

public enum ActionType {
        Movement = 10,
        Attack = 20
}

public interface IAction {
    ActionType Type { get; set; }
}
