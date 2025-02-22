using UnityEngine;

namespace BricksAndBalls.Data.Game
{
   [System.Serializable]
   public class DestroyableObstacleData : GameElementData
   {
      public int NumberOfHitsNeeded;
      public bool WasHitLastFrame;
      public Color Color;
   }
}
