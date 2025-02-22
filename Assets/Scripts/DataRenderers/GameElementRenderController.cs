using BricksAndBalls.Data.Game;
using System;
using UnityEngine;

namespace BricksAndBalls.Game.Renderers
{
   public class GameElementRenderController
   {
      private static GameElementRenderController __instance;
      public static GameElementRenderController Instance
      {
         get
         {
            if(__instance == null)
               __instance = new GameElementRenderController();

            return __instance;
         }
      }

      public GameElementRendererBase CreateRenderer<T>(Type type, T data) where T : GameElementData
      {
         var drawerPrefab = Resources.Load<GameElementRendererBase>($"GameElements/Renderers/{type.Name}Renderer" );
         if(drawerPrefab == null)
            throw new System.Exception("Missing prefab at location");

         var drawer = GameObject.Instantiate(drawerPrefab);
         drawer.SetData(data);
         return drawer;
      }
   }
}