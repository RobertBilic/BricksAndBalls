using BricksAndBalls.App.States;
using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.Controllers;
using BricksAndBalls.UI.View;
using UnityEngine;


namespace BricksAndBalls.Game.States
{
   class WaitForInput: GameState
   {
      private WaitingForInputScreen waitScreen;

      private const float holdDownTime = 0.3f;
      private float timeHeldDown;

      public WaitForInput(GameLogicStateMachine stateMachine) : base(stateMachine)
      {
         timeHeldDown = 0.0f;
      }

      public override void OnEnter()
      {
         base.OnEnter();

         waitScreen = ScreenController.Instance.CreateScreen<WaitingForInputScreen, ScreenData>();
      }

      public override void Update(float deltaTime)
      {
         base.Update(deltaTime);

         if(timeHeldDown >= holdDownTime)
         {
            stateMachine.ChangeState<AimState>();
            return;
         }

         if(Input.GetMouseButton(0))
         {
            timeHeldDown += Time.deltaTime;
         } else
         {
            timeHeldDown = 0.0f;
         }
      }

      public override void OnExit()
      {
         base.OnExit();

         GameObject.Destroy(waitScreen.gameObject);
      }
   }
}
