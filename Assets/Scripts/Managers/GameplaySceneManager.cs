using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour {

    public Player player, player2;
    [Header("UI Elements")]
    public Canvas GlobalUI;
    public MulliganController MulliganP1;
    public MulliganController MulliganP2;
    public BoardController BoardController;


    public void Setup() {

        

        MulliganP1.Init()
            MulliganP2.Init()
    }

}
