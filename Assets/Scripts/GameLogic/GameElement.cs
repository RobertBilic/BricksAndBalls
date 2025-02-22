using BricksAndBalls.Data.Game;
using BricksAndBalls.Physics;
using UnityEngine;
using Collider = BricksAndBalls.Physics.IPhysicsEntity;

namespace BricksAndBalls.GameLogic.Elements
{
   public delegate void ElementDestroyedDelegate();
   public delegate void GaemElementDataUpdated(GameElementData data);

   public abstract class GameElement
   {
      private ElementDestroyedDelegate destroyedDelegate;
      private GaemElementDataUpdated dataUpdatedCallback;

      public void AddOnDataUpdatedCallback(GaemElementDataUpdated callback)
      {
         dataUpdatedCallback += callback;
      }

      public void AddOnDestroyCallback(ElementDestroyedDelegate del)
      {
         destroyedDelegate += del;
      }
      public void Destroy()
      {
         destroyedDelegate?.Invoke();

         destroyedDelegate = null;
         dataUpdatedCallback = null;
      }

      protected void SetDataUpdated()
      {
         dataUpdatedCallback?.Invoke(GetData());
      }

      public abstract IPhysicsEntity GetPhysicsEntity();
      public abstract GameElementData GetData();
      public abstract void DeserializeCustomData(string json);
      public virtual string SerializeCustomData()
      {
         return JsonUtility.ToJson(GetData());
      }
   }
}