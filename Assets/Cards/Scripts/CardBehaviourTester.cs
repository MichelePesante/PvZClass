using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviourTester : MonoBehaviour
{
    public CardController cardController;
    public BoardController boardController;

    // Start is called before the first frame update
    void Start()
    {
        boardController.SetUp();
        boardController.InstantiateBoard();
        cardController.Setup();
    }
}
