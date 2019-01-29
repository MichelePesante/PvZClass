using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private RectTransform Mulligan;
    [SerializeField]
    private RectTransform Board;

    public void EnableMenu(PanelType _panelToEnable) {
        DisableAllPanels();
        switch (_panelToEnable) {
            case PanelType.Mulligan:
                Mulligan.gameObject.SetActive(true);
                break;
            case PanelType.Board:
                Board.gameObject.SetActive(true);
                break;
        }
    }

    public void DisableAllPanels() {
        Mulligan.gameObject.SetActive(false);
        Board.gameObject.SetActive(false);
    }
}

public enum PanelType {
    Mulligan,
    Board,
}
