//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class QuestRequirement
//{
//  public int count;
//  public Item item;
//}

//[System.Serializable]
//public class Quest
//{
//  public string name;
//  public int ID;
//  public QuestRequirement[] requirements;
  
//  public bool[] checkRequirements(Player player)
//  {
//    bool[] status = new bool[requirements.Length];

//    int counter = 0;
//    foreach (QuestRequirement qr in requirements)
//    {
//      status[counter] = (player.getItemCount(qr.item) >= qr.count);
//      Debug.Log(status[counter]);
//      ++counter;
//    }

//    return status;
//  }
//}

//public class QuestManager : MonoBehaviour
//{
//  public Quest[] quests;

//  private static QuestManager inst;

//  public static Quest[] Quests
//  {
//    get
//    {
//      return inst.quests;
//    }
//  }

//  void Awake()
//  {
//    inst = this;
//    DontDestroyOnLoad(gameObject);
//  }
//}
