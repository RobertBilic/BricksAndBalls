using BricksAndBalls.Data.Game;
using UnityEngine;

namespace BricksAndBalls.Game.Renderers
{
   public class SideWallRenderer: GameElementRenderer<WallData>
   {
      protected override void OnDataUpdated(WallData data)
      {
         SetPosition(data.Position);
         SetSize(data.VectorSize * data.Size);
      }

      private void SetSize(Vector2 size) => transform.localScale = size; 
   }
}