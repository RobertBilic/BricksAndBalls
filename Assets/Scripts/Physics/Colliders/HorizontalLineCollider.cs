
namespace BricksAndBalls.Physics.Colliders
{
   public class HorizontalLineCollider: Collider
   {
      public float Width { get; set; }
      public float OffsetY { get; set; }

      public HorizontalLineCollider(float width, float offset)
      {
         Width = width;
         OffsetY = offset;
      }
   }
}