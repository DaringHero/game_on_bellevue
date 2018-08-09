using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceReminder : MonoBehaviour {

    public YellOnClaim myYellOnClaim;
    public CheckQuestStatus myCheckQuestStatus;

    public List<GameObject> allResources;

    //ui elements
    public GameObject ResourceText1;
    public GameObject ResourceSprite1;
    public GameObject ResourceText2;
    public GameObject ResourceSprite2;
    public GameObject ResourceText3;
    public GameObject ResourceSprite3;

    public GameObject WrongLocationDisplay;
    public GameObject IsQuestLocationDisplay;

    public float waittime = 3.0f;
    public GameObject Activate;
    public GameObject Deactivate;

    public void DisableAllResourceReadouts()
    {
        this.ResourceText1.SetActive(false);
        this.ResourceSprite1.SetActive(false);
        this.ResourceText2.SetActive(false);
        this.ResourceSprite2.SetActive(false);
        this.ResourceText3.SetActive(false);
        this.ResourceSprite3.SetActive(false);
    }

    public  void OnEnable()
    {
        int currentQuest = this.myYellOnClaim.MyCurrentToy.customData.GetInt("CurrentQuest", -1);

        if ((int)this.myYellOnClaim.currentLocation == currentQuest)// are we in the right location for the player?
        {
            this.IsQuestLocationDisplay.SetActive(true);// set the right display
            this.WrongLocationDisplay.SetActive(false);
            this.DisableAllResourceReadouts();// turn off displays to be turned on as needed

            List<KeyValuePair<string, int>> temp = this.myCheckQuestStatus.GetPlayerQuestResourceData();

            if(temp[1].Value <1)
            {
                ResourceText1.SetActive(true);
                ResourceSprite1.SetActive(true);
                ResourceText1.GetComponent<Text>().text = temp[1].Key;
                ResourceSprite1.GetComponent<Image>().sprite = this.GetResourceSprite(temp[1].Key);
            }
            if (temp[2].Value < 1)
            {
                ResourceText2.SetActive(true);
                ResourceSprite2.SetActive(true);
                ResourceText2.GetComponent<Text>().text = temp[2].Key;
                ResourceSprite2.GetComponent<Image>().sprite = this.GetResourceSprite(temp[2].Key);
            }
            if (temp[3].Value < 1)
            {
                ResourceText3.SetActive(true);
                ResourceSprite3.SetActive(true);
                ResourceText3.GetComponent<Text>().text = temp[3].Key;
                ResourceSprite3.GetComponent<Image>().sprite = this.GetResourceSprite(temp[3].Key);
            }

        }
        else
        {
            this.WrongLocationDisplay.SetActive(true);
            this.IsQuestLocationDisplay.SetActive(false);// set the right display
        }

        this.StartCoroutine(this.WaitAndThenActivate());
    }

    public Sprite GetResourceSprite(string resoruceName)
    {
        for (int i = 0; i < this.allResources.Count; i++)
        {
            if (resoruceName.ToLower() == this.allResources[i].GetComponent<OnClickHarvest>().ResourceType)// get resource associated with the name
            {
                return this.allResources[i].GetComponent<Image>().sprite;
            }
        }

        return null;
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
