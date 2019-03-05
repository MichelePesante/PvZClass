using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour {

    private static GameplaySceneManager i;

    public Player player1, player2;
    public BoardController BoardCtrl;
    [Header("UI Elements")]
    public UIManager GlobalUI;
    public MulliganController mulliganP1;
    public MulliganController mulliganP2;
    public HandDeckViewController playerOneHandUI;
    public HandDeckViewController playerTwoHandUI;

    private void Awake()
    {
        i = this;
    }

    public void Setup() {
    }

    public static BoardController GetBoardController()
    {
        return i.BoardCtrl;
    }

    public static MulliganController GetMulliganController(Player.Type _type)
    {
        switch (_type)
        {
            case Player.Type.one:
                return i.mulliganP1;
            case Player.Type.two:
                return i.mulliganP2;
        }
        return null;
    }
}
