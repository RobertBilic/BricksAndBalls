using BricksAndBalls.Data.Game;
using BricksAndBalls.Game.Renderers;
using BricksAndBalls.GameLogic.Elements;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Game.States
{
   class AimState: GameState
   {
      private float angleCoefficient;
      private Vector2? startPosition;

      private List<GameElementRendererBase> generatedBalls;

      private float stepSize = 15.0f;
      private int maxBalls = 7;

      public AimState(GameLogicStateMachine stateMachine) : base(stateMachine)
      {
         GenerateBalls();
         UpdateBallPositions();
      }

      public override void Update(float deltaTime)
      {
         base.Update(deltaTime);

         if(!startPosition.HasValue)
            startPosition = Input.mousePosition;



         if(Input.GetMouseButton(0))
         {
            angleCoefficient = (Input.mousePosition.x - startPosition.Value.x) / Screen.width;
            angleCoefficient = Mathf.Clamp(angleCoefficient * 5, -1f, 1f);
            stateMachine.Level.SetAimAngle(angleCoefficient);

            UpdateBallPositions();
         } else
         {
            stateMachine.ChangeState<SimulateState>();
         }
      }

      private void GenerateBalls()
      {
         generatedBalls = new List<GameElementRendererBase>();
         for(int i = 0; i < maxBalls; ++i)
         {
            var ball = new Ball(Vector2.zero, 5.0f);
            var drawer = GameElementRenderController.Instance.CreateRenderer<GameElementData>(typeof(Ball), new BallData());
            drawer.SetData(ball.GetData());

            generatedBalls.Add(drawer);
         }
      }

      private void UpdateBallPositions()
      {
         var startPosition = stateMachine.Level.LevelData.StartPosition;

         var angle = Mathf.PI / 2f * 0.9f * angleCoefficient;
         var direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
         var positionOffset = direction * stepSize;

         for(int i = 0; i < generatedBalls.Count; ++i)
         {
            generatedBalls[i].transform.position = startPosition;
            startPosition = startPosition + positionOffset;
         }
      }

      public override void OnExit()
      {
         base.OnExit();

         foreach(var ball in generatedBalls)
         {
            GameObject.Destroy(ball.gameObject);
         }

         generatedBalls.Clear();
      }


   }
}
