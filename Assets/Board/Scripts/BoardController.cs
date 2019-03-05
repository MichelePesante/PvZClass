using UnityEngine;
using System.Collections.Generic;

public class BoardController : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField]
    private LaneViewController LanePrefab;

    [Header("Data")]
    [SerializeField]
    private BoardData boardData;

    public const int CardSlotsPerPlayer = 2;

    List<LaneViewController> laneUIs;

    public void SetUp(BoardData _boardData = null) {
        if (_boardData) {
            boardData = _boardData;
        } else {
            if (!boardData) {
                Debug.LogError(name + " has no board data!!");
                return;
            }
        }
        laneUIs = new List<LaneViewController>();
    }

    public void InstantiateBoard() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (LaneData l in boardData.Lanes) {
            LaneViewController instantiatedLane = Instantiate(LanePrefab, transform).SetUp(l, CardSlotsPerPlayer);
            laneUIs.Add(instantiatedLane);
        }
    }

    public bool CheckCardPlayability(CardViewController _cardToCheck) {
        foreach (LaneViewController laneUI in laneUIs) {
            if (LaneController.CheckCardPlayability(laneUI.Data, _cardToCheck.Data))
                return true;
        }
        return false;
    }
}