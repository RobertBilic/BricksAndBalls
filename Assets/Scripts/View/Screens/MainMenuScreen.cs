using BricksAndBalls.Data.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.UI.View
{
   public class MainMenuScreen: Screen<MainMenuScreenData>
   {
      [SerializeField]
      private Button playButton;
      [SerializeField]
      private Button highScoreButton;

      public override void SetData(MainMenuScreenData data)
      {
         playButton.onClick.RemoveAllListeners();
         highScoreButton.onClick.RemoveAllListeners();

         playButton.onClick.AddListener(data.onPlayAction);
         highScoreButton.onClick.AddListener(data.onHighScoreAction);
      }
   }
}