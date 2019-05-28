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
            context.BoardCtrl.StartCoroutine(CombatRoutine());
        }

        public override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Space))
                context.GenericForwardCallBack();
        }

        //Routine
        IEnumerator CombatRoutine()
        {
            //for each lane wait lane
            foreach (LaneViewController lane in context.BoardCtrl.laneUIs)
            {
                yield return context.BoardCtrl.StartCoroutine(LaneRoutine(lane));
            }

            context.GameFlowButton.ToggleGoNextButton(true);
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

            if (p1CardsView.Data.Cards.Count > 0)
                p1CurrentCard = CardViewManager.GetCardViewByCardData(p1CardsView.Data.Cards[p1CardsView.Data.Cards.Count - 1]);
            if (p2CardsView.Data.Cards.Count > 0)
                p2CurrentCard = CardViewManager.GetCardViewByCardData(p2CardsView.Data.Cards[p2CardsView.Data.Cards.Count - 1]);

            //p1 attack
            if (p1CurrentCard && p2CurrentCard)
            {
                //if cards on both sides
                CardController.UpdateLife(p2CurrentCard.Data, p2CurrentCard.Data.Life - p1CurrentCard.Data.Attack);
                if (p2CurrentCard.Data.Life <= 0)
                {
                    CardData currentCardData = p2CurrentCard.Data;
                    p2CardsView.DoMove(ref trashDeck, ref currentCardData);
                }
            }
            else if (p1CurrentCard && !p2CurrentCard)
            {
                p1CardsView.Data.Player.CurrentLife -= p1CurrentCard.Data.Attack;
            }

            //p2 attack
            if (p1CurrentCard && p2CurrentCard)
            {
                //if cards on both sides
                CardController.UpdateLife(p1CurrentCard.Data, p1CurrentCard.Data.Life - p2CurrentCard.Data.Attack);
                if (p1CurrentCard.Data.Life <= 0)
                {
                    CardData currentCardData = p1CurrentCard.Data;
                    p1CardsView.DoMove(ref trashDeck, ref currentCardData);
                }
            }
            else if (!p1CurrentCard && p2CurrentCard)
            {
                p2CardsView.Data.Player.CurrentLife -= p2CurrentCard.Data.Attack;
            }

            yield return null;
        }

        public override void Exit()
        {
            context.GameFlowButton.GoToNextPhase();
            context.GameFlowButton.ToggleGoNextButton(false);
        }
    }
}