using UnityEngine;
public class PlayerView : MonoBehaviour {
    public PlayerData Data { get; set; }
    public DeckViewController HandDeck { get; protected set; }
    public DeckViewController PlayerDeck { get; protected set; }

    public void Init(PlayerData _data) {
        Data = _data;
        Data.Lost += onLost;
    }

    /// <summary>
    /// Richiamata quando il player perde.
    /// </summary>
    /// <param name="player"></param>
    private void onLost(PlayerData player) {
        Destroy(gameObject);
    }

    public void CardPlayed(CardData cardData) {
        PlayerController.CardPlayed(Data, cardData);
    }

    /// <summary>
    /// Gruppo di carte in mano al player.
    /// </summary>
    /// <param name="deckView"></param>
    public void SetHandDeck(DeckViewController deckView) {
        HandDeck = deckView;
        PlayerController.SetHandDeck(Data, deckView.Data);
    }

    /// <summary>
    /// Setta il deck del player.
    /// </summary>
    /// <param name="deckView"></param>
    public void SetPlayerDeck(DeckViewController deckView) {
        PlayerDeck = deckView;
        PlayerController.SetPlayerDeck(Data, deckView.Data);
    }

}

