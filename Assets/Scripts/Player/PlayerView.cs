using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {

    [SerializeField] Text energyText, lifeText;

    public PlayerData Data { get; set; }
    public DeckViewController HandDeck { get; protected set; }
    public DeckViewController PlayerDeck { get; protected set; }

    public void Init(PlayerData _data) {
        Data = _data;
        Data.Lost += onLost;
        Data.OnEnergyChange += onEnergyChange;
        Data.OnLifeChange += onLifeChange;
    }

    /// <summary>
    /// Richiamata quando il player perde.
    /// </summary>
    /// <param name="player"></param>
    private void onLost(PlayerData player) {
        Destroy(gameObject);
    }

    void onEnergyChange()
    {
        if (energyText)
            energyText.text = Data.CurrentEnergy.ToString();
    }

    void onLifeChange()
    {
        if (lifeText)
            lifeText.text = Data.CurrentLife.ToString();
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

    /// <summary>
    /// Restituisce la deckview del player corrente della <paramref name="_lane"/>.
    /// </summary>
    /// <param name="_lane"></param>
    /// <returns></returns>
    public DeckViewController GetMyLaneDeck(LaneData _lane) {
        DeckData returnDeck = null;
        switch (Data.CurrentType) {
            case PlayerData.Type.one:
                returnDeck = _lane.playerAPlacedDeck;
                break;
            case PlayerData.Type.two:
                returnDeck = _lane.playerBPlacedDeck;
                break; 
            default:
                break;
        }

        if (returnDeck != null) {
            foreach (var deckView in FindObjectsOfType<DeckViewController>()) {
                if (deckView.Data == returnDeck)
                    return deckView;
            }
        }
        return null;
    }

    public DeckViewController GetHandView() {
        return HandDeck;
    }

}

