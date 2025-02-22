using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.View;
using BricksAndBalls.Util.Factorys;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.UI.Controllers
{
   public class ScreenController
   {
      private static ScreenController _instance;
      public static ScreenController Instance
      {
         get
         {
            if(_instance == null)
            {
               _instance = new ScreenController();
               _instance.Init();
            }
            return _instance;
         }
      }

      private Canvas mainCanvas;

      private void Init()
      {
         mainCanvas = CreateCanvas("MainCanvas");
      }

      private Canvas CreateCanvas(string canvasName)
      {
         GameObject newGameobject = new GameObject(canvasName);
         var canvas = newGameobject.AddComponent<Canvas>();
         newGameobject.AddComponent<GraphicRaycaster>();
         canvas.renderMode = RenderMode.ScreenSpaceOverlay;

         var canvasScaler = newGameobject.AddComponent<CanvasScaler>();
         canvasScaler.matchWidthOrHeight = 1.0f;
         canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
         canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
         canvasScaler.referenceResolution = new Vector2(1080, 1920);

         return canvas;
      }

      public T CreateScreen<T,U>()
         where T: Screen<U>
         where U : ScreenData
      {
         var screenFactory = new GenericSreenFactory<T, U>();
         var screen = screenFactory.Create();
         screen.transform.SetParent(mainCanvas.transform, false);
         return screen;
      }
   }
}