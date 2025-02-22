using BricksAndBalls.App.States;

namespace BricksAndBalls.Game.States
{
   public abstract class GameState: StateMachineStateBase
   {
      protected GameLogicStateMachine stateMachine;

      protected GameState(GameLogicStateMachine stateMachine)
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
