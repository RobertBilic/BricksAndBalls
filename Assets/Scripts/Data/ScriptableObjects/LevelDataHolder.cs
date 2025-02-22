using BricksAndBalls.Data.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelDataHOlder",menuName = "ScriptableObjects/LevelDataHolder")]
public class LevelDataHolder : ScriptableObject
{
   public GameLevelData gameLevelData;
}
