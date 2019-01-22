using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Board", menuName = "Board/Board")]
public class BoardData : ScriptableObject
{
    public BoardThemeType Theme;
    public List<Lane> Lanes;
}