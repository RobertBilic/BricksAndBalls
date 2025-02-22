using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Data.Game
{
   [System.Serializable]
   public class GameLevelData
   {
      public List<ObstacleData> BrickData;
      public int BallNumber;

      public int Tries;
      public Vector2 StartPosition;
   }

   [System.Serializable]
   public class ObstaclesColorSettings
   {
      public Color Color;
      public int StrengthLowerLimit;
      public int StrengthUpperLimit;
   }


}
