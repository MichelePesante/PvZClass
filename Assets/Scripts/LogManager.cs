using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LogManager : MonoBehaviour
{
    public TextMeshProUGUI TextPrefab;
    [SerializeField] Transform content;

    RectTransform rectTransform;
    Vector2 oldSize;
    Size currentSize;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        oldSize = rectTransform.sizeDelta;
        ResizeLog(Size.Close);
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

    public void ResizeLog(Size _size)
    {
        switch (_size)
        {
            case Size.Minimize:
                rectTransform.sizeDelta = oldSize;
                currentSize = Size.Minimize;
                break;
            case Size.Open:
                rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height);
                currentSize = Size.Open;
                break;
            case Size.Close:
                rectTransform.sizeDelta = Vector2.zero;
                currentSize = Size.Close;
                break;
            default:
                break;
        }
    }

    public void ResizeLog()
    {
        switch (currentSize)
        {
            case Size.Minimize:
                rectTransform.sizeDelta = new Vector2(Screen.width / 2, Screen.height);
                currentSize = Size.Open;
                break;
            case Size.Open:
                rectTransform.sizeDelta = Vector2.zero;
                currentSize = Size.Close;
                break;
            case Size.Close:
                rectTransform.sizeDelta = oldSize;
                currentSize = Size.Minimize;
                break;
            default:
                break;
        }
    }

    public enum Size
    {
        Close,
        Minimize,
        Open,
    }
}
