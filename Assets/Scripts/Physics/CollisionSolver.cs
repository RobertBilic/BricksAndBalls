using System;
using UnityEngine;
using BricksAndBalls.Physics.Colliders;

using Collider = BricksAndBalls.Physics.Colliders.Collider;

namespace BricksAndBalls.Physics
{
   internal class CollisionSolver
   {
      public Type t1;
      public Type t2;

      public delegate CollisionData GetCollisionInfo(IPhysicsEntity e1, Collider c1, IPhysicsEntity e2, Collider c2);

      public GetCollisionInfo GetCollisionInfoMethod;

      public CollisionSolver(Type t1, Type t2, GetCollisionInfo getCollisionInfoMethod)
      {
         this.t1 = t1;
         this.t2 = t2;
         GetCollisionInfoMethod = getCollisionInfoMethod;
      }

      public bool IsSolverFor(Collider c1, Collider c2)
          => c1.GetType() == t1 && c2.GetType() == t2;

      public static bool SolveCollision(IPhysicsEntity e1, IPhysicsEntity e2)
      {
         foreach(var c1 in e1.Colliders)
         {
            foreach(var c2 in e2.Colliders)
            {
               foreach(var solver in CollisionSolvers)
               {
                  CollisionData data = null;
                  if(solver.IsSolverFor(c1, c2))
                     data = solver.GetCollisionInfoMethod(e1, c1, e2, c2);
                  else if(solver.IsSolverFor(c2, c1))
                  {
                     data = solver.GetCollisionInfoMethod(e2, c2, e1, c1);
                     if(data != null)
                        data = new CollisionData(-data.Displacement, data.DirectionFlip);
                  }

                  if(data == null)
                     continue;
                  if(e1.IsStatic && e2.IsStatic)
                  {

                  }

                  var nonStaticEntity = e1.IsStatic ? e2 : e1;
                  var timeSpentInside = Mathf.Max(
                             nonStaticEntity.Velocity.x != 0f ? data.Displacement.x / nonStaticEntity.Velocity.x : 0,
                             nonStaticEntity.Velocity.y != 0f ? data.Displacement.y / nonStaticEntity.Velocity.y : 0);
                  nonStaticEntity.Position -= timeSpentInside * nonStaticEntity.Velocity;
                  nonStaticEntity.FlipVelocity(data.DirectionFlip);
                  nonStaticEntity.Position += timeSpentInside * nonStaticEntity.Velocity;
                  return true;
               }
            }
         }

         return false;
      }

      public class CollisionData
      {
         public Vector2 Displacement;
         public Vector2 DirectionFlip;

         public CollisionData()
         {
            DirectionFlip = Vector2.one;
         }

         public CollisionData(Vector2 displacement, Vector2 directionFlip)
         {
            Displacement = displacement;
            DirectionFlip = directionFlip;
         }
      }

