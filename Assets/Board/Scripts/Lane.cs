using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lane
{
    [SerializeField] private LaneType type;
    public LaneType Type { get { return type; } private set { type = value; } }
    private GameObject prefab;
    public GameObject Prefab { get { return prefab; } private set { prefab = value; } }

    public Lane(LaneType _type, GameObject _prefab)
    {
        Type = _type;
        Prefab = _prefab;
    }

    public void SetType(LaneType _s)
    {
        if (_s == null)
        {
            Debug.LogError("Lanes do not accept a null string for their type!");
            return;
        }
        Type = _s;
    }

    public void SetPrefab(GameObject _p)
    {
        if (!_p)
        {
            Debug.LogError("Lanes do not accept a null prefab!");
            return;
        }
        Prefab = _p;
    }
}