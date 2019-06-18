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
        CardViewController.OnAttack += OnCardAttack;
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

    void OnCardAttack(GameplayAttackAction gameplayAttackAction)
    {
        CreateText(gameplayAttackAction);
    }

    void CreateText(IAction _gameplayAction)
    {
        TextMeshProUGUI text = Instantiate(TextPrefab, content);
        text.transform.SetAsFirstSibling();
        text.text = _gameplayAction.ToString();
    }

    public void ResizeLog()
    {

    }
}
