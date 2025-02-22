namespace BricksAndBalls.Physics
{
   public class CollisionInfo
   {
      public readonly IPhysicsEntity E1;
      public readonly IPhysicsEntity E2;

      public CollisionInfo(IPhysicsEntity e1, IPhysicsEntity e2)
      {
         E1 = e1;
         E2 = e2;
      }
   }
}