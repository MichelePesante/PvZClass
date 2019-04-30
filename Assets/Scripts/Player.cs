using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

}

/* ===== MVC =====
 * - Utilizzare Snippet _MVC_Class per creare la struttura classi MVC
 * - Le classi View possono esistere o meno e possono essere più di una.
 * - Nella classe data devono esserci solo i dati e funzioni/eventi/delegate che agiscono solo sui dati
 * - La classe controller contiene solo le logiche di funzionamento. Non ha dati ma gli devono essere passati per poter espletare le sue logiche.
 * 
 */
[Serializable]
public class PlayerData {

    public PlayerData(int life, int maxEnergy) {
        this.life = life;
        this.maxEnergy = maxEnergy;
        this.CurrentEnergy = maxEnergy;
    }

    #region Serializables
    [SerializeField] private Type currentType;
    [SerializeField] private int life;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energy;
    [SerializeField] private int shield;
    [SerializeField] public enum Type { one, two }
    #endregion

    #region Properties
    public Type CurrentType {
        get { return currentType; }
        set { currentType = value; }
    }

    public int Shield {
        get { return shield; }
        set { shield = value; }
    }

    public int Life {
        get { return life; }
        set {
            life = value;
            if (life <= 0) {
                if (Lost != null)
                    Lost(this);
            }
        }
    }

    public int MaxEnergy {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }

    public int CurrentEnergy {
        get { return energy; }
        set { energy = value; }
    }

    #endregion

    #region events
    public event PlayerEvent.PlayerLost Lost;
    #endregion

}

public static class PlayerController {

    public static void SetHandDeck(PlayerData player, DeckData deck) {
        // TODO
        // deck.Player = player; controllare se rimane così
    }

    public static void SetPlayerDeck(PlayerData player, DeckData deck) {
        // TODO
        // deck.Player = player; controllare se rimane così
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

public class PlayerView : MonoBehaviour {
    public PlayerData Data { get; set; }

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

}

public class PlayerMenuSelectionView : MonoBehaviour {
    public PlayerData Data { get; set; }

    public void Init(PlayerData _data) {
        Data = _data;
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
}

