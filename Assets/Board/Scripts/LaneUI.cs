using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneUI : MonoBehaviour
{
    public Lane MyLane { get; set; }

    public Image LaneColourImage;

    public void SetUp(Lane _l)
    {
        MyLane = _l;
        LaneColourImage.color = MyLane.Type.LaneColor;
    }

}
