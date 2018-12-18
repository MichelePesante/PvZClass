using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    [Header("UI References")]
    public Text Name;
    public Text Attack;
    public Text Life;
    public Text Cost;
    public Image Image;
	
    public void SetUp(CardController _cardController)
    {

    }

}
