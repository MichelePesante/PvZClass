public static class PlayerController {

    public static void SetLife(PlayerData _playerData, int _value)
    {
        _playerData.CurrentLife = _value;
    }

    public static void SetHandDeck(PlayerData player, DeckData deck) {
        deck.Player = player;
        player.Hand = deck;
    }

    public static void SetPlayerDeck(PlayerData player, DeckData deck) {
        deck.Player = player;
        player.Deck = deck;
    }

    public static void CardPlayed(PlayerData playerData, CardData cardData) {
        // todo...
    }
}