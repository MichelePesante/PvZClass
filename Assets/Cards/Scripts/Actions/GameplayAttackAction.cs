public class GameplayAttackAction : IAction {

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

    public override string ToString()
    {
        if (defendingCardData != null)
            return string.Format("{0} attack {1} by {2}", attackingCardData.CardName, defendingCardData.CardName, attackingCardData.Attack.ToString());
        else if (defendingPlayerData != null)
            return string.Format("{0} attack {1} by {2}", attackingCardData.CardName, defendingPlayerData.Faction, attackingCardData.Attack.ToString());
        else
            return string.Format("{0} attack nothing", attackingCardData.CardName);
    }

    //HACK: there was a tostring thing here.
}