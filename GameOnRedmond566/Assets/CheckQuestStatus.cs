﻿using System.Collections;
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
        int currentQuest = this.myYellOnClaim.MyCurrentToy.customData.GetInt("CurrentQuest", -1);
        Debug.Log("CheckQuestStatus currentQuest ="+currentQuest.ToString());
        if (currentQuest <= 0)//sanc
        {
            Debug.Log("get new quest");
            //assign a quest
            this.Activate = this.ActivateQuestNone;// player needs to get a quest!
        }
        else
        {
            Debug.Log("fetching quest?");
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

                        int currentQuestsCompleted = myYellOnClaim.MyCurrentToy.customData.GetInt("QuestsCompleted", -999);
                        if(currentQuestsCompleted == -999)//if i havent completed any quests
                         this.myYellOnClaim.MyCurrentToy.customData.AddInt("QuestsCompleted", 1);// update number of quests completed
                        else
                        {
                            myYellOnClaim.MyCurrentToy.customData.SetInt("QuestsCompleted", ++currentQuestsCompleted);
                        }

                        this.myYellOnClaim.MyCurrentToy.customData.SetInt("CurrentQuest", -1);// reset the current quest to 'no quest'
                        this.myYellOnClaim.MyCurrentToy.customData.SetBool("DragonUpgraded", true);//true show dragon upgrade

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
                myYellOnClaim.WriteToErrorLog("CheckQuestStatus did not find quest region!");
            }
        }

        //for the special quest
        if(myYellOnClaim.isPlayerCurrentlyOnASpecialQuest)
        {
            int specialquest = myYellOnClaim.MyCurrentToy.customData.GetInt("SpecialQuest", -999);
            if ((specialquest == -999) || (specialquest == 0)) //
            {
                //shouldnt get here, somethieng is wrong
                Debug.LogException(new MissingReferenceException("Player is Not on a Special Quest but we think they are!!1"));
            }
            else //special quest is now completed!
            {
                //what should we do here?
                //Activate = ActivateSpecialQuestComplete;
                //do some fireworks or something!

                //we dont have a special quest anymore
                myYellOnClaim.MyCurrentToy.customData.SetInt("SpecialQuest", 0);

                int currentSpecialQuestsCompleted = myYellOnClaim.MyCurrentToy.customData.GetInt("SpecialQuestsCompleted", -999);
                if (currentSpecialQuestsCompleted == -999)//if i havent completed any quests
                    this.myYellOnClaim.MyCurrentToy.customData.AddInt("SpecialQuestsCompleted", 1);// update number of quests completed
                else
                {
                    myYellOnClaim.MyCurrentToy.customData.SetInt("SpecialQuestsCompleted", ++currentSpecialQuestsCompleted);
                }


            }
        }//else we dont need a special quest, there is a chance to assign them in getquest.cs

        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }

    public List<string> GetQuestResourceStrings()// this function retruns a list of strings that are the quest resources for the current player
    {
        int currentQuest = this.myYellOnClaim.MyCurrentToy.customData.GetInt("CurrentQuest", -1);

        List<ResourceSpawner2> Regions = new List<ResourceSpawner2>(Resources.FindObjectsOfTypeAll<ResourceSpawner2>());// gets inactive objects
        bool fail = true;
        List<string> ret = new List<string>();

        for (int i = 0; i < Regions.Count; i++)
        {

            if ((int)Regions[i].location == currentQuest)//find region my quest is for
            {
                fail = false;//we found the location
                string resource1 = Regions[i].Resource1.GetComponent<OnClickHarvest>().ResourceType;
                string resource2 = Regions[i].Resource2.GetComponent<OnClickHarvest>().ResourceType;
                string resource3 = Regions[i].uniqueResource.GetComponent<OnClickHarvest>().ResourceType;
                ret.Add(resource3);
                ret.Add(resource2);
                ret.Add(resource1);
            }
        }

        return ret;
    }

}
