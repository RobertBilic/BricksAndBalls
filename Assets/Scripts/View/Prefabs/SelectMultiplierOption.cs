using BricksAndBalls.Data.Screens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BricksAndBalls.UI
{
   public class SelectMultiplierOption: MonoBehaviour
   {
      [SerializeField]
      private Button button;
      [SerializeField]
      private TextMeshProUGUI multiplierText;

      public void SetData(int multiplier, OnMultiplierSelected action)
      {
         multiplierText.text = $"{multiplier.ToString()}x";
         button.onClick.AddListener(() => action(multiplier));
      }
   }
}