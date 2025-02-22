using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Physics.Colliders
{
   public class Collider
   {
      public static List<Collider> CreateBoxColliderSet(Vector2 center, Vector2 size)
      {
         return new List<Collider>()
            {
                new HorizontalLineCollider(size.x, size.y/2f),
                new HorizontalLineCollider(size.x, -size.y/2f),
                new VerticalLineCollider(size.y, size.x/2f),
                new VerticalLineCollider(size.y, -size.x/2f)
            };
      }
   }
}