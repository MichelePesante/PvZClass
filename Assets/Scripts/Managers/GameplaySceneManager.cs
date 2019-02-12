using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour {

    private static GameplaySceneManager singleton;

    public Player player1, player2;
    public BoardController BoardCtrl;
    [Header("UI Elements")]
    public UIManager GlobalUI;
    public MulliganController mulliganP1;
    public MulliganController mulliganP2;

    private void Awake()
    {
        singleton = this;
    }

    public void Setup() {
    }

    public static BoardController GetBoardController()
    {
        return singleton.BoardCtrl;
    }

    public static MulliganController GetMulliganController(Player.Type _type)
    {
        switch (_type)
        {
            case Player.Type.one:
                return singleton.mulliganP1;
            case Player.Type.two:
                return singleton.mulliganP2;
        }
        return null;
    }
}
