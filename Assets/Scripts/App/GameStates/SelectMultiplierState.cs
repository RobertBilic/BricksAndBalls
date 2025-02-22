using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.Controllers;
using BricksAndBalls.UI.View;
using UnityEngine;

namespace BricksAndBalls.Game.States
{
   class SelectMultiplierState: GameState
   {
      public SelectMultiplierState(GameLogicStateMachine stateMachine) : base(stateMachine)
      {
      }

      private SelectMultiplierScreen multiplierScreen;
      private SelectMultiplierScreenData screenData;

      public override void OnEnter()
      {
         base.OnEnter();

         int[] multipliers = new int[]
         {
            1,3,5
         };

         screenData = new SelectMultiplierScreenData()
         {
            Multipliers = multipliers,
            MultiplierSelected = OnSelectedMultiplier
         };

         multiplierScreen = ScreenController.Instance.CreateScreen<SelectMultiplierScreen, SelectMultiplierScreenData>();
         multiplierScreen.SetData(screenData);
      }

      public override void OnExit()
      {
         base.OnExit();

         GameObject.Destroy(multiplierScreen.gameObject);
      }

      private void OnSelectedMultiplier(int multiplier)
      {
         stateMachine.Level.Multiplier = multiplier;
         stateMachine.TriggerGameOver();
      }
   }
}
