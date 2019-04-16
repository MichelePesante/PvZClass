using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayAction : IAction {

    public CardData cardData;
    public DeckData deckDataFrom;
    public DeckData deckDataTo;


    public ActionType Type { get; set; }

    public static GameplayAction CreateMovementAction(CardData _cardData, DeckData _deckDataFrom, DeckData _deckDataTo) {
        GameplayAction returnAction = new GameplayAction() {
            cardData = _cardData,
            deckDataFrom = _deckDataFrom,
            deckDataTo = _deckDataTo,
            Type = ActionType.Movement,
        };

        return returnAction;
    }


}

public enum ActionType {
        Movement = 10,
}

public interface IAction {
    ActionType Type { get; set; }
}
