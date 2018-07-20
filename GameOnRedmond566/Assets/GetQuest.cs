using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetQuest : MonoBehaviour
{
    public float waittime = 3.0f;

    public GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;

    public List<string> QuestStrings;//0.SANC, 1.SWAMP, 2.MOUNTAIN, 3.FOREST, 4.LAKE, 5.WIND //only 4 for now

    public Text DisplayText;

    private void OnEnable()
    {
        int nextQuest = Random.Range(1, QuestStrings.Count);
        this.myYellOnClaim.MyCurrentToy.customData.SetInt("CurrentQuest", nextQuest);

        this.DisplayText.text = this.QuestStrings[nextQuest];

        Debug.Log("Quest = "+ ((YellOnClaim.Location)nextQuest).ToString());
        GUIUtility.systemCopyBuffer += "\n" + System.DateTime.Now + " " + "Quest is " + ((YellOnClaim.Location)nextQuest).ToString();
        StartCoroutine(WaitAndThenActivate());

    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
