using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestReminder : MonoBehaviour {

    public GetQuest myGetQuest;
    public Text myText;

    public void OnEnable()
    {
        this.myText.text = this.myGetQuest.GetQuestReminderText();
    }
}
