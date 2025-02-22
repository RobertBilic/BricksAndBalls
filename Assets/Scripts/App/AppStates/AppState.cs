namespace BricksAndBalls.App.States
{
   public abstract class AppState: StateMachineStateBase
   {
      protected StateMachine<AppState> stateMachine;

      protected AppState(StateMachine<AppState> stateMachine)
      {
         this.stateMachine = stateMachine;
      }

      public override void OnEnter()
      {
         base.OnEnter();
      }

      public override void OnExit()
      {
         base.OnExit();
      }

      public override void Update(float deltaTime)
      {
         base.Update(deltaTime);
      }
   }
}
