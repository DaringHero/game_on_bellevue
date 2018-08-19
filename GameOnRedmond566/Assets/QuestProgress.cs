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
        foreach(YellOnClaim.Location loc in (YellOnClaim.Location[])System.Enum.GetValues(typeof(YellOnClaim.Location)))
        {
            string res = myYellOnClaim.GetComponent<DictionariesForThings>().EnumLocation2Resource[loc];
            int questinfo = myYellOnClaim.MyCurrentToy.customData.GetInt(res, -1);
            if (questinfo == 0)
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
