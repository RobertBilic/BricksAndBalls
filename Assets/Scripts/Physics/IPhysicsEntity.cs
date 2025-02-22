using System;
using System.Collections.Generic;
using UnityEngine;

using Collider = BricksAndBalls.Physics.Colliders.Collider;

namespace BricksAndBalls.Physics
{
   public interface IPhysicsEntity
   {
      public bool IsActive { get; }
      public Vector2 Velocity { get; set; }
      public Vector2 Position { get; set; }
      public bool IsStatic { get; }
      public int Layer { get; }
      public List<Collider> Colliders { get; }

      public void FlipVelocity(Vector2 directionFlip);
      public void OnCollision(IPhysicsEntity otherEntity);
   }
}