      private static CollisionSolver[] CollisionSolvers = new[]
      {
            new CollisionSolver(
                typeof(CircleCollider), typeof(CircleCollider),
                (e1, c1, e2, c2) =>
                {
                    var (cirlce1, circle2) = (c1 as CircleCollider, c2 as CircleCollider);
                    var distance = (e1.Position - e2.Position).magnitude;
                    var radiusReach = cirlce1.Radius + circle2.Radius;
                    if (radiusReach < distance)
                        return null;
                    return new CollisionData(new Vector2(radiusReach - distance, 0), Vector2.zero);
                }),
            new CollisionSolver(
                typeof(CircleCollider), typeof(HorizontalLineCollider),
                (e1, c1, e2, c2) =>
                {
                    var (c, line) = (c1 as CircleCollider, c2 as HorizontalLineCollider);


                   var closestPointX = Mathf.Clamp(e1.Position.x, e2.Position.x - line.Width / 2, e2.Position.x + line.Width/2);
                        var linePosY = e2.Position.y + line.OffsetY;


                    float distance = Vector2.Distance(new Vector2(closestPointX, linePosY), e1.Position);
                    var isIntersecting =  distance <= c.Radius;

                    if (isIntersecting)
                    {
                        var circleTop = e1.Position.y + c.Radius;
                        var circleBot = e1.Position.y - c.Radius;

                        if(e1.Position.y > linePosY)
                            return new CollisionData(
                                new Vector2(0, linePosY - circleBot),
                                new Vector2(1, -1f));
                        else
                            return new CollisionData(
                                new Vector2(0, linePosY - circleTop),
                                new Vector2(1, -1f));
                    }
                    else return null;
                }),
            new CollisionSolver(
                typeof(CircleCollider), typeof(VerticalLineCollider),
                (e1, c1, e2, c2) =>
                {
                   var (c, line) = (c1 as CircleCollider, c2 as VerticalLineCollider);

                   var linePosX = e2.Position.x + line.OffsetX;
                   var closestPointY = Mathf.Clamp(e1.Position.y, e2.Position.y - line.Height / 2, e2.Position.y + line.Height/2);

                    float distance = Vector2.Distance(new Vector2(linePosX, closestPointY), e1.Position);
                    var isIntersecting =  distance <= c.Radius;


                    if (isIntersecting)
                    {
                        var circleRight = e1.Position.x + c.Radius;
                         var circleLeft = e1.Position.x - c.Radius;
                        if(e1.Position.x > linePosX)
                            return new CollisionData(
                                new Vector2(linePosX - circleLeft, 0),
                                new Vector2(-1, 1f));
                        else
                            return new CollisionData(
                                new Vector2(linePosX - circleRight, 0),
                                new Vector2(-1, 1f));
                    }
                    else return null;
                }),
            new CollisionSolver(typeof(CircleCollider), typeof(RectangleCollider),CircleRectangleCollision),
            new CollisionSolver(typeof(CircleCollider), typeof(SquareCollider),CircleRectangleCollision)

        };

      private static CollisionData CircleRectangleCollision(IPhysicsEntity circleEntity, Collider c1, IPhysicsEntity squareEntity, Collider c2)
      {
         var (circle, square) = (c1 as CircleCollider, c2 as RectangleCollider);

         float closestX = Mathf.Clamp(circleEntity.Position.x, squareEntity.Position.x - square.Size.x / 2, squareEntity.Position.x + square.Size.x / 2);
         float closestY = Mathf.Clamp(circleEntity.Position.y, squareEntity.Position.y - square.Size.y / 2, squareEntity.Position.y + square.Size.y / 2);

         var distance = Vector2.Distance(new Vector2(closestX, closestY), circleEntity.Position);

         if(distance > circle.Radius)
            return null;

         var angle = Mathf.Atan2(squareEntity.Position.y - circleEntity.Position.y,
                  squareEntity.Position.x - circleEntity.Position.x) * (180f / Mathf.PI);
         angle += 180f;
         angle = angle % 360;
         if(angle < 0)
            angle += 360;


         var sideAngle = Mathf.Atan2(square.Size.y, square.Size.x) * (180f / Mathf.PI);
         var verticalAngle = 90.0f - sideAngle;

         var rightTopHalf = sideAngle;
         var topSideAngle = sideAngle + 2 * verticalAngle;
         var leftSideAngle = 3 * sideAngle + 2 * verticalAngle;
         var bottomSideAngle = 3 * sideAngle + 4 * verticalAngle;

         if(angle < rightTopHalf)
            return new CollisionData(new Vector2((squareEntity.Position.x + square.Size.x / 2f) - (circleEntity.Position.x - circle.Radius), 0),
               new Vector2(-1, 1));
         else if(angle < topSideAngle)
            return new CollisionData(new Vector2(0, (squareEntity.Position.y + square.Size.y / 2f) - (circleEntity.Position.y - circle.Radius)),
               new Vector2(1, -1));
         else if(angle < leftSideAngle)
            return new CollisionData(new Vector2((squareEntity.Position.x - square.Size.x / 2f) - (circleEntity.Position.x + circle.Radius), 0),
              new Vector2(-1, 1));
         else if(angle <bottomSideAngle)
            return new CollisionData(new Vector2(0, (squareEntity.Position.y - square.Size.y / 2f) - (circleEntity.Position.y + circle.Radius)),
             new Vector2(1, -1));
         else if(angle < 360)
            return new CollisionData(new Vector2((squareEntity.Position.x + square.Size.x / 2f) - (circleEntity.Position.x - circle.Radius), 0),
             new Vector2(-1, 1));

         Debug.LogError(angle);
         return null;
      }

   }
}