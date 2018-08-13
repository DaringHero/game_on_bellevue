﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgress : MonoBehaviour {

    public YellOnClaim myYellOnClaim;

    bool StationIsForQuest()// scale for sanctuary
    {
        string myCurrentResource = myYellOnClaim.Location2Resource[this.myYellOnClaim.currentLocation.ToString()];

        return false;
    }


    bool CompletedAllQuests()
    {
        int ice = myYellOnClaim.MyCurrentToy.customData.GetInt("ICE", -1);
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
}
