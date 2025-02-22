using BricksAndBalls.Data.Screens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.UI.View
{
   public class LoadingScreen: Screen<LoadingScreenData>
   {
      private void Start()
      {
         //NOTE: Generating a new sprite to be able to use the Filled image type, without importing an progress bar graphic
         Texture2D tex = new Texture2D(1, 1);
         tex.SetPixel(0, 0, Color.white);

         fillSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0.0f,0.0f));
         loadingBar.sprite = fillSprite;
      }

      [SerializeField]
      private AnimationCurve progressCurve;
      [SerializeField]
      private Image loadingBar;

      private Sprite fillSprite;

      public override void SetData(LoadingScreenData data)
      {
         var progress = progressCurve.Evaluate(data.loadProgress);

         loadingBar.type = Image.Type.Filled;
         loadingBar.fillMethod = Image.FillMethod.Horizontal;
         loadingBar.fillOrigin = 0;
         loadingBar.fillAmount = progress;
      }
   }
}
