using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LogManager : MonoBehaviour
{
    public TextMeshProUGUI TextPrefab;
    [SerializeField] Transform content;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        DeckViewController.OnCardMoved += OnCardMoved;
        //DeckViewController.OnCardsMoved += OnCardsMoved;
    }

    void OnCardMoved(GameplayMovementAction gameplayAction)
    {
        CreateText(gameplayAction);
    }

    void OnCardsMoved(List<GameplayMovementAction> gameplayActions)
    {
        foreach (GameplayMovementAction gameplayAction in gameplayActions)
        {
            CreateText(gameplayAction);
        }
    }

    void CreateText(GameplayMovementAction _gameplayAction)
    {
        TextMeshProUGUI text = Instantiate(TextPrefab, content);
        text.transform.SetAsFirstSibling();
        text.text = _gameplayAction.ToString();
    }
}
