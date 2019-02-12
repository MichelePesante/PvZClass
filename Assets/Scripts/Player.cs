using UnityEngine;
using System;

public class Player : MonoBehaviour, IPlayer
{

    public enum Type { one, two }

    Type currentType;
    public Type CurrentType { get { return currentType; } set { currentType = value; } }

    DeckController deck;
    public DeckController Deck
    {
        get { return deck; }
        set { deck = value; }
    }

    DeckController hand;
    public DeckController Hand
    {
        get { return hand; }
        set { hand = value; }
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
                Lost(this);
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

    public void Draw(int cards = 1)
    {
        if (Hand == null)
            Hand = new DeckController();
        Deck.Draw(Hand, cards);
    }
}
