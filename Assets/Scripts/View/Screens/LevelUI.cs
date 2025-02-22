using BricksAndBalls.Data.Screens;
using TMPro;
using UnityEngine;

namespace BricksAndBalls.UI.View
{
   public class LevelUI: Screen<LevelUIData>
   {
      [SerializeField]
      private TextMeshProUGUI highScore;
      [SerializeField]
      private TextMeshProUGUI triesLeft;

      public override void SetData(LevelUIData data)
      {
         highScore.text = "Score: " + data.Score.ToString();
         triesLeft.text = "Tries left: " + data.NumberOfTries.ToString();
      }
   }
}
