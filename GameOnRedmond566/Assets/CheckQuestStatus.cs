using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuestStatus : MonoBehaviour {

    public float waittime = 3.0f;
    public GameObject ActivateQuestContinue;
    public GameObject ActivateQuestComplete;
    public GameObject ActivateQuestNone;
    private GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;

    private void OnEnable()
    {
        int currentQuest = this.myYellOnClaim.MyCurrentToy.customData.GetInt("CurrentQuest", 0);

        if (currentQuest == 0)//sanc
        {
            //assign a quest
            this.Activate = this.ActivateQuestNone;// player needs to get a quest!
        }
        else
        {
            List<ResourceSpawner2> Regions = new List<ResourceSpawner2>(Resources.FindObjectsOfTypeAll<ResourceSpawner2>());// gets inactive objects
            bool fail = true;
            for (int i = 0; i < Regions.Count; i++)
            {

                if ((int)Regions[i].location == currentQuest)//find region my quest is for
                {
                     fail = false;//we found the location
                    string resource1 = Regions[i].Resource1.GetComponent<OnClickHarvest>().ResourceType;
                   string resource2 = Regions[i].Resource2.GetComponent<OnClickHarvest>().ResourceType;
                   string resource3 = Regions[i].uniqueResource.GetComponent<OnClickHarvest>().ResourceType;

                    int resCount1 = this.myYellOnClaim.MyCurrentToy.customData.GetInt(resource1, 0);
                    int resCount2 = this.myYellOnClaim.MyCurrentToy.customData.GetInt(resource2, 0);
                    int resCount3 = this.myYellOnClaim.MyCurrentToy.customData.GetInt(resource3, 0);

                    if (resCount1 > 0 && resCount2 > 0 && resCount3 > 0)
                    {
                        //remove resources
                        this.myYellOnClaim.MyCurrentToy.customData.SetInt(resource1, resCount1 - 1);
                        this.myYellOnClaim.MyCurrentToy.customData.SetInt(resource2, resCount2 - 1);
                        this.myYellOnClaim.MyCurrentToy.customData.SetInt(resource3, resCount3 - 1);
                        this.myYellOnClaim.MyCurrentToy.customData.AddInt("QuestsCompleted", 1);// update number of quests completed

                        this.myYellOnClaim.MyCurrentToy.customData.SetInt("CurrentQuest", 0);// reset the current quest

                        this.Activate = this.ActivateQuestComplete;// player has completed quest
                        
                    }
                    else
                    {
                        this.Activate = this.ActivateQuestContinue;// player has to continue quest

                    }

                }
            }
            if (fail)// failed to find quest region
            {
                Debug.LogException( new MissingReferenceException("CheckQuestStatus did not find quest region!"));
            }
        }

        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
