
namespace BricksAndBalls.Physics.Colliders
{
   public class CircleCollider: Collider
   {
      public float Radius { get; set; }

      public CircleCollider(float radius)
      {
         Radius = radius;
      }
   }
}