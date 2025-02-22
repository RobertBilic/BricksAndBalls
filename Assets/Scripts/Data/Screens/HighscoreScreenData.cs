using System.Collections.Generic;
using UnityEngine.Events;

namespace BricksAndBalls.Data.Screens
{
   public class HighscoreScreenData : ScreenData
   {
      public List<HighScoreEntry> Scores;
      public int PlayerRank;
      public UnityAction PlayAgainAction; 
   }

   public class HighScoreEntry
   {
      public string PlayerName;
      public int Score;
   }
}
