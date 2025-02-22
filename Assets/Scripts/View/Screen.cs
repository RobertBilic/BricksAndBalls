using BricksAndBalls.Data.Screens;
using UnityEngine;

namespace BricksAndBalls.UI.View
{
   public abstract class Screen<T>: MonoBehaviour where T : ScreenData
   {
      public abstract void SetData(T data);
   }
}