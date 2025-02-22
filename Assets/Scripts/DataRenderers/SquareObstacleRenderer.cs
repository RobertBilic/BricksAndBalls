using BricksAndBalls.Data.Game;
using System.Collections;
using TMPro;
using UnityEngine;

namespace BricksAndBalls.Game.Renderers
{
   public class SquareObstacleRenderer: GameElementRenderer<DestroyableObstacleData>
   {
      private const float hitScaleIncrease = 0.2f;

      [SerializeField]
      private TextMeshProUGUI numberOfHits;
      [SerializeField]
      private AnimationCurve animationCurve;
      [SerializeField]
      private SpriteRenderer spriteRenderer;

      private Coroutine scalingCoroutine;
      private float currentScale;


      protected override void OnDataUpdated(DestroyableObstacleData data)
      {
         SetPosition(data.Position);

         numberOfHits.text = data.NumberOfHitsNeeded.ToString();
         spriteRenderer.color = data.Color;
         if(data.WasHitLastFrame)
         {
            if(scalingCoroutine != null)
            {
               StopCoroutine(scalingCoroutine);
               scalingCoroutine = null;
            }

            scalingCoroutine = StartCoroutine(ScaleEoutAnimation(data.Size, 0.05f));
         } else
         {
            currentScale = data.Size;
            SetSize(data.Size);
         }
      }

      private IEnumerator ScaleEoutAnimation(float normalScale, float duration)
      {
         var eof = new WaitForEndOfFrame();
         var t = Mathf.InverseLerp(normalScale, normalScale + hitScaleIncrease, currentScale);
         var time = Mathf.Lerp(0.0f, duration, t);

         while(time <= duration)
         {
            var scalingT = animationCurve.Evaluate(time / duration);
            var size = scalingT * hitScaleIncrease * normalScale+ normalScale;
            time += Time.deltaTime;
            SetSize(size);
            yield return eof;
         }

         time = 0.0f;

         while(time <= duration)
         {
            var scalingT = animationCurve.Evaluate(1 - time / duration);
            var size = scalingT * hitScaleIncrease * normalScale + normalScale;
            time += Time.deltaTime;
            SetSize(size);
            yield return eof;
         }
      }

   }
}