using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLocation : MonoBehaviour {
    /// <summary>
    /// random range for quest level
    /// </summary>
    public int RangeForEgg;
    public int RangeForBaby;
    public int RangeForYoungAdult;
    public int RangeForAdult;
    public int RangeForSanctuary;

    public YellOnClaim myYellOnClaim;

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //future: use haversian distance to calc closest station based on lat and longitude
    //could also use the dictionary in setstationpages to get locations instead of hardcoding them
    public List<YellOnClaim.Location> GetNewLocation(int numberoflocation)
    {
        List<YellOnClaim.Location> list2return = new List<YellOnClaim.Location>();
       // get a list of stations located in region we want
        if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCEAST) //only 2 stations, so 1 and 2 (swamp and mountain)
        {
            //direct players either to the other station(if they need it) or the RTCWEST stations
            list2return.Add(YellOnClaim.Location.SWAMP);
            list2return.Add(YellOnClaim.Location.MOUNTAIN);
            list2return.Add(YellOnClaim.Location.FOREST);
            list2return.Add(YellOnClaim.Location.LAKE);

            //figure out what resources/quests are done or dont need and cross those off the list (1 or -1, we only want the 0s)
            int ice = myYellOnClaim.MyCurrentToy.customData.GetInt("ICE", -1);
            int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
            int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
            int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
            
            if(ice == 1 || ice == -1)
            {
                list2return.Remove(YellOnClaim.Location.LAKE);
            }

            if(rock == 1 || rock == -1)
            {
                list2return.Remove(YellOnClaim.Location.MOUNTAIN);
            }

            if(wood == 1 || wood == -1)
            {
                list2return.Remove(YellOnClaim.Location.FOREST);
            }

            if (mush == 1 || mush == -1)
            {
                list2return.Remove(YellOnClaim.Location.SWAMP);
            }

            //if there are no choices, send them somewhere else (could add sanc here)
            if (list2return.Count == 0) //probs shouldnt be here
            {
                list2return.Add(YellOnClaim.Location.WIND);
                list2return.Add(YellOnClaim.Location.FARM);

                int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);
                int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);


                if (pinecone == 1 || pinecone == -1)
                {
                    list2return.Remove(YellOnClaim.Location.FARM);
                }

                if (reed == 1 || reed == -1)
                {

                    list2return.Remove(YellOnClaim.Location.WIND);
                }
            }

                return list2return;

        }
        else if(myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCWEST || myYellOnClaim.currentRegion == YellOnClaim.Regions.CLEVELAND) // 4 stations, above + FOREST and LAKE
        {
            //direct players to ALL stations
            list2return.Add(YellOnClaim.Location.WIND);
            list2return.Add(YellOnClaim.Location.FARM);
            list2return.Add(YellOnClaim.Location.SWAMP);
            list2return.Add(YellOnClaim.Location.MOUNTAIN);
            list2return.Add(YellOnClaim.Location.FOREST);
            list2return.Add(YellOnClaim.Location.LAKE);

                int ice = myYellOnClaim.MyCurrentToy.customData.GetInt("ICE", -1);
                int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
                int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
                int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
                int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);
                int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);

                if (ice == 1 || ice == -1)
                {
                    list2return.Remove(YellOnClaim.Location.LAKE);
                }

                if (rock == 1 || rock == -1)
                {
                    list2return.Remove(YellOnClaim.Location.MOUNTAIN);
                }

                if (wood == 1 || wood == -1)
                {
                    list2return.Remove(YellOnClaim.Location.FOREST);
                }

                if (mush == 1 || mush == -1)
                {
                    list2return.Remove(YellOnClaim.Location.SWAMP);
                }


                if (pinecone == 1 || pinecone == -1)
                {
                    list2return.Remove(YellOnClaim.Location.FARM);
                }

                if (reed == 1 || reed == -1)
                {

                    list2return.Remove(YellOnClaim.Location.WIND);
                }



                return list2return;

            }

         return list2return;
    }
}
