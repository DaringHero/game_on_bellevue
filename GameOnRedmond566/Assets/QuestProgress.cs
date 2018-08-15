using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgress : MonoBehaviour {

    public YellOnClaim myYellOnClaim;

    public bool StationIsForQuest()// scale for sanctuary
    {
        string myCurrentResource = myYellOnClaim.gameObject.GetComponent<DictionariesForThings>().Location2Resource[this.myYellOnClaim.currentLocation.ToString()];

        int temp = myYellOnClaim.MyCurrentToy.customData.GetInt(myCurrentResource, -1);

        if (temp == 0)// already been there or dont need to go there
        {
            return true;
        }

        return false;
    }


    public bool CompletedAllQuests()
    {
        int ice  = myYellOnClaim.MyCurrentToy.customData.GetInt("ICE", -1);
        int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
        int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
        int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
        int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);
        int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);

        if (ice == 0 || rock == 0 || wood == 0)
        {
            return false;
        }
        if (mush == 0 || reed == 0 || pinecone == 0)
        {
            return false;
        }
        return true;
    }
    
    public void SetQuests(List<YellOnClaim.Location> locations)
    {
        foreach(YellOnClaim.Location loc in locations)
        {
            //set resource to 0, meaning it needs to be collected (-1 means not considered, 1 is collected)
            myYellOnClaim.MyCurrentToy.customData.SetInt(myYellOnClaim.gameObject.GetComponent<DictionariesForThings>().Location2Resource[myYellOnClaim.currentLocation.ToString()], 0);
        }
    }
}
