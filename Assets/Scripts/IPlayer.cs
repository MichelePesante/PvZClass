using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{

    DeckController Deck { get; set; }
    DeckController Hand { get; set; }
    int Life { get; set; }
    int MaxEnergy { get; set; }
    int CurrentEnergy { get; set; }
    int Shield { get; set; }

    void Draw(int cards = 1);
}
