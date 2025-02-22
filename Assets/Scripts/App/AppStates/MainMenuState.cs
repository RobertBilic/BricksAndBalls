using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.Controllers;
using BricksAndBalls.UI.View;
using UnityEngine;

namespace BricksAndBalls.App.States
{
   class MainMenuState: AppState
   {
      private MainMenuScreen mainMenu;
      private MainMenuScreenData screenData;

      public MainMenuState(StateMachine<AppState> stateMachine) : base(stateMachine)
      {
         mainMenu = ScreenController.Instance.CreateScreen<MainMenuScreen, MainMenuScreenData>();
         screenData = new MainMenuScreenData()
         {
            onHighScoreAction = ChangeToHighScoreState,
            onPlayAction = ChangeToPlayState
         };
      }

      public override void OnEnter()
      {
         base.OnEnter();
         mainMenu.SetData(screenData);
      }

      public override void OnExit()
      {
         base.OnExit();
         GameObject.Destroy(mainMenu.gameObject);
      }

      private void ChangeToPlayState()
      {
         stateMachine.ChangeState<GameplayState>();
      }

      private void ChangeToHighScoreState()
      {
         var highScoreGame = stateMachine.CreateState<HighScoreState>();
         var highscore = PlayerPrefs.GetInt("HighScore", 0);
         highScoreGame.HighScore = highscore;
         stateMachine.ChangeState(highScoreGame);
      }
   }
}
