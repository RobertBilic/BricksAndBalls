
namespace BricksAndBalls.Physics.Colliders
{
   public class VerticalLineCollider: Collider
   {
      public float Height { get; set; }
      public float OffsetX { get; set; }

      public VerticalLineCollider(float height, float offset)
      {
         Height = height;
         OffsetX = offset;
      }
   }
}