using BricksAndBalls.Game.Renderers;
using BricksAndBalls.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Game.States
{
   class LevelSetupState: GameState
   {
      private float loadTime = 0.3f;
      private List<GameElementRendererBase> drawers;

      public LevelSetupState(GameLogicStateMachine stateMachine) : base(stateMachine)
      {
         drawers = new List<GameElementRendererBase>();
      }

      public override void OnEnter()
      {
         base.OnEnter();

         var elements = stateMachine.Level.GenerateObstacles(stateMachine.Level.LevelData.BrickData);
         stateMachine.Level.InitObstacles(elements);

         foreach(var element in elements)
         {
            CreateDrawers(element);
         }

         stateMachine.GameOverAction += (x) =>
         {
            foreach(var drawer in drawers)
            {
               if(drawer != null && drawer.gameObject != null)
                  drawer.Destroy();
            }
         };

      }

      public override void Update(float deltaTime)
      {
         base.Update(deltaTime);

         loadTime -= Time.deltaTime;

         if(loadTime <= 0.0f)
         {
            stateMachine.ChangeState<WaitForInput>();
         }
      }

      private void CreateDrawers(GameElementGenerationInfo gameElementGenerationData)
      {
         if(gameElementGenerationData.NeedsDrawer)
         {
            var gameElement = gameElementGenerationData.GameElement;
            var drawer = GameElementRenderController.Instance.CreateRenderer(gameElement.GetType(), gameElement.GetData());
            drawers.Add(drawer);

            if(drawer != null)
            {
               gameElement.AddOnDataUpdatedCallback((x) =>
               {
                  drawer?.SetData(x);
               });
               gameElement.AddOnDestroyCallback(() =>
               {
                  drawers.Remove(drawer);
                  if(drawer != null && drawer.gameObject != null)
                     drawer.Destroy();
               });
            }
         }
      }

   }
}


