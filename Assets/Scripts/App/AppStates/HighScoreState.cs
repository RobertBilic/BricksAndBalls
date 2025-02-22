using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.Controllers;
using BricksAndBalls.UI.View;
using BricksAndBalls.Util;
using UnityEngine;
using System.Linq;

namespace BricksAndBalls.App.States
{
   class HighScoreState: AppState
   {
      public int HighScore;

      private HighScoreScreen screen;

      public HighScoreState(StateMachine<AppState> stateMachine) : base(stateMachine)
      {

      }

      public override void OnEnter()
      {
         base.OnEnter();

         screen = ScreenController.Instance.CreateScreen<HighScoreScreen, HighscoreScreenData>();
         var scores = new System.Collections.Generic.List<HighScoreEntry>();
         var playerEntry = new HighScoreEntry()
         {
            PlayerName = "Player",
            Score = HighScore
         };
         scores.Add(playerEntry);

         var minGeneratedScore = Mathf.Max(HighScore / 2, 300);
         var maxGeneratedScore = Mathf.Min(HighScore * 2, 1000);

         for(int i=0;i<99;++i)
         {
            HighScoreEntry entry = new HighScoreEntry()
            {
               PlayerName = RandomNameGen.GetRandomName(),
               Score = Random.Range(minGeneratedScore, maxGeneratedScore)
            };

            scores.Add(entry);
         }

         scores = scores.OrderByDescending(x => x.Score).ToList();
         var playerIndex = scores.IndexOf(playerEntry);
         var screenData = new HighscoreScreenData()
         {
            PlayAgainAction = GoToMainMenu,
            PlayerRank = playerIndex + 1,
            Scores = scores
         };

         screen.SetData(screenData);
      }

      public override void OnExit()
      {
         base.OnExit();


         GameObject.Destroy(screen.gameObject);
      }

      private void GoToMainMenu()
      {
         stateMachine.ChangeState<MainMenuState>();
      }
   }
}
