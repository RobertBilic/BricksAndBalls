using UnityEngine;

namespace BricksAndBalls.App.States
{
   public abstract class StateMachineStateBase
   {
      public virtual void OnEnter()
      {
         Debug.Log($"Entering {GetType().ToString()}...");
      }

      public virtual void Update(float deltaTime) { }

      public virtual void OnExit()
      {
         Debug.Log($"Exiting {GetType().ToString()}...");
      }
   }
}