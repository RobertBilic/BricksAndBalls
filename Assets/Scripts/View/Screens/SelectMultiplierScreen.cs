using BricksAndBalls.Data.Screens;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BricksAndBalls.UI.View
{
   public class SelectMultiplierScreen: Screen<SelectMultiplierScreenData>
   {
      [SerializeField]
      private Transform content;
      [SerializeField]
      private SelectMultiplierOption prefab;
      
      public override void SetData(SelectMultiplierScreenData data)
      {
         for(int i = 0; i < data.Multipliers.Length; ++i)
         {
            var option = GameObject.Instantiate(prefab, content);
            option.SetData(data.Multipliers[i], data.MultiplierSelected);
         }
      }
   }
}
