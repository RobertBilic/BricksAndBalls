using BricksAndBalls.Data.Game;
using UnityEngine;

namespace BricksAndBalls.Game.Renderers
{
   public class GameElementRenderer<T>: GameElementRendererBase  where T : GameElementData
   {
      public override void SetData(object obj)
      {
         SetData(obj as T);
      }

      private void SetData(T data)
      {
         OnDataUpdated(data);
      }

      protected virtual void OnDataUpdated(T data)
      {
         SetPosition(data.Position);
         SetSize(data.Size);
      }


      protected void SetPosition(Vector2 position) => transform.position = position;
      protected void SetSize(float size) => transform.localScale = size * Vector2.one;
   }

   public abstract class GameElementRendererBase :MonoBehaviour
   {
      public abstract void SetData(object obj);
      public void Destroy() => GameObject.Destroy(gameObject);
   }
}