//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuestMenu : MonoBehaviour
//{
//  public Text Title;
//  public QuestPanel Template;
//  private QuestPanel[] activeQuestPanels;

//  public void setCurrentQuest(Quest quest, bool[] completion)
//  {
//    if (activeQuestPanels != null)
//    {
//      foreach (QuestPanel qp in activeQuestPanels)
//      {
//        Destroy(qp.gameObject);
//      }
//    }

//    Title.text = quest.name;
    
//    activeQuestPanels = new QuestPanel[quest.requirements.Length];

//    int counter = 0;
//    foreach (QuestRequirement req in quest.requirements)
//    {
//      activeQuestPanels[counter] = Instantiate(Template.gameObject, Template.transform.position, new Quaternion(), transform).GetComponent<QuestPanel>();
//      activeQuestPanels[counter].transform.position = Template.transform.position - new Vector3(0, 50 * (counter), 0);

//      activeQuestPanels[counter].complete = completion[counter];
//      activeQuestPanels[counter].setItem(req.count, req.item);
//      activeQuestPanels[counter].gameObject.SetActive(true);

//      ++counter;
//    }
//  }
//}
