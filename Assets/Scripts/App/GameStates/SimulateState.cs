using BricksAndBalls.Data.Screens;
using BricksAndBalls.Game.Renderers;
using BricksAndBalls.GameLogic;
using BricksAndBalls.GameLogic.Elements;
using BricksAndBalls.UI.Controllers;
using BricksAndBalls.UI.View;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Game.States
{
   class SimulateState: GameState
   {
      private LevelUIData screenData;
      private LevelUI screen;
      private Level level;
      private List<(Ball, GameElementRendererBase)> ballsSpawned;
      public SimulateState(GameLogicStateMachine stateMachine) : base(stateMachine)
      {
         level = stateMachine.Level;
         ballsSpawned = new List<(Ball, GameElementRendererBase)>();
      }

      public override void OnEnter()
      {
         base.OnEnter();

         level.ResetRound();
         level.AddOnGameObjectGeneratedCallback(OnGameElementGenerated);
         screen = ScreenController.Instance.CreateScreen<LevelUI, LevelUIData>();
         screenData = new LevelUIData();
      }

      public override void Update(float deltaTime)
      {
         base.Update(deltaTime);
         level.Update(deltaTime);


         for(int i = 0; i < ballsSpawned.Count; ++i)
         {
            ballsSpawned[i].Item2.SetData(ballsSpawned[i].Item1.GetData());
         }

         screenData.Score = level.HighScoreTracker.GetHighScore();
         screenData.NumberOfTries = level.LevelData.Tries - level.RoundNumber;
         screen.SetData(screenData);

         if(level.IsWinConditionAchieved)
         {
            stateMachine.ChangeState<SelectMultiplierState>();
            return;
         }

         if(level.IsLoseConditionAchieved)
         {
            var triesLeft = level.LevelData.Tries - level.RoundNumber; 

            if(triesLeft > 0)
               stateMachine.ChangeState<WaitForInput>();
            else
               stateMachine.ChangeState<SelectMultiplierState>();
         }
      }

      public void Destroy()
      {
         foreach(var ball in ballsSpawned)
         {
            if(ball.Item2 != null)
            {
               ball.Item2.Destroy();
               ball.Item1.Destroy();
            }
         }
      }

      public override void OnExit()
      {
         base.OnExit();
         Destroy();
         level.RemoveOnGameObjectGeneratedCallback(OnGameElementGenerated);
         GameObject.Destroy(screen.gameObject);
      }

      public void OnGameElementGenerated(GameElementGenerationInfo data)
      {
         if(!(data.GameElement is Ball))
            return;

         var type = data.GameElement.GetType();
         var drawer = GameElementRenderController.Instance.CreateRenderer(type, data.GameElement.GetData());
         ballsSpawned.Add((data.GameElement as Ball, drawer));
      }
   }
}
