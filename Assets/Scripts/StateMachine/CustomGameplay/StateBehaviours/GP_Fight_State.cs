using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public class GP_Fight_State : GP_BaseState
    {
        DeckData trashDeck;

        public override void Enter()
        {
            trashDeck = CardViewManager.GetTrashDeckView().Data;

            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(false);

            context.PlayerOne.Data.OnDeath += context.OnMatchEnd;
            context.PlayerTwo.Data.OnDeath += context.OnMatchEnd;

            //context.BoardCtrl.StartCoroutine(CombatRoutine());
            Combat();
            CardViewManager.ExecuteAttackActions(() => CardViewManager.DoMovementActions(handleActionsCompleteCallback));
        }

        void Combat()
        {
            foreach (LaneViewController lane in context.BoardCtrl.laneUIs)
            {
                CombatManager.LaneCombat(lane, context.PlayerOne, context.PlayerTwo, trashDeck);
            }
        }

        ////Routine
        //IEnumerator CombatRoutine()
        //{
        //    //for each lane wait lane
        //    foreach (LaneViewController lane in context.BoardCtrl.laneUIs)
        //    {
        //        yield return context.BoardCtrl.StartCoroutine(LaneRoutine(lane));
        //    }

        //    CardViewManager.ExecuteAttackActions(() => CardViewManager.DoMovementActions(handleActionsCompleteCallback));            
        //}

        //IEnumerator LaneRoutine(LaneViewController _laneView)
        //{
        //    //TODO generate gameplay actions events for the view manager.
        //    //TODO repeat attack pattern for every card in lane instead of just the one in front.
        //    //TODO check card death if life goes to 0 or below.
        //    //TODO animations or something visual.

        //    //player A always attacks first!
        //    DeckViewController p1CardsView = _laneView.PlayerASlotsView;
        //    DeckViewController p2CardsView = _laneView.PlayerBSlotsView;

        //    CardViewController p1CurrentCard = null, p2CurrentCard = null;

        //    for (int i = _laneView.PlayerASlotsView.Data.MaxCards; i >= 0; i--)
        //    {
        //        if (i < p1CardsView.Data.Cards.Count /*&& p1CurrentCard == null*/)
        //            p1CurrentCard = CardViewManager.GetCardViewByCardData(p1CardsView.Data.Cards[i]);

        //        if (i < p2CardsView.Data.Cards.Count && p2CurrentCard.Data.Life > 0)
        //            p2CurrentCard = CardViewManager.GetCardViewByCardData(p2CardsView.Data.Cards[i]);

        //        //p1 attack
        //        //if cards on both sides
        //        if (p1CurrentCard && p2CurrentCard)
        //        {
        //            //if defender has 0 health go behind
        //            if (p2CurrentCard.Data.Life <= 0)
        //            {
        //                if (i - 1 < p1CardsView.Data.Cards.Count)
        //                    p2CurrentCard = CardViewManager.GetCardViewByCardData(p2CardsView.Data.Cards[i]);
        //                else
        //                {
        //                    //Attack the player!
        //                    p1CurrentCard.DoAttackPlayer(ref context.PlayerTwo);
        //                }
        //            }
        //            else
        //            {
        //                //Attack the card!
        //                p1CurrentCard.DoAttackCard(ref p2CurrentCard);
        //                if (p2CurrentCard.Data.Life <= 0)
        //                {
        //                    CardData currentCardData = p2CurrentCard.Data;
        //                    p2CardsView.DoMoveFromMe(ref trashDeck, ref currentCardData, false);
        //                }
        //            }
        //        }
        //        else if (p1CurrentCard && !p2CurrentCard)
        //        {
        //            //Attack the player!
        //            p1CurrentCard.DoAttackPlayer(ref context.PlayerTwo);
        //        }

        //        //p2 attack
        //        //if cards on both sides
        //        if (p1CurrentCard && p2CurrentCard)
        //        {
        //            //if defender has 0 health go behind
        //            if (p1CurrentCard.Data.Life <= 0)
        //            {
        //                if (i - 1 < p2CardsView.Data.Cards.Count)
        //                    p1CurrentCard = CardViewManager.GetCardViewByCardData(p1CardsView.Data.Cards[i]);
        //                else
        //                {
        //                    //Attack the player!!
        //                    p2CurrentCard.DoAttackPlayer(ref context.PlayerOne);
        //                }
        //            }
        //            else
        //            {
        //                //Attack the card!
        //                p2CurrentCard.DoAttackCard(ref p1CurrentCard);
        //                if (p1CurrentCard.Data.Life <= 0)
        //                {
        //                    CardData currentCardData = p1CurrentCard.Data;
        //                    p1CardsView.DoMoveFromMe(ref trashDeck, ref currentCardData, false);
        //                }
        //            }
        //        }
        //        else if (!p1CurrentCard && p2CurrentCard)
        //        {
        //            //Attack the player!!
        //            p2CurrentCard.DoAttackPlayer(ref context.PlayerOne);
        //        }
        //    }

        //    yield return null;
        //}

        void handleActionsCompleteCallback()
        {
            context.GenericForwardCallBack();
        }

        public override void Exit()
        {
            context.PlayerOne.Data.OnDeath -= context.OnMatchEnd;
            context.PlayerTwo.Data.OnDeath -= context.OnMatchEnd;
            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(false);
        }
    }
}