using UnityEngine;
using System.Collections;

namespace StateMachine.Gameplay
{
    public class GP_Fight_State : GP_BaseState
    {

        public override void Enter()
        {
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
            DeckData p1Cards = _laneView.Data.playerAPlacedDeck;
            DeckData p2Cards = _laneView.Data.playerBPlacedDeck;

            CardData p1CurrentCard = null, p2CurrentCard = null;

            if (p1Cards.Cards.Count > 0)
                p1CurrentCard = p1Cards.Cards[p1Cards.Cards.Count - 1];
            if (p2Cards.Cards.Count > 0)
                p2CurrentCard = p2Cards.Cards[p2Cards.Cards.Count - 1];

            //p1 attack
            if (p1CurrentCard && p2CurrentCard)
            {
                //if cards on both sides
                CardController.UpdateLife(p2CurrentCard, p2CurrentCard.Life - p1CurrentCard.Attack);
            }
            else if (p1CurrentCard && !p2CurrentCard)
            {
                p1Cards.Player.Life -= p1CurrentCard.Attack;
            }

            //p2 attack
            if (p1CurrentCard && p2CurrentCard)
            {
                //if cards on both sides
                CardController.UpdateLife(p1CurrentCard, p1CurrentCard.Life - p2CurrentCard.Attack);
            }
            else if (!p1CurrentCard && p2CurrentCard)
            {
                p2Cards.Player.Life -= p2CurrentCard.Attack;
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