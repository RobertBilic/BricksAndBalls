using BricksAndBalls.Data.Game;
using BricksAndBalls.Physics;
using BricksAndBalls.Physics.Colliders;
using System.Collections.Generic;
using UnityEngine;
using Collider = BricksAndBalls.Physics.Colliders.Collider;

namespace BricksAndBalls.GameLogic.Elements
{
   public class TopWall: GameElement, IPhysicsEntity
   {
      private WallData data;
      private List<Collider> colliders;

      public TopWall()
      {
         data = new WallData();
         colliders = new List<Collider>();
      }

      public TopWall(Vector2 position, Vector2 size)
      {
         data = new WallData();
         data.Position = position;
         data.Size = 1.0f;
         data.VectorSize = size;

         colliders = new List<Collider>()
         {
            new RectangleCollider(size)
         };
      }

      public Vector2 Velocity { get => Vector2.zero; set { } }
      public Vector2 Position { get => data.Position; set => data.Position = value; }
      public bool IsStatic => true;
      public int Layer => 2;
      public List<Physics.Colliders.Collider> Colliders => colliders;

      public bool IsActive => true;

      public override GameElementData GetData() => data;
      public override void DeserializeCustomData(string json)
      {
         data = JsonUtility.FromJson<WallData>(json);
         colliders = new List<Collider>()
         {
            new RectangleCollider(data.VectorSize)
         };
      }
      public void FlipVelocity(Vector2 directionFlip) { }
      public override IPhysicsEntity GetPhysicsEntity() => this;

      public void OnCollision(IPhysicsEntity otherEntity)
      {
      }
   }
}