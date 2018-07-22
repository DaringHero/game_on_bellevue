//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BittoysDataException : System.Exception
//{
//  public BittoysDataException(string what)
//  {
//    exc = "Questor:" + what;
//  }

//  private string exc;

//  public string what()
//  {
//    return exc;
//  }
//}

//public enum Item
//{
//  INVALID,
//  Wheat,
//  Iron,
//  Water,

//  RegularCount,

//  SpecialWheat,
//  SpecialIron,
//  SpecialWater,

//  TotalCount
//}

//public class ItemManager
//{
//  private static string[] ItemNames =
//  {
//    "INVALID",
//    "Wheat",
//    "Iron",
//    "Water",
//    "INVALID",
//    "Special Wheat",
//    "Special Iron",
//    "Special Water"
//  };

//  private static string[] IconLocation =
//  {
//    "INVALID",
//    "Images/icons/Wheat",
//    "Images/icons/Iron",
//    "Images/icons/Water",
//    "INVALID",
//    "Images/icons/SpecialWheat",
//    "Images/icons/BrilliantGold",
//    "Images/icons/EnchantedWater",
//  };

//  public static string getName(Item i)
//  {
//    return ItemNames[(int) i];
//  }

//  public static string getSpritePath(Item i)
//  {
//    return IconLocation[(int)i];
//  }

//  public static Item randomItem()
//  {
//    return (Item)(Random.Range(1, (int)Item.RegularCount));
//  }
//}

//public enum ShardColor
//{
//  INVALID,
//  Red,
//  Yellow,
//  Blue,
//  Orange,
//  Green,
//  Purple,

//  Count
//}

//public class ShardManager
//{
//  private static readonly string[] ShardNames =
//  {
//    "INVALID",
//    "Red",
//    "Yellow",
//    "Blue",
//    "Orange",
//    "Green",
//    "Purple"
//  };

//  private static readonly Color[] actualColorValues =
//  {
//    new Color(1,    1, 1),
//    new Color(1,    0, 0),
//    new Color(1,    1, 0),
//    new Color(0,    0, 1),
//    new Color(1, 0.5f, 0),
//    new Color(0,    1, 0),
//    new Color(1,    0, 1)
//  };

//  public static Color getColor(ShardColor sc)
//  {
//    return actualColorValues[(int) sc];
//  }

//  public static string getName(ShardColor sc)
//  {
//    return ShardNames[(int) sc];
//  }

//  public static ShardColor randomShardColor()
//  {
//    return (ShardColor)Random.Range((int)ShardColor.Red, (int) ShardColor.Blue + 1);
//  }

//  public static ShardColor Add(ShardColor color1, ShardColor color2)
//  {
//    switch(color1)
//    {
//      case ShardColor.Red:
//        {
//          switch(color2)
//          {
//            case ShardColor.Yellow:
//              return ShardColor.Orange;
//            case ShardColor.Blue:
//              return ShardColor.Purple;
//          }
//        }
//        break;
//      case ShardColor.Yellow:
//        {
//          switch (color2)
//          {
//            case ShardColor.Red:
//              return ShardColor.Orange;
//            case ShardColor.Blue:
//              return ShardColor.Green;
//          }
//        }
//        break;
//      case ShardColor.Blue:
//        {
//          switch (color2)
//          {
//            case ShardColor.Red:
//              return ShardColor.Purple;
//            case ShardColor.Yellow:
//              return ShardColor.Green;
//          }
//        }
//        break;
//    }

//    return ShardColor.INVALID;
//  }
//}

//public class Player
//{
//  public static Player RegisterPlayer(ToyWrapper toy, string username, int team, ShardColor aff)
//  {
//    Player player = new Player(toy);
//    toy.customData.SetString("username", username);
//    toy.customData.SetBool("registered", true);

//    toy.customData.SetInt("affinity", (int)aff);

//    toy.customData.SendAsync();

//    return player;
//  }

//  public Player(ToyWrapper bittoy)
//  {
//    toy = bittoy;
//  }

//  public int completedQuestCounter
//  {
//    get
//    {
//      return toy.customData.GetInt("completedQuestCounter", 0);
//    }
//    set
//    {
//      toy.customData.SetInt("completedQuestCounter", value);
//    }
//  }

//  public int activeQuest
//  {
//    get
//    {
//      return toy.customData.GetInt("activeQuest", -1);
//    }
//    set
//    {
//      toy.customData.SetInt("activeQuest", value);
//    }
//  }

//  public bool registered
//  {
//    get
//    {
//      return toy.customData.GetBool("registered", false);
//    }
//  }

//  public string username
//  {
//    get
//    {
//      return toy.customData.GetString("username", "Name_Err");
//    }
//    set
//    {
//      toy.customData.SetString("username", value);
//    }
//  }

//  public ShardColor shardAffinity
//  {
//    get
//    {
//      return (ShardColor)toy.customData.GetInt("affinity", 0);
//    }
//  }
  
//  public int getItemCount(Item i)
//  {
//    return toy.customData.GetInt("inv_" + ItemManager.getName(i), 0);
//  }

//  public void takeItem(Item i, int amount)
//  {
//    giveItem(i, -amount);
//  }

//  public void giveItem(Item i, int amount)
//  {
//    int curr = getItemCount(i);
//    toy.customData.SetInt("inv_" + ItemManager.getName(i), curr + amount);
//  }

//  public void syncPlayer()
//  {
//    toy.customData.SendAsync();
//  }
  
//  private ToyWrapper toy;
//}

//public class Challenge
//{
//  public Challenge(ShardColor player, ShardColor support)
//  {
//    this.playerColor = player;
//    this.supportColor = support;
//  }

//  // Color1, active players color
//  public ShardColor playerColor;

//  // Color2, new color for mixing
//  public ShardColor supportColor;

//  public ShardColor getChallengeColor()
//  {
//    return ShardManager.Add(playerColor, supportColor);
//  }
//}

//public class Questor
//{
//  public static Quest GetQuest(Location loc)
//  {
//    int randIndx = Random.Range(0, loc.questIDs.Length);

//    return QuestManager.Quests[loc.questIDs[randIndx]];
//  }

//  public static List<Item> GetItems(Location loc, Player activePlayer)
//  {
//    List<Item> items = new List<Item>();
//    for(int i = 0; i < 3; ++i)
//    {
//      items.Add(ItemManager.randomItem());
//    }

//    Debug.Log(loc.specificName);
//    Debug.Log(activePlayer.activeQuest);

//    if (activePlayer.activeQuest != -1)
//    {
//      Quest q = QuestManager.Quests[activePlayer.activeQuest];
//      bool requiresUniqueItem = false;

//      foreach (QuestRequirement qr in q.requirements)
//      {
//        if (qr.item == loc.uniqueItem)
//        {
//          requiresUniqueItem = true;
//          break;
//        }
//      }

//      if (requiresUniqueItem == true)
//      {
//        items.Add(loc.uniqueItem);
//      }
//    }
//    return items;
//  }

//  public static bool IsThereAChallenge(Location loc, Player activePlayer)
//  {
//    return true; // (Random.Range(0, 5) == 0);
//  }

//  public static Challenge GetChallenge(Location loc, Player activePlayer)
//  {
//    ShardColor playerColor = activePlayer.shardAffinity;

//    int rand = Random.Range(1, 3); // inclusive - exclusive
//    ShardColor randColor = (ShardColor)((((((int)playerColor) - 1) + rand) % 3) + 1);

//    return new Challenge(activePlayer.shardAffinity, randColor);
//  }
//}