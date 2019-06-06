using System;
using UnityEngine;

public static class CardController
{
    #region Events

    public static Action<CardData> OnPlaced;

    #endregion

    public static CardData ResetOriginalLife(CardData _data)
    {
        _data.ResetOriginalLife();
        return _data;
    }

    public static CardData UpdateLife(CardData _data, int _value)
    {
        _data.Life = _value;
        if (_data.OnDataChanged != null)
            _data.OnDataChanged(_data);
        return _data;
    }

    public static GameplayAttackAction AttackCard(CardData _attackingCard, CardData _defendingCard)
    {
        UpdateLife(_defendingCard, _defendingCard.Life - _attackingCard.Attack);

        return GameplayAttackAction.CreateCardAttackAction(_attackingCard, _defendingCard);
    }

    public static GameplayAttackAction AttackPlayer(CardData _attackingCard, PlayerData _defendingPlayer)
    {
        PlayerController.SetLife(_defendingPlayer, _defendingPlayer.CurrentLife - _attackingCard.Attack);

        return GameplayAttackAction.CreatePlayerAttackAction(_attackingCard, _defendingPlayer);
    }
}