using BricksAndBalls.Data.Screens;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.UI.View
{
   public class HighScoreScreen: Screen<HighscoreScreenData>
   {
      [SerializeField]
      private ScrollRect scrollRect;
      [SerializeField]
      private LayoutGroup layoutGroup;
      [SerializeField]
      private HighScoreItem prefab;
      [SerializeField]
      private Button playAgain;

      public override void SetData(HighscoreScreenData data)
      {
         for(int i = 0; i < data.Scores.Count; ++i)
         {
            var item = GameObject.Instantiate(prefab, layoutGroup.transform);
            item.SetData(data.Scores[i].PlayerName, data.Scores[i].Score, i + 1, data.PlayerRank);
         }

         playAgain.onClick.RemoveAllListeners();
         playAgain.onClick.AddListener(data.PlayAgainAction);
         var t = 1.0f - Mathf.InverseLerp(1, data.Scores.Count, data.PlayerRank);

         StartCoroutine(ResizeLayoutGroup(t));
      }

      private IEnumerator ResizeLayoutGroup(float normalizePosition)
      {
         yield return new WaitForEndOfFrame();

         var rectTransform = layoutGroup.transform as RectTransform;
         rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, layoutGroup.preferredHeight);
         scrollRect.verticalNormalizedPosition = normalizePosition;
      }
   }
}
