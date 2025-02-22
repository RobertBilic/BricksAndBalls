using BricksAndBalls.Physics.Colliders;
using UnityEngine;

namespace BricksAndBalls.Physics.Colliders
{
   public class RectangleCollider: BricksAndBalls.Physics.Colliders.Collider
   {
      public Vector2 Size;

      public RectangleCollider(Vector2 size)
      {
         Size = size;
      }
   }
}