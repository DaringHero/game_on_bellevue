using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLocation : MonoBehaviour {
    /// <summary>
    /// random range for quest level
    /// </summary>
    public int RangeForEgg;
    public int RangeForBaby;
    //public int RangeForYoungAdult;
    public int RangeForAdult;
    public int RangeForSanctuary;

    public YellOnClaim myYellOnClaim;

    

    //future: use haversian distance to calc closest station based on lat and longitude
    //could also use the dictionary in setstationpages to get locations instead of hardcoding them
    public  List<KeyValuePair<YellOnClaim.Location, bool>> GetUpdatedLocation()
    {
        List<KeyValuePair<YellOnClaim.Location, bool>> list2return = new List<KeyValuePair<YellOnClaim.Location, bool>>();

       // get a list of stations located in region we want
        if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCEAST) //only 2 stations, so 1 and 2 (swamp and mountain)
        {
            //direct players either to the other station(if they need it) or the RTCWEST stations
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>( YellOnClaim.Location.SWAMP, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, false));

            //figure out what resources/quests are done or dont need and cross those off the list (1 or -1, we only want the 0s)
            int ice = myYellOnClaim.MyCurrentToy.customData.GetInt(myYellOnClaim.GetComponent<DictionariesForThings>().Location2Resource["LAKE"], -1);
            int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
            int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
            int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
            int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);
            int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);

            if (ice == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
            }

            if(rock == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
            }

            if(wood == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
            }

            if (mush == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
            }



            //now check for checkmarks
            if (ice == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, true));
            }

            if (rock == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, true));
            }

            if (wood == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, true));
            }

            if (mush == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.SWAMP, true));
            }

            if(reed == 1)
            {
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.WIND, true));
            }

            if(pinecone == 1)
            {
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FARM, true));
            }


                return list2return;

        }
        else if(myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCWEST || myYellOnClaim.currentRegion == YellOnClaim.Regions.CLEVELAND) // 4 stations, above + FOREST and LAKE
        {
            //direct players either to the other station(if they need it) or the RTCWEST stations
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.SWAMP, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.WIND, false));
            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FARM, false));

            //figure out what resources/quests are done or dont need and cross those off the list (1 or -1, we only want the 0s)
            int ice = myYellOnClaim.MyCurrentToy.customData.GetInt("ICE", -1);
            int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
            int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
            int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
            int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);
            int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);

            if (ice == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
            }

            if (rock == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
            }

            if (wood == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
            }

            if (mush == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
            }

            if (reed == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.WIND));
            }

            if (pinecone == -1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FARM));
            }


            //now check for checkmarks
            if (ice == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, true));
            }

            if (rock == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, true));
            }

            if (wood == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, true));
            }

            if (mush == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.SWAMP, true));
            }

            if (reed == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.WIND));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.WIND, true));
            }

            if (pinecone == 1)
            {
                list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FARM));
                list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FARM, true));
            }


            return list2return;






            }

         return list2return;
    }

    //use this for each new quest time, i.e. when we get an egg, we will call this function and get what locations we need ("you have new quests")
    public List<YellOnClaim.Location> GetNewLocation(bool finalquest)
    {
        //can also have a list of resources, but whoever uses this function can use the location to index into the dictionary and get the resource
        List<YellOnClaim.Location> ret = new List<YellOnClaim.Location>();

        //add all locations
        foreach (YellOnClaim.Location loc in System.Enum.GetValues(typeof(YellOnClaim.Location)))
        {
            ret.Add(loc);
        }

        //if its the final quest, remove the scale
        if (finalquest)
        {
            ret.Clear();
            ret.Add((YellOnClaim.Location)0);
        }

        //for each location, remove the ones we dont want
        if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCEAST) //only 2 stations, so 1 and 2 (swamp and mountain)
        {
            //take away the far ones
            ret.Remove(YellOnClaim.Location.FARM);
            ret.Remove(YellOnClaim.Location.WIND);

        }
        else if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCWEST)
        {
            //dont take away any
        }
        else if (myYellOnClaim.currentRegion == YellOnClaim.Regions.CLEVELAND)
        {
            //dont take away any
        }

        //remove our current location (we dont want the situation where they get new quests and then have to scan in again immediately)
        ret.Remove(myYellOnClaim.currentLocation);

        return ret;
    }

}
