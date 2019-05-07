public static class PlayerController {

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

    // TODO : vecchie operazioni del setup del player che vanno ricollocate
    //public static void Setup(PlayerData data) {
    //    Hand.Setup(new DeckData());
    //    Hand.Draw(Deck, 8, HandDrawCallback);
    //    Hand.Data.Player = this;
    //    CardController.OnPlaced += HandleCardPlacement;
    //    TurnManager.OnTurnChange += HandleTurnChange;
    //}

    // Vecchie funzione handles da rilocare
    //private void HandleTurnChange(IPlayer _player) {
    //    UpdateHandState(CardViewController.State.Idle);
    //}

    //private void HandleCardPlacement(CardData _card) {
    //    Hand.RemoveCard(_card);
    //    UpdateHandState(CardViewController.State.Idle);
    //}

}

