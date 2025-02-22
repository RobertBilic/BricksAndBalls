using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.UI
{
   public class HighScoreItem : MonoBehaviour
   {
      [SerializeField]
      private Color playerColor;
      [SerializeField]
      private Color standardColor;

      [SerializeField]
      private TextMeshProUGUI rank;
      [SerializeField]
      private TextMeshProUGUI playerName;
      [SerializeField]
      private TextMeshProUGUI score;
      [SerializeField]
      private Image playerIndicator;

      public void SetData(string playerName, int score, int rank, int playerRank)
      {
         this.playerIndicator.color = rank == playerRank ? playerColor : standardColor;
         this.rank.text = $"{rank}.";
         this.playerName.text = playerName;
         this.score.text = score.ToString();
      }

   }
}
