using BricksAndBalls.App.States;
using BricksAndBalls.Data.Game;
using BricksAndBalls.Data.GameData;
using BricksAndBalls.Game.Renderers;
using BricksAndBalls.GameLogic;
using BricksAndBalls.GameLogic.Elements;
using System.Collections.Generic;

namespace BricksAndBalls.Game.States
{
   public class GameLogicStateMachine : StateMachine<GameState>
   {
      public delegate void GameOverDelegate(GameResultData data);

      public Level Level { get; private set; }
      public GameOverDelegate GameOverAction;
      public Dictionary<GameElement, GameElementRendererBase> GameElements;

      public GameLogicStateMachine(GameLevelData levelData,GameOverDelegate gameOverAction)
      {
         this.GameOverAction = gameOverAction;
         this.Level = new Level(levelData);

         ChangeState<LevelSetupState>();
      }

      public void TriggerGameOver()
      {
         GameOverAction?.Invoke(Level.GetResultData());
      }
   }
}
