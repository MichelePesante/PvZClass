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

            context.BoardCtrl.StartCoroutine(CombatRoutine());
        }

        //Routine
        IEnumerator CombatRoutine()
        {
            //for each lane wait lane
            foreach (LaneViewController lane in context.BoardCtrl.laneUIs)
            {
                yield return context.BoardCtrl.StartCoroutine(LaneRoutine(lane));
            }

            context.GenericForwardCallBack();
        }

        IEnumerator LaneRoutine(LaneViewController _laneView)
        {
            //TODO generate gameplay actions events for the view manager.
            //TODO repeat attack pattern for every card in lane instead of just the one in front.
            //TODO check card death if life goes to 0 or below.
            //TODO animations or something visual.

            //player A always attacks first!
            DeckViewController p1CardsView = _laneView.PlayerASlotsView;
            DeckViewController p2CardsView = _laneView.PlayerBSlotsView;

            CardViewController p1CurrentCard = null, p2CurrentCard = null;

            for (int i = _laneView.PlayerASlotsView.Data.MaxCards; i >= 0; i--)
            {
                p1CurrentCard = null;
                if (i < p1CardsView.Data.Cards.Count)
                    p1CurrentCard = CardViewManager.GetCardViewByCardData(p1CardsView.Data.Cards[i]);

                p2CurrentCard = null;
                if (i < p2CardsView.Data.Cards.Count)
                    p2CurrentCard = CardViewManager.GetCardViewByCardData(p2CardsView.Data.Cards[i]);

                //p1 attack
                //if cards on both sides
                if (p1CurrentCard && p2CurrentCard)
                {
                    //if defender has 0 health go behind
                    if (p2CurrentCard.Data.Life <= 0)
                    {
                        if (i - 1 < p1CardsView.Data.Cards.Count)
                            p2CurrentCard = CardViewManager.GetCardViewByCardData(p2CardsView.Data.Cards[i]);
                        else
                        {
                            p2CardsView.Data.Player.CurrentLife -= p1CurrentCard.Data.Attack;
                        }
                    }
                    else
                    {
                        CardController.UpdateLife(p2CurrentCard.Data, p2CurrentCard.Data.Life - p1CurrentCard.Data.Attack);
                        if (p2CurrentCard.Data.Life <= 0)
                        {
                            CardData currentCardData = p2CurrentCard.Data;
                            p2CardsView.DoMove(ref trashDeck, ref currentCardData);
                        }
                    }
                }
                else if (p1CurrentCard && !p2CurrentCard)
                {
                    p2CardsView.Data.Player.CurrentLife -= p1CurrentCard.Data.Attack;
                }

                //p2 attack
                //if cards on both sides
                if (p1CurrentCard && p2CurrentCard)
                {
                    //if defender has 0 health go behind
                    if (p1CurrentCard.Data.Life <= 0)
                    {
                        if (i - 1 < p2CardsView.Data.Cards.Count)
                            p1CurrentCard = CardViewManager.GetCardViewByCardData(p1CardsView.Data.Cards[i]);
                        else
                        {
                            p2CardsView.Data.Player.CurrentLife -= p1CurrentCard.Data.Attack;
                        }
                    }
                    else
                    {
                        CardController.UpdateLife(p1CurrentCard.Data, p1CurrentCard.Data.Life - p2CurrentCard.Data.Attack);
                        if (p1CurrentCard.Data.Life <= 0)
                        {
                            CardData currentCardData = p1CurrentCard.Data;
                            p1CardsView.DoMove(ref trashDeck, ref currentCardData);
                        }
                    }
                }
                else if (!p1CurrentCard && p2CurrentCard)
                {
                    p1CardsView.Data.Player.CurrentLife -= p2CurrentCard.Data.Attack;
                }
            }

            yield return null;
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