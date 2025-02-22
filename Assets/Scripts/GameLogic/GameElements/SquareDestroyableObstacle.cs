using BricksAndBalls.Data.Game;
using BricksAndBalls.Physics;
using System.Collections.Generic;
using UnityEngine;

using Collider = BricksAndBalls.Physics.Colliders.Collider;

namespace BricksAndBalls.GameLogic.Elements.Obstacles
{
   public class SquareDestroyableObstacle: GameElement, IPhysicsEntity, IHighscoreModifier, IWinCondtion
   {
      private const int hitScoreAmount = 10;

      private DestroyableObstacleData data;
      private List<Collider> colliders;
      private HighScoreUpdateDelegate highscoreUpdateDelegate = null;

      public SquareDestroyableObstacle()
      {
         data = new DestroyableObstacleData();
         colliders = new List<Collider>();
      }

      public SquareDestroyableObstacle(Vector2 position, float size, int numberOfHits)
      {
         data = new DestroyableObstacleData();
         data.NumberOfHitsNeeded = numberOfHits;
         data.Position = position;
         data.Size = size;

         colliders = new List<Collider>()
         {
            new SquareCollider(data.Size)
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
         data = JsonUtility.FromJson<DestroyableObstacleData>(json);

         colliders = new List<Collider>()
         {
            new SquareCollider(data.Size)
         };
      }
      public void FlipVelocity(Vector2 directionFlip) { }
      public override IPhysicsEntity GetPhysicsEntity() => this;
      public void OnCollision(IPhysicsEntity otherEntity)
      {
         data.WasHitLastFrame = true;
         data.NumberOfHitsNeeded--;
         highscoreUpdateDelegate?.Invoke(hitScoreAmount);
         SetDataUpdated();
         if(data.NumberOfHitsNeeded <= 0)
         {
            Destroy();
         }

         data.WasHitLastFrame = false;
      }

      public void SetHighScoreUpdateDelegate(HighScoreUpdateDelegate del)
      {
         highscoreUpdateDelegate = del;
      }

      public bool IsCompleted()
      {
         return data.NumberOfHitsNeeded <= 0;
      }
   }
}