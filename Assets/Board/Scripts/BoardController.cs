using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private LaneUI LanePrefab;
 
    [Header("Data")]
    [SerializeField] private BoardData boardData;

    public void SetUp(BoardData _boardData)
    {
        if (!_boardData)
        {
            Debug.LogError("Board not set!");
            return;
        }
        boardData = _boardData;
    }

    public void InstantiateBoard()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (Lane l in boardData.Lanes)
        {
            l.SetPrefab(LanePrefab.gameObject);
            LaneUI instantiatedLane = Instantiate(l.Prefab, transform).GetComponent<LaneUI>();
            instantiatedLane.SetUp(l);
        }
    }

    public bool CheckCardPlayability(LaneType _lanetype, Player.Type _playerType)
    {
        foreach (Lane lane in boardData.Lanes)
        {
            if(lane.Type == _lanetype && lane.HasFreeSlot(_playerType))
            {
                return true;
            }
        }
        return false;
    }

}
