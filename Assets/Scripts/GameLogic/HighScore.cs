using BricksAndBalls.Data.Screens;
using UnityEngine;

namespace BricksAndBalls.GameLogic
{
   public delegate void HighScoreUpdateDelegate(int amount);
   
   public class HighScoreTracker
   {
      public void AddTrackerObject(IHighscoreModifier modifier)
      {
         modifier.SetHighScoreUpdateDelegate(ModifyHighscore);
      }

      private int HighScore;

      private void ModifyHighscore(int amount)
      {
         HighScore += amount;
      }

      public int GetHighScore() => HighScore;
   }

   public interface IHighscoreModifier
   {
      public void SetHighScoreUpdateDelegate(HighScoreUpdateDelegate del);
   }

}
