using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Image IpsterBG;
    public Color IpsterColor;

    public Image AlcoolBG;
    public Color AlcoolColor;

    public Image CommonBG;

    public void ChangeFactionBG(CardData.Faction _faction) {
        switch (_faction) {
            case CardData.Faction.Hipster:
                IpsterBG.color = new Color(IpsterBG.color.r, IpsterBG.color.g, IpsterBG.color.b, 1);
                AlcoolBG.color = new Color(AlcoolBG.color.r, AlcoolBG.color.g, AlcoolBG.color.b, 0);
                CommonBG.color = IpsterColor;
                break;
            case CardData.Faction.Alcool:
                AlcoolBG.color = new Color(AlcoolBG.color.r, AlcoolBG.color.g, AlcoolBG.color.b, 1);
                IpsterBG.color = new Color(IpsterBG.color.r, IpsterBG.color.g, IpsterBG.color.b, 0);
                CommonBG.color = AlcoolColor;
                break;
        }
    }
}
