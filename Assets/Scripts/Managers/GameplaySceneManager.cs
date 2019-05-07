using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneManager : MonoBehaviour {

    private static GameplaySceneManager i;

    public PlayerView player1, player2;
    public BoardController BoardCtrl;
    public CardViewManager cardViewManager;

    [Header("UI Elements")]
    public UIManager GlobalUI;
    public MulliganController mulliganP1;
    public MulliganController mulliganP2;
    public GameFlowButtonController gameFlowButton;

    private void Awake()
    {
        i = this;
    }

    public void Setup() {
        cardViewManager.Init();
    }

    public static GameFlowButtonController GetGameFlowButton()
    {
        return i.gameFlowButton;
    }

    public static BoardController GetBoardController()
    {
        return i.BoardCtrl;
    }

    public static CardViewManager GetCardViewManager()
    {
        return i.cardViewManager;
    }

    public static MulliganController GetMulliganController(PlayerData.Type _type)
    {
        switch (_type)
        {
            case PlayerData.Type.one:
                return i.mulliganP1;
            case PlayerData.Type.two:
                return i.mulliganP2;
        }
        return null;
    }

    public static PlayerView GetPlayer(PlayerData.Type _player)
    {
        switch (_player)
        {
            case PlayerData.Type.one:
                return i.player1;
            case PlayerData.Type.two:
                return i.player2;
        }
        return null;
    }
}
