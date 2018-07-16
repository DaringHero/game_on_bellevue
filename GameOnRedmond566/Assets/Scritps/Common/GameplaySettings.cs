using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp code to define teams, eventually this needs to move to the cloud.

public struct TeamDefinition
{
  public string name;
}

public class GameplaySettings
{
  // Team info

  public static readonly TeamDefinition[] teams =
  {
    new TeamDefinition { name = "Invalid" },
    new TeamDefinition { name = "Red" },
    new TeamDefinition { name = "Yellow" },
    new TeamDefinition { name = "Blue" },
  };

  public static string GetTeamNameFromID(int id)
  {
    return teams[id].name;
  }
}
