using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour, IPlayer
{
    #region Delegates
    public static Action<Type,List<CardData>> OnCardsDrawn;
    #endregion

    public enum Type { one, two }

    [SerializeField]
    Type currentType;
    public Type CurrentType { get { return currentType; } set { currentType = value; } }

    [SerializeField]
    DeckViewController _deck;
    public DeckViewController Deck
    {
        get { return _deck; }
        set { _deck = value; }
    }

    [SerializeField]
    HandDeckViewController _hand;
    public HandDeckViewController Hand
    {
        get { return _hand; }
        set
        {
            _hand = value;
        }
    }

    public event PlayerEvent.PlayerLost Lost;

    int life;
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            if (life <= 0)
            {
                if (Lost != null)
                    Lost(this);
            }
        }
    }

    int maxEnergy;
    public int MaxEnergy
    {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }

    int energy;
    public int CurrentEnergy
    {
        get { return energy; }
        set { energy = value; }
    }

    int shield;
    public int Shield
    {
        get { return shield; }
        set { shield = value; }
    }

    public void Setup()
    {
        Life = 20;
        MaxEnergy = 0;
        CurrentEnergy = 0;
        Deck.Data.Player = this;
        Hand.Setup(new DeckData());
        DeckData deckFrom = Deck.Data;
        Hand.Draw(ref deckFrom, 8, HandDrawCallback);
        Hand.Data.Player = this;
        CardController.OnPlaced += HandleCardPlacement;
        TurnManager.OnTurnChange += HandleTurnChange;
    }

    private void HandleTurnChange(IPlayer _player)
    {
        UpdateHandState(CardViewController.State.Idle);
    }

    private void HandleCardPlacement(CardData _card)
    {
        DeckData deckTo = null;
        Hand.Move(ref deckTo, ref _card);
        UpdateHandState(CardViewController.State.Idle);
    }

    public void UpdateHandState(CardViewController.State state)
    {
        DeckController.SetCardsState(Hand, state);
    }

    public void HandDrawCallback(List<CardData> _drawnCard)
    {
        if (OnCardsDrawn != null)
            OnCardsDrawn(currentType,_drawnCard);
    }
}
