using BricksAndBalls.App.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BricksAndBalls.App.Controllers
{

   class AppController : MonoBehaviour
   {
      private AppStateMachine mainStateMachine;

      private void Awake()
      {
         mainStateMachine = new AppStateMachine();
      }

      private void Start()
      {
         mainStateMachine.ChangeState<InitialLoadingState>();
      }

      private void Update()
      {
         mainStateMachine.Update(Time.deltaTime);
      }
   }
}
