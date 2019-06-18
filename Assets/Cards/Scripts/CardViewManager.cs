using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardViewManager : MonoBehaviour
{
    private static CardViewManager instance;

    [SerializeField]
    private CardViewController cardPrefab;
    [SerializeField]
    private DeckViewController p1DeckView;
    [SerializeField]
    private DeckViewController p2DeckView;
    [SerializeField]
    private DeckViewController p1HandView;
    [SerializeField]
    private DeckViewController p2HandView;
    [SerializeField]
    private DeckViewController trashDeckView;
    DeckViewController[] boardDeckViews;

    List<CardViewController> instantiatedCards = new List<CardViewController>();

    public void Init()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DeckViewController.OnCardMoved += HandleOnCardMoved;
        DeckViewController.OnCardsMoved += HandleOnCardsMoved;
        CardViewController.OnAttack += HandleOnCardAttack;
    }

    public void Setup(DeckViewController[] _boardDeckViews)
    {
        boardDeckViews = _boardDeckViews;
    }

    #region Attack
    Queue<GameplayAttackAction> attackActions = new Queue<GameplayAttackAction>();

    private void HandleOnCardAttack(GameplayAttackAction _attackAction)
    {
        attackActions.Enqueue(_attackAction);
    }

    private void ExecuteAttackAction(GameplayAttackAction _attackAction, Action _callback = null)
    {
        CardViewController attackingCardView = null;
        CardViewController defendingCardView = null;
        PlayerView defendingPlayerView = null;

        if (_attackAction.attackingCardData)
        {
            attackingCardView = GetCardViewByCardData(_attackAction.attackingCardData);
        }
        if (_attackAction.defendingCardData)
        {
            defendingCardView = GetCardViewByCardData(_attackAction.defendingCardData);
        }
        if (_attackAction.defendingPlayerData != null)
        {
            defendingPlayerView = GameplaySceneManager.GetPlayer(_attackAction.defendingPlayerData.CurrentType);
        }

        if (defendingCardView)
        {
            RectTransform tempRectTransform = attackingCardView.transform as RectTransform;
            tempRectTransform.DOMove(defendingCardView.transform.position, 0.5f).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo).OnComplete(() => { if (_callback != null) _callback(); });
        }
        else if (defendingPlayerView)
        {
            RectTransform tempRectTransform = attackingCardView.transform as RectTransform;
            tempRectTransform.DOMove(defendingPlayerView.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnComplete(() => { if (_callback != null) _callback(); });
        }
    }

    public static void ExecuteAttackActions(Action _callBack = null)
    {
        if (instance.attackActions.Count > 0)
            instance.ExecuteAttackAction(instance.attackActions.Dequeue(), () => ExecuteAttackActions(_callBack));
        else if (_callBack != null)
        {
            _callBack.Invoke();
        }
    }
    #endregion

    #region Move
    List<GameplayMovementAction> movementActions = new List<GameplayMovementAction>();

    public static void DoMovementActions(Action _OnActionsCompleteCallback)
    {
        if (instance.movementActions != null && instance.movementActions.Count > 0)
        {
            foreach (GameplayMovementAction action in instance.movementActions)
            {
                instance.MoveCard(action);
            }

            instance.movementActions.Clear();
        }

        if (_OnActionsCompleteCallback != null)
            _OnActionsCompleteCallback();
    }

    private void HandleOnCardsMoved(List<GameplayMovementAction> actions)
    {
        foreach (GameplayMovementAction action in actions)
        {
            if (action.instantExecute)
                MoveCard(action);
            else
                movementActions.Add(action);
        }
    }

    private void HandleOnCardMoved(GameplayMovementAction action)
    {
        if (action.instantExecute)
            MoveCard(action);
        else
            movementActions.Add(action);
    }

    private void MoveCard(GameplayMovementAction action)
    {
        //TODO controllare il deck a cui va aggiunto
        DeckViewController deckFrom = GetDeckViewControllerByDeckData(action.deckDataFrom);
        DeckViewController deckTo = GetDeckViewControllerByDeckData(action.deckDataTo);
        CardData changedCardData = action.cardData;

        //Se non c'è un deck da cui la carta deriva la istanzio a prescindere
        if (deckFrom == null)
        {
            switch (deckTo.CurrentViewType)
            {
                case DeckViewController.ViewType.covered:
                    AddCardToDeck(deckTo, changedCardData);
                    break;
                case DeckViewController.ViewType.visible:
                    AddCardToDeck(deckTo, changedCardData);
                    break;
                case DeckViewController.ViewType.none:
                    break;
            }
            return;
        }

        //Se non c'è un deck destinatario o il deck è non visibile la distruggo a prescindere
        if (deckTo == null || deckTo.CurrentViewType == DeckViewController.ViewType.none)
        {
            for (int i = 0; i < instantiatedCards.Count; i++)
            {
                if (changedCardData.CompareIndex(instantiatedCards[i].Data.CardIndex))
                {
                    CardViewController cardToDestroy = instantiatedCards[i];
                    instantiatedCards.RemoveAt(i);

                    if (boardDeckViews != null && deckFrom != null && Array.Exists(boardDeckViews, deck => deck == deckFrom) && deckTo == trashDeckView)
                    {
                        Material mat = Instantiate(cardToDestroy.Frame.material);
                        cardToDestroy.Frame.material = mat;
                        mat.DOFloat(1, "_DissolvePercent", 0.5f).OnComplete(() => Destroy(cardToDestroy.gameObject));
                    }
                    else
                        Destroy(cardToDestroy.gameObject);
                    return;
                }
            }
            return;
        }

        //Se c'è da spostare una carta da un deck ad un altro
        for (int i = 0; i < instantiatedCards.Count; i++)
        {
            if (changedCardData.CompareIndex(instantiatedCards[i].Data.CardIndex))
            {

                if (deckFrom.CurrentViewType == DeckViewController.ViewType.covered && (deckTo == p1HandView || deckTo == p2HandView))
                {
                    //Se la carta esiste gìà la sposto
                    instantiatedCards[i].transform.DOMove(deckTo.transform.position, 1f).OnComplete(() =>
                    {
                        instantiatedCards[i].transform.SetParent(deckTo.transform);
                    });
                }
                else if ((deckFrom == p1HandView || deckFrom == p2HandView) && Array.Exists(boardDeckViews, deck => deck == deckTo))
                {
                    instantiatedCards[i].transform.SetParent(deckTo.transform);
                    if (deckTo.transform.childCount == 2)
                        instantiatedCards[i].transform.SetSiblingIndex((deckTo.Data.Player.CurrentType == PlayerData.Type.one) ? 0 : 1);

                    Sequence placeSequence = DOTween.Sequence();

                    Vector3 endScale = instantiatedCards[i].transform.localScale / 1.5f;
                    Tween scaleTween = instantiatedCards[i].transform.DOScale(endScale, 0.25f);

                    Tween shakeTween = instantiatedCards[i].transform.DOShakeScale(0.15f, Vector3.one);

                    placeSequence.Append(scaleTween);
                    placeSequence.Insert(0.20f, shakeTween);
                }
                else
                    instantiatedCards[i].transform.SetParent(deckTo.transform);
                return;
            }
        }

        //Se la carta non esisteva ancora la istanzio
        AddCardToDeck(deckTo, changedCardData);
    }

    private void AddCardToDeck(DeckViewController _deckToAdd, CardData _cardData)
    {
        CardViewController instantiatedCard = Instantiate(cardPrefab);
        instantiatedCards.Add(instantiatedCard);
        instantiatedCard.transform.parent = _deckToAdd.transform;
        instantiatedCard.transform.position = _deckToAdd.transform.position;
        instantiatedCard.Setup(_cardData, GameplaySceneManager.GetPlayer(_deckToAdd.Data.Player.CurrentType));
    }
    #endregion

    #region Getter
    public static DeckViewController GetHandDeck(PlayerData.Type currentType)
    {
        switch (currentType)
        {
            case PlayerData.Type.one:
                return instance.p1HandView;
            case PlayerData.Type.two:
                return instance.p2HandView;
        }
        return null;
    }

    public static DeckViewController GetPlayerDeck(PlayerData.Type currentType)
    {
        switch (currentType)
        {
            case PlayerData.Type.one:
                return instance.p1DeckView;
            case PlayerData.Type.two:
                return instance.p2DeckView;
        }
        return null;
    }

    public static DeckViewController GetDeckViewControllerByDeckData(DeckData _data)
    {
        if (_data == null)
            return null;
        if (instance.trashDeckView.Data == _data)
            return instance.trashDeckView;
        if (instance.p1DeckView.Data == _data)
            return instance.p1DeckView;
        if (instance.p2DeckView.Data == _data)
            return instance.p2DeckView;
        if (instance.p1HandView.Data == _data)
            return instance.p1HandView;
        if (instance.p2HandView.Data == _data)
            return instance.p2HandView;

        if (instance.boardDeckViews != null)
        {
            foreach (DeckViewController deckView in instance.boardDeckViews)
            {
                if (deckView.Data == _data)
                    return deckView;
            }
        }

        return null;
    }

    public static DeckViewController GetTrashDeckView()
    {
        return instance.trashDeckView;
    }

    public static CardViewController GetCardViewByCardData(CardData _data)
    {
        foreach (CardViewController card in instance.instantiatedCards)
        {
            if (card.Data.CompareIndex(_data.CardIndex))
            {
                return card;
            }
        }

        return null;
    }
    #endregion

    #region Effects

    #endregion

    private void OnDisable()
    {
        DeckViewController.OnCardMoved -= HandleOnCardMoved;
        DeckViewController.OnCardsMoved -= HandleOnCardsMoved;
    }
}