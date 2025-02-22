using BricksAndBalls.Physics.Colliders;
using UnityEngine;

public class SquareCollider : BricksAndBalls.Physics.Colliders.RectangleCollider
{
   public SquareCollider(float size) : base(Vector2.one * size)
   {

   }
}
