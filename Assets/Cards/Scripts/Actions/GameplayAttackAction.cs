﻿public class GameplayAttackAction : IAction {

    public CardData attackingCardData;
    public CardData defendingCardData;
    public PlayerData defendingPlayerData;

    public ActionType Type { get; set; }

    /// <summary>
    /// Create an action which describes <paramref name="_attackingCardData"/> attacking <paramref name="_defendingCardData"/>.
    /// </summary>
    /// <param name="_attackingCardData"></param>
    /// <param name="_defendingCardData"></param>
    /// <returns></returns>
    public static GameplayAttackAction CreateCardAttackAction(CardData _attackingCardData, CardData _defendingCardData) {
        GameplayAttackAction returnAction = new GameplayAttackAction() {
            attackingCardData = _attackingCardData,
            defendingCardData = _defendingCardData,
            Type = ActionType.Attack,
        };

        return returnAction;
    }

    /// <summary>
    /// Create an action which describes <paramref name="_attackingCardData"/> attacking the player <paramref name="_defendingPlayerData"/>.
    /// </summary>
    /// <param name="_attackingCardData"></param>
    /// <param name="_defendingPlayerData"></param>
    /// <returns></returns>
    public static GameplayAttackAction CreatePlayerAttackAction(CardData _attackingCardData, PlayerData _defendingPlayerData)
    {
        GameplayAttackAction returnAction = new GameplayAttackAction()
        {
            attackingCardData = _attackingCardData,
            defendingPlayerData = _defendingPlayerData,
            Type = ActionType.Attack,
        };

        return returnAction;
    }

    //HACK: there was a tostring thing here.
}