using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.App.States
{
   public abstract class StateMachine<T> where T:  StateMachineStateBase
   {
      public StateMachine()
      {

      }

      public StateMachine(T initialState)
      {
         ChangeState(initialState);
      }

      private StateMachineStateBase currentState;

      public K ChangeState<K>() where K : T
      {
         var stateInstance = CreateState<K>();
         ChangeState(stateInstance);
         return stateInstance;
      }

      public void ChangeState(StateMachineStateBase state)
      {
         currentState?.OnExit();

         state.OnEnter();
         currentState = state;
      }


      public K CreateState<K>() where K : T
      {
         return (K)Activator.CreateInstance(typeof(K), this);
      }

      public void Destroy() { currentState?.OnExit(); }


      public void Update(float deltaTime) => currentState?.Update(deltaTime);
   }
}
