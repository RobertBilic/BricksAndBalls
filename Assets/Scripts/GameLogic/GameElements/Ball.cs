using BricksAndBalls.Data.Game;
using BricksAndBalls.Physics;
using BricksAndBalls.Physics.Colliders;
using System.Collections.Generic;
using UnityEngine;

using Collider = BricksAndBalls.Physics.Colliders.Collider;

namespace BricksAndBalls.GameLogic.Elements
{
   public class Ball: GameElement, IPhysicsEntity, ILoseCondition
   {
      private List<Collider> colliders;
      private bool isActive = true;

      public Ball(Vector2 position, float size)
      {
         data = new BallData();

         data.Position = position;
         data.Size = size;
         colliders = new List<Collider>()
         {
            new CircleCollider(size/2.0f)
         };
      }

      private BallData data = new BallData();

      public Vector2 Velocity
      {
         get => data.Velocity;
         set => data.Velocity = value;
      }

      public Vector2 Position
      {
         get => data.Position;
         set => data.Position = value;
      }

      public bool IsStatic => false;

      public int Layer => 1;

      public List<Physics.Colliders.Collider> Colliders => colliders;

      bool IPhysicsEntity.IsActive => isActive;

      public void FlipVelocity(Vector2 directionFlip)
      {
         data.Velocity.x = data.Velocity.x * directionFlip.x;
         data.Velocity.y = data.Velocity.y * directionFlip.y;
      }

      public override GameElementData GetData()
      {
         return data;
      }

      public override void DeserializeCustomData(string json)
      {
      }

      public override IPhysicsEntity GetPhysicsEntity() => this;

      public void OnCollision(IPhysicsEntity otherEntity)
      {
         var isBottomWall = otherEntity is BottomWall;

         if(!isBottomWall)
            return;

         MarkAsInactive();
      }

      private void MarkAsInactive()
      {
         isActive = false;
      }

      public bool IsCompleted()
      {
         return !isActive;
      }
   }
}