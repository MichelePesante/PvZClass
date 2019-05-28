namespace StateMachine.Gameplay
{

    public class GP_PreLoop_State : GP_BaseState
    {

        public override void Enter()
        {
            context.BoardCtrl.InstantiateBoard();

            GameplaySceneManager.GetCardViewManager().Setup(context.BoardCtrl.deckViews);

            context.GenericForwardCallBack();
            context.GameFlowButton.Setup(context.GenericForwardCallBack);
        }
    }
}
