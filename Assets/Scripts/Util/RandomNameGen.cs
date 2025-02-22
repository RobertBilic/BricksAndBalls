using System.Collections.Generic;
using UnityEngine;

namespace BricksAndBalls.Util
{
   public class RandomNameGen
   {
      public static string GetRandomName()
      {
         return $"{firstNames[Random.Range(0, firstNames.Count)]} {lastNames[Random.Range(0, lastNames.Count)]}";
      }

      private static List<string> firstNames = new List<string>()
      {
            "John",
            "Jane",
            "Michael",
            "Emily",
            "William",
            "Olivia",
            "James",
            "Emma",
            "Alexander",
            "Sophia",
            "Daniel",
            "Isabella",
            "Matthew",
            "Ava",
            "Christopher",
            "Mia",
            "Andrew",
            "Abigail",
            "Joseph",
            "Ella",
            "David",
            "Grace",
            "Nicholas",
            "Lily",
            "Benjamin",
            "Chloe",
            "Samuel",
            "Madison",
            "Ethan",
            "Aiden"
      };

      private static List<string> lastNames = new List<string>()
      {
            "Smith",
            "Johnson",
            "Williams",
            "Jones",
            "Brown",
            "Davis",
            "Miller",
            "Wilson",
            "Moore",
            "Taylor",
            "Anderson",
            "Thomas",
            "Jackson",
            "White",
            "Harris",
            "Martin",
            "Thompson",
            "Garcia",
            "Martinez",
            "Robinson",
            "Clark",
            "Rodriguez",
            "Lewis",
            "Lee",
            "Walker",
            "Hall",
            "Allen",
            "Young",
            "King",
            "Wright"
      };

   }
}
