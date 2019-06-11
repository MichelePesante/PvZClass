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
        DeckViewController.OnCardsMoved += OnCardsMoved;
    }

    void OnCardMoved(GameplayMovementAction gameplayMovementAction)
    {
        CreateText(gameplayMovementAction);
    }

    void OnCardsMoved(List<GameplayMovementAction> gameplayMovementActions)
    {
        foreach (GameplayMovementAction gameplayAction in gameplayMovementActions)
        {
            CreateText(gameplayAction);
        }
    }

    void CreateText(GameplayMovementAction _gameplayMovementAction)
    {
        TextMeshProUGUI text = Instantiate(TextPrefab, content);
        text.transform.SetAsFirstSibling();
        text.text = _gameplayMovementAction.ToString();
    }
}
