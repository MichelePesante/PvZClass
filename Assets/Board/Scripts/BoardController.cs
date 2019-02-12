using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private LaneUI LanePrefab;
 
    [Header("Data")]
    [SerializeField] private BoardData boardData;

    //-- TESTING
    //[ContextMenu("Create Board")]
    //public void CreateBoard()
    //{
    //    SetUp(boardData);
    //    InstantiateBoard();
    //}
    //-- TESTING

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

}
