using System.Collections.Generic;

namespace BricksAndBalls.Physics
{
   public class PhysicsManager
   {
      public LayerCollisionManager LayerCollisionManager { get; } = new();
      private List<IPhysicsEntity> _entities = new();
      private List<IPhysicsEntity> inactiveEntities = new List<IPhysicsEntity>();
      private List<CollisionInfo> collisionsLastFrame = new List<CollisionInfo>();

      public List<IPhysicsEntity> GetInactiveEntities() => inactiveEntities;

      public void AddEntity(IPhysicsEntity entity)
      {
         _entities.Add(entity);
      }

      public void RemoveEntity(IPhysicsEntity entity)
      {
         _entities.Remove(entity);
      }


      public void PhysicsStep(float deltaTime)
      {
         ApplyVelocities(deltaTime);
         SolveCollisions();
         HandleCollision();

         collisionsLastFrame.Clear();
      }

      private void ApplyVelocities(float deltaTime)
      {
         foreach(var entity in _entities)
         {
            if(entity.IsActive)
               entity.Position += entity.Velocity * deltaTime;
         }
      }

      private void SolveCollisions()
      {
         foreach(var entity1 in _entities)
         {
            if(!entity1.IsActive)
            {
               if(!inactiveEntities.Contains(entity1))
                  inactiveEntities.Add(entity1);

               continue;
            }

            foreach(var entity2 in _entities)
            {
               if(!entity2.IsActive)
               {
                  if(!inactiveEntities.Contains(entity2))
                     inactiveEntities.Add(entity2);

                  continue;
               }

               if(entity1 == entity2)
                  continue;

               if(LayerCollisionManager.CanCollide(entity1.Layer, entity2.Layer))
               {
                  if(CollisionSolver.SolveCollision(entity1, entity2))
                  {
                     collisionsLastFrame.Add(new CollisionInfo()
                     {
                        entity1 = entity1,
                        entity2 = entity2
                     });
                  }
               }
            }
         }
      }

      private void HandleCollision()
      {
         foreach(var collisionInfo in collisionsLastFrame)
         {
            collisionInfo.entity1.OnCollision(collisionInfo.entity2);
            collisionInfo.entity2.OnCollision(collisionInfo.entity1);
         }
      }

      private class CollisionInfo
      {
         public IPhysicsEntity entity1;
         public IPhysicsEntity entity2;
      }
   }
}