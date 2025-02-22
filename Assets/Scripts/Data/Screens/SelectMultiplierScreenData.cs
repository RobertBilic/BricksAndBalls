namespace BricksAndBalls.Data.Screens
{
   public delegate void OnMultiplierSelected(int multiplier);
   public class SelectMultiplierScreenData : ScreenData
   {
      public int[] Multipliers;
      public OnMultiplierSelected MultiplierSelected;
   }
}
