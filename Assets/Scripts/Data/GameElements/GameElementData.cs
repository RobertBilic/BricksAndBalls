using UnityEngine;

namespace BricksAndBalls.Data.Game
{
   public class GameElementData
   {
      public GameElementData()
      {
         Position = Vector2.zero;
         Velocity = Vector2.zero;
      }

      public Vector2 Position;
      public Vector2 Velocity;
      public float Size;
   }
}
