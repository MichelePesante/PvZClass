using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private void Start() {
        LoadCards();
    }

    [Serializable]
    public class Database {
        public List<CardData> Cards;
    }

    public void LoadCards() {
        TextAsset mytxtData = (TextAsset)Resources.Load("Cards");
        string txt = mytxtData.text;
        Database db = new Database();
        JsonUtility.FromJsonOverwrite(txt, db);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
