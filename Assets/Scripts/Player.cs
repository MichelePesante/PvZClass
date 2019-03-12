using UnityEngine;
using System;

public class Player : MonoBehaviour, IPlayer {

    public enum Type { one, two }

    [SerializeField]
    Type currentType;
    public Type CurrentType { get { return currentType; } set { currentType = value; } }

    DeckData _deck;
    public DeckData Deck {
        get { return _deck; }
        set { _deck = value; }
    }

    [SerializeField] HandDeckViewController _hand;
    public HandDeckViewController Hand {
        get { return _hand; }
        set {
            _hand = value;
        }
    }

    public event PlayerEvent.PlayerLost Lost;

    int life;
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

    int maxEnergy;
    public int MaxEnergy {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }

    int energy;
    public int CurrentEnergy {
        get { return energy; }
        set { energy = value; }
    }

    int shield;
    public int Shield {
        get { return shield; }
        set { shield = value; }
    }

    public void Draw(int cards = 1) {

        DeckController.Draw(Hand.Data, Deck, cards);
    }
}
