using UnityEngine;
using System.Collections.Generic;

public class BoardController : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField]
    private LaneViewController LanePrefab;

    [Header("Data")]
    [SerializeField]
    private BoardData boardData;

    [Header("Prefabs")]
    [SerializeField]
    private Transform laneContainer;

    public const int CardSlotsPerPlayer = 2;

    public List<LaneViewController> laneUIs;
    public DeckViewController[] deckViews;

    public void SetUp(BoardData _boardData = null, bool _instantiate = false) {
        if (_boardData) {
            boardData = _boardData;
        } else {
            if (!boardData) {
                Debug.LogError(name + " has no board data!!");
                return;
            }
        }
        laneUIs = new List<LaneViewController>();

        if (_instantiate)
            InstantiateBoard();
    }

    public void InstantiateBoard() {
        for (int i = 0; i < laneContainer.transform.GetComponentsInChildren<LaneViewController>().Length; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        deckViews = new DeckViewController[boardData.Lanes.Count * 2];

        for (int i = 0; i < boardData.Lanes.Count; i++)
        {
            LaneViewController instantiatedLane = Instantiate(LanePrefab, laneContainer).SetUp(boardData.Lanes[i], CardSlotsPerPlayer);
            deckViews[i * 2] = instantiatedLane.PlayerASlotsView;
            deckViews[i * 2 + 1] = instantiatedLane.PlayerBSlotsView;
            laneUIs.Add(instantiatedLane);
        }
    }

    public bool CheckCardPlayability(CardViewController _cardToCheck) {

        if(_cardToCheck.GetPlayerOwner().Data.CurrentEnergy < _cardToCheck.Data.Cost)
        {
            return false;
        }

        foreach (LaneViewController laneUI in laneUIs) {
            if (LaneController.CheckCardPlayability(laneUI.Data, _cardToCheck.Data))
                return true;
        }

        return false;
    }
}