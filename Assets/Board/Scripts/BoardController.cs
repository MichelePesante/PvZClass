using UnityEngine;
using System.Collections.Generic;

public class BoardController : MonoBehaviour {
    [Header("Prefabs")]
    [SerializeField]
    private LaneUI LanePrefab;

    [Header("Data")]
    [SerializeField]
    private BoardData boardData;

    List<LaneUI> laneUIs;

    public void SetUp(BoardData _boardData = null) {
        if (_boardData) {
            boardData = _boardData;
        } else {
            if (!boardData) {
                Debug.LogError(name + " has no board data!!");
                return;
            }
        }
        laneUIs = new List<LaneUI>();
    }

    public void InstantiateBoard() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (Lane l in boardData.Lanes) {
            l.SetPrefab(LanePrefab.gameObject);
            LaneUI instantiatedLane = Instantiate(l.Prefab, transform).GetComponent<LaneUI>();
            instantiatedLane.SetUp(l);
            laneUIs.Add(instantiatedLane);
        }
    }

    public bool CheckCardPlayability(CardController _cardToCheck) {
        foreach (LaneUI laneUI in laneUIs) {
            if (laneUI.MyLane.CheckCardPlayability(_cardToCheck))
                return true;
        }
        return false;
    }

    public void ToggleBoardInteractability(bool _value, CardController _cardToCheck = null) {
        foreach (LaneUI laneUI in laneUIs) {
            if (_cardToCheck)
                laneUI.SetInteractability(_value, _cardToCheck);
            else
                laneUI.SetInteractability(_value);
        }
    }
}
