using BricksAndBalls.Data.GameData;
using BricksAndBalls.Game.States;
using System.Linq;
using UnityEngine;

namespace BricksAndBalls.App.States
{
   public class GameplayState: AppState
   {
      private StateMachine<GameState> gameLogicStateMachine;

      public GameplayState(StateMachine<AppState> stateMachine) : base(stateMachine)
      {
         //TODO: replace with level selecting
         var selectableElements = Resources.LoadAll<LevelDataHolder>("Levels");

         gameLogicStateMachine = new GameLogicStateMachine(selectableElements.First().gameLevelData, OnGameEnded);
      }

      public override void Update(float deltaTime)
      {
         base.Update(deltaTime);

         if(gameLogicStateMachine == null)
            return;

         gameLogicStateMachine.Update(deltaTime);
      }

      private void OnGameEnded(GameResultData result)
      {
         gameLogicStateMachine.Destroy();
         gameLogicStateMachine = null;

         var adjustedHighscore = result.HighScore * result.Multiplier + PlayerPrefs.GetInt("HighScore", 0);
         PlayerPrefs.SetInt("HighScore", adjustedHighscore);

         var state = stateMachine.CreateState<HighScoreState>();
         state.HighScore = adjustedHighscore;
         stateMachine.ChangeState(state);
      }
   }
}
