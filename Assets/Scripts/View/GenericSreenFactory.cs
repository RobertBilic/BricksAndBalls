using BricksAndBalls.Data.Screens;
using BricksAndBalls.UI.View;
using UnityEngine;

namespace BricksAndBalls.Util.Factorys
{
   public class GenericSreenFactory<T,U>: GenericFactory<T> 
      where U : ScreenData
      where T : Screen<U>   
   {
      public override T Create()
      {
         var prefabPath = $"UI/{typeof(T).Name}";
         var prefab = Resources.Load<T>(prefabPath);
         if(prefab == null)
            throw new System.Exception($"Missing prefab at {prefabPath}");
         return GameObject.Instantiate<T>(prefab);
      }
   }
}