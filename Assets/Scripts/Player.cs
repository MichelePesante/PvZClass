using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IPlayer {

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

    int life;
    public int Life
    {
        get { return life; }
        set { life = value; }
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
