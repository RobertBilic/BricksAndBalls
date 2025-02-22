using BricksAndBalls.Data.Game;
using BricksAndBalls.Data.GameData;
using BricksAndBalls.GameLogic.Elements;
using BricksAndBalls.Physics;
using BricksAndBalls.Util.CameraExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BricksAndBalls.GameLogic
{
   public class Level
   {
      private const float speed = 300.0f;
      private const float spawnDelay = 0.03f;
      private readonly int spawnNumber;

      public bool IsWinConditionAchieved
      {
         get
         {
            if(winConditions.Count == 0)
               return false;

            return winConditions.All(x => x.IsCompleted());
         }
      }
      public bool IsLoseConditionAchieved
      {
         get
         {
            if(loseConditions.Count == 0)
               return false;

            return loseConditions.All(x => x.IsCompleted());
         }
      }

      public int RoundNumber { get; private set; }
      public GameLevelData LevelData { get; private set; }
      public int Multiplier;
      public HighScoreTracker HighScoreTracker;
      private List<IWinCondtion> winConditions;
      private List<ILoseCondition> loseConditions;
      private PhysicsManager physicsManager;
      private float aimAngle;
      private float spawnTimer;
      private int spawnCount;
      private GameElementGeneratedDelegate gameElementGeneratedDel;

      public Level(GameLevelData levelData)
      {
         this.LevelData = levelData;
         this.winConditions = new List<IWinCondtion>();
         this.loseConditions = new List<ILoseCondition>();
         this.HighScoreTracker = new HighScoreTracker();
         this.physicsManager = new PhysicsManager();
         this.physicsManager.LayerCollisionManager.AddCollisionRule(1, 2);
         this.spawnNumber = LevelData.BallNumber;
         this.RoundNumber = 0;
      }

      public void AddWinCondition(IWinCondtion condition) => winConditions.Add(condition);
      public void AddLoseCOndition(ILoseCondition condition) => loseConditions.Add(condition);

      public void SetAimAngle(float angle)
      {
         this.aimAngle = angle;
      }

      public GameResultData GetResultData()
      {
         return new GameResultData()
         {
            HighScore = HighScoreTracker.GetHighScore(),
            Multiplier = Multiplier
         };
      }

      public void Update(float deltaTime)
      {
         physicsManager.PhysicsStep(deltaTime);


         if(spawnCount < spawnNumber)
         {
            spawnTimer += deltaTime;

            if(spawnTimer >= spawnDelay)
            {
               GenerateBall();
               spawnCount++;
               spawnTimer = 0.0f;
            }
         }

         var inactiveEntities = physicsManager.GetInactiveEntities();

         foreach(var inactiveEntity in inactiveEntities)
            MoveInactiveBall(inactiveEntity, LevelData.StartPosition, deltaTime);
      }


      private void MoveInactiveBall(IPhysicsEntity entity, Vector2 position, float deltaTime)
      {
         if((position - entity.Position).sqrMagnitude < 0.01f)
            return;

         entity.Position = Vector2.MoveTowards(entity.Position, position, deltaTime * speed);
      }


      public List<GameElementGenerationInfo> GenerateObstacles(List<ObstacleData> data)
      {
         var obstacles = new List<GameElementGenerationInfo>();

         for(int i = 0; i < data.Count; ++i)
         {
            var obstacle = GenerateObstacle(data[i]);
            obstacles.Add(obstacle);
         }
         var defaultObstacles = GenerateDefaultObstacles();

         foreach(var obstacle in defaultObstacles)
            obstacles.Add(obstacle);

         return obstacles;
      }

      private GameElementGenerationInfo GenerateObstacle(ObstacleData data)
      {
         var type = Type.GetType(data.TypeString);
         var element = Activator.CreateInstance(type) as GameElement;
         element.DeserializeCustomData(data.CustomData);
         physicsManager.AddEntity(element.GetPhysicsEntity());

         if(element is IHighscoreModifier)
            HighScoreTracker.AddTrackerObject(element as IHighscoreModifier);
         if(element is IWinCondtion)
            AddWinCondition(element as IWinCondtion);

         return new GameElementGenerationInfo(element, true);
      }

      private List<GameElementGenerationInfo> GenerateDefaultObstacles()
      {
         var minimumSize = 50.0f;

         var camera = Camera.main;

         var minExtent = camera.GetMinCameraExtent();
         var currentExtent = camera.GetCameraExtent();

         var wallWidth = Mathf.Max(currentExtent.x - minExtent.x, minimumSize);

         var sidePosition = new Vector2(currentExtent.x + wallWidth / 2, 0.0f);
         var sideSize = new Vector2(wallWidth, currentExtent.y * 2.0f * 1.2f);
         var verticalPosition = new Vector2(0.0f, currentExtent.y + minimumSize / 2.0f);
         var verticalSize = new Vector2(currentExtent.x * 2.0f * 1.2f, minimumSize);

         GameElement rightWall = new SideWall(sidePosition, sideSize);
         GameElement leftWall = new SideWall(-sidePosition, sideSize);
         GameElement topWall = new TopWall(verticalPosition, verticalSize);
         GameElement bottomWall = new BottomWall(-verticalPosition, verticalSize);

         physicsManager.AddEntity(rightWall.GetPhysicsEntity());
         physicsManager.AddEntity(leftWall.GetPhysicsEntity());
         physicsManager.AddEntity(topWall.GetPhysicsEntity());
         physicsManager.AddEntity(bottomWall.GetPhysicsEntity());

         List<GameElementGenerationInfo> obstacleList = new List<GameElementGenerationInfo>()
         {
            new GameElementGenerationInfo(leftWall, true),
            new GameElementGenerationInfo(rightWall, true),
            new GameElementGenerationInfo(bottomWall, false),
            new GameElementGenerationInfo(topWall, false),
         };

         return obstacleList;
      }


      private void GenerateBall()
      {
         var element = new Ball(LevelData.StartPosition, 5.0f);
         element.Position = LevelData.StartPosition;

         var angle = Mathf.PI / 2f * 0.9f * aimAngle;
         var direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
         element.Velocity = (direction * speed);

         gameElementGeneratedDel.Invoke(new GameElementGenerationInfo(element,true));
         AddLoseCOndition(element);
         physicsManager.AddEntity(element);

         element.AddOnDestroyCallback(() =>
         {
            physicsManager.RemoveEntity(element);
         });
      }

      public void InitObstacles(List<GameElementGenerationInfo> elements)
      {
         foreach(var element in elements)
         {
            element.GameElement.AddOnDestroyCallback(() =>
            {
               physicsManager.RemoveEntity(element.GameElement.GetPhysicsEntity());
            });

         }
      }

      public void ResetRound()
      {
         RoundNumber++;
         spawnCount = 0;
         loseConditions.Clear();
      }

      public void AddOnGameObjectGeneratedCallback(GameElementGeneratedDelegate callback) => gameElementGeneratedDel += callback;
      public void RemoveOnGameObjectGeneratedCallback(GameElementGeneratedDelegate callback) => gameElementGeneratedDel -= callback;
   }

   public delegate void GameElementGeneratedDelegate(GameElementGenerationInfo element);

   public class GameElementGenerationInfo
   {
      public GameElement GameElement;
      public bool NeedsDrawer;

      public GameElementGenerationInfo(GameElement gameElement, bool needsDrawer)
      {
         GameElement = gameElement;
         NeedsDrawer = needsDrawer;
      }
   }
}