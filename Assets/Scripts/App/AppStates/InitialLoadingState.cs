using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.Controllers;
using BricksAndBalls.UI.View;
using UnityEngine;

namespace BricksAndBalls.App.States
{
   public class InitialLoadingState: AppState
   {
      private const float loadTime = 5.0f;
      private float timeSpent;

      private LoadingScreen loadingScreen;
      private LoadingScreenData loadingData;

      public InitialLoadingState(StateMachine<AppState> stateMachine) : base(stateMachine)
      {
         timeSpent = 0.0f;
         loadingData = new LoadingScreenData();
         loadingScreen = ScreenController.Instance.CreateScreen<LoadingScreen, LoadingScreenData>();
      }

      public override void Update(float deltaTime)
      {
         if(timeSpent >= loadTime)
         {
            OnLoadCompleted();
            return;
         }

         timeSpent += deltaTime;

         loadingData.loadProgress = Mathf.InverseLerp(0.0f, loadTime, timeSpent);
         loadingScreen.SetData(loadingData);
      }

      private void OnLoadCompleted()
      {
         stateMachine.ChangeState<MainMenuState>();
      }

      public override void OnExit()
      {
         base.OnExit();
         GameObject.Destroy(loadingScreen.gameObject);
      }
   }
}