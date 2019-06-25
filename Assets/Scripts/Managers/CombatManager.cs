using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{
    private static CombatManager instance;

    public CombatManager()
    {
        if (instance == null)
            instance = this;
    }

    public static void LaneCombat(LaneViewController _laneView, PlayerView _playerAView, PlayerView _playerBView, DeckData _trashDeckData)
    {
        List<CardViewController> _playerACardViews = new List<CardViewController>();
        foreach (CardData cardData in _laneView.PlayerASlotsView.Data.Cards)
        {
            _playerACardViews.Add(CardViewManager.GetCardViewByCardData(cardData));
        }

        List<CardViewController> _playerBCardViews = new List<CardViewController>();
        foreach (CardData cardData in _laneView.PlayerBSlotsView.Data.Cards)
        {
            _playerBCardViews.Add(CardViewManager.GetCardViewByCardData(cardData));
        }

        int playerACardViewsCount = _playerACardViews.Count;
        int playerBCardViewsCount = _playerBCardViews.Count;
        int maxCards = playerACardViewsCount > playerBCardViewsCount ? playerACardViewsCount : playerBCardViewsCount;

        CardViewController currentAttackingCardView = null, currentDefendingCardView = null;

        for (int i = maxCards - 1; i >= 0; i--)
        {
            currentAttackingCardView = currentDefendingCardView = null;

            #region Player A attack
            //PLAYER A
            if (i < _playerACardViews.Count)
                currentAttackingCardView = _playerACardViews[i];

            for (int j = maxCards - 1; j >= 0; j--)
            {
                if (j < _playerBCardViews.Count && _playerBCardViews[j].Data.Life > 0)
                {
                    currentDefendingCardView = _playerBCardViews[j];
                    break;
                }
            }

            //card v card
            if (currentAttackingCardView && currentDefendingCardView)
            {
                currentAttackingCardView.DoAttackCard(ref currentDefendingCardView);
                if (currentDefendingCardView.Data.Life <= 0)
                {
                    CardData currentCardData = currentDefendingCardView.Data;
                    _laneView.PlayerBSlotsView.DoMoveFromMe(ref _trashDeckData, ref currentCardData, false);
                }
            }
            //card v player
            else if (currentAttackingCardView && !currentDefendingCardView)
            {
                currentAttackingCardView.DoAttackPlayer(ref _playerBView);
            } 
            #endregion

            currentAttackingCardView = currentDefendingCardView = null;

            #region playerB attack
            //PLAYER B
            if (i < _playerBCardViews.Count)
                currentAttackingCardView = _playerBCardViews[i];

            for (int j = maxCards - 1; j >= 0; j--)
            {
                if (j < _playerACardViews.Count && _playerACardViews[j].Data.Life > 0)
                {
                    currentDefendingCardView = _playerACardViews[j];
                    break;
                }
            }

            //card v card
            if (currentAttackingCardView && currentDefendingCardView)
            {
                currentAttackingCardView.DoAttackCard(ref currentDefendingCardView);
                if (currentDefendingCardView.Data.Life <= 0)
                {
                    CardData currentCardData = currentDefendingCardView.Data;
                    _laneView.PlayerASlotsView.DoMoveFromMe(ref _trashDeckData, ref currentCardData, false);
                }
            }
            //card v player
            else if (currentAttackingCardView && !currentDefendingCardView)
            {
                currentAttackingCardView.DoAttackPlayer(ref _playerAView);
            } 
            #endregion
        }
    }
}