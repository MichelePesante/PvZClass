using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour {

    public Player player1, player2;
    public BoardController BoardCtrl;
    [Header("UI Elements")]
    public UIManager GlobalUI;
    public MulliganController mulliganP1;
    public MulliganController mulliganP2;


    public void Setup() {
    }

}
