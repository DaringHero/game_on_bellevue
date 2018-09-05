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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            TestFunctions();
    }


    //future: use haversian distance to calc closest station based on lat and longitude
    //could also use the dictionary in setstationpages to get locations instead of hardcoding them
    //public  List<KeyValuePair<YellOnClaim.Location, bool>> GetUpdatedLocation()
    //{
    //    List<KeyValuePair<YellOnClaim.Location, bool>> list2return = new List<KeyValuePair<YellOnClaim.Location, bool>>();

    //   // get a list of stations located in region we want
    //    if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCEAST) //only 2 stations, so 1 and 2 (swamp and mountain)
    //    {
    //        //direct players either to the other station(if they need it) or the RTCWEST stations
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>( YellOnClaim.Location.SWAMP, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, false));

    //        //figure out what resources/quests are done or dont need and cross those off the list (1 or -1, we only want the 0s)
    //        int ice = myYellOnClaim.MyCurrentToy.customData.GetInt(myYellOnClaim.GetComponent<DictionariesForThings>().Location2Resource["LAKE"], -1);
    //        int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
    //        int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
    //        int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
    //        int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);
    //        int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);

    //        if (ice == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
    //        }

    //        if(rock == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
    //        }

    //        if(wood == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
    //        }

    //        if (mush == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
    //        }



    //        //now check for checkmarks
    //        if (ice == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, true));
    //        }

    //        if (rock == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, true));
    //        }

    //        if (wood == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, true));
    //        }

    //        if (mush == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.SWAMP, true));
    //        }

    //        if(reed == 1)
    //        {
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.WIND, true));
    //        }

    //        if(pinecone == 1)
    //        {
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FARM, true));
    //        }


    //            return list2return;

    //    }
    //    else if(myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCWEST || myYellOnClaim.currentRegion == YellOnClaim.Regions.CLEVELAND) // 4 stations, above + FOREST and LAKE
    //    {
    //        //direct players either to the other station(if they need it) or the RTCWEST stations
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.SWAMP, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.WIND, false));
    //        list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FARM, false));

    //        //figure out what resources/quests are done or dont need and cross those off the list (1 or -1, we only want the 0s)
    //        int ice = myYellOnClaim.MyCurrentToy.customData.GetInt("ICE", -1);
    //        int rock = myYellOnClaim.MyCurrentToy.customData.GetInt("ROCK", -1);
    //        int wood = myYellOnClaim.MyCurrentToy.customData.GetInt("WOOD", -1);
    //        int mush = myYellOnClaim.MyCurrentToy.customData.GetInt("MUSH", -1);
    //        int reed = myYellOnClaim.MyCurrentToy.customData.GetInt("REED", -1);
    //        int pinecone = myYellOnClaim.MyCurrentToy.customData.GetInt("PINECONE", -1);

    //        if (ice == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
    //        }

    //        if (rock == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
    //        }

    //        if (wood == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
    //        }

    //        if (mush == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
    //        }

    //        if (reed == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.WIND));
    //        }

    //        if (pinecone == -1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FARM));
    //        }


    //        //now check for checkmarks
    //        if (ice == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.LAKE));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.LAKE, true));
    //        }

    //        if (rock == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.MOUNTAIN));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.MOUNTAIN, true));
    //        }

    //        if (wood == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FOREST));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FOREST, true));
    //        }

    //        if (mush == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.SWAMP));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.SWAMP, true));
    //        }

    //        if (reed == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.WIND));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.WIND, true));
    //        }

    //        if (pinecone == 1)
    //        {
    //            list2return.RemoveAll(x => x.Key.Equals(YellOnClaim.Location.FARM));
    //            list2return.Add(new KeyValuePair<YellOnClaim.Location, bool>(YellOnClaim.Location.FARM, true));
    //        }


    //        return list2return;






    //        }

    //     return list2return;
    //}

    public void TestFunctions()
    {
        Debug.Log("**************************");
        Debug.Log("**************************");
        List<YellOnClaim.Location> newlist = (GetNewLocation(false));
        foreach(YellOnClaim.Location a in newlist)
        {
            Debug.Log(a);
        }
        Debug.Log("**************************");
        Debug.Log("**************************");
    }
    //TODO for s - use final quest for sanc or not, make sure this works with some tests
    public List<YellOnClaim.Location> GetNewLocation(bool finalquest) //for now its 4
    {
        List<YellOnClaim.Location> ret = new List<YellOnClaim.Location>();
        List<int> t = myYellOnClaim.GetRandomUniqueNumbers(7, 4);
        foreach(int i in t)
        {
            int thing2add = i;
            if(i == (int)myYellOnClaim.currentLocation)
            {
                thing2add %= 6;
            }
            else
                ret.Add((YellOnClaim.Location)thing2add);
        }
        return ret;
    }


    //use this for each new quest time, i.e. when we get an egg, we will call this function and get what locations we need ("you have new quests")
    //public List<YellOnClaim.Location> GetNewLocation(bool finalquest) //for now its 3
    //{
    //    //can also have a list of resources, but whoever uses this function can use the location to index into the dictionary and get the resource
    //    List<YellOnClaim.Location> ret = new List<YellOnClaim.Location>();

    //    //add all locations
    //    foreach (YellOnClaim.Location loc in System.Enum.GetValues(typeof(YellOnClaim.Location)))
    //    {
    //        ret.Add(loc);
    //    }

    //    //remove our current location (we dont want the situation where they get new quests and then have to scan in again immediately)
    //    ret.Remove(myYellOnClaim.currentLocation);


    //    //if its the final quest, remove the scale
    //    if (finalquest)
    //    {
    //        ret.Clear();
    //        ret.Add((YellOnClaim.Location)0);
    //        return ret;
    //    }
    //    else
    //    {
    //        if(ret.Contains(YellOnClaim.Location.SANC))
    //        ret.Remove(YellOnClaim.Location.SANC);
    //    }

    //    //for each location, remove the ones we dont want
    //    if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCEAST) //only 2 stations, so 1 and 2 (swamp and mountain)
    //    {        
    //        //take away the far ones (by setting max range to 4)
    //        ret.Remove(YellOnClaim.Location.FARM);
    //        ret.Remove(YellOnClaim.Location.WIND);



    //     //   List<int> convertlist = myYellOnClaim.GetRandomUniqueNumbers(4, 2, (int)myYellOnClaim.currentLocation);
    //    //    foreach(int i in convertlist)
    //    //    {
    //    //        if(ret.Contains((YellOnClaim.Location)i))
    //    //            ret.Remove((YellOnClaim.Location)i);
    //    //    }


    //    }
    //    else if (myYellOnClaim.currentRegion == YellOnClaim.Regions.RTCWEST)
    //    {
    //        //take away 2 randomly
    //        List<int> convertlist = myYellOnClaim.GetRandomUniqueNumbers(6, 2, (int)myYellOnClaim.currentLocation);
    //        foreach (int i in convertlist)
    //        {
    //            if (ret.Contains((YellOnClaim.Location)i))
    //                ret.Remove((YellOnClaim.Location)i);
    //        }
    //    }
    //    else if (myYellOnClaim.currentRegion == YellOnClaim.Regions.CLEVELAND)
    //    {
    //        //take away 2 randomly
    //        List<int> convertlist = myYellOnClaim.GetRandomUniqueNumbers(6, 2, (int)myYellOnClaim.currentLocation);
    //        foreach (int i in convertlist)
    //        {
    //            if (ret.Contains((YellOnClaim.Location)i))
    //                ret.Remove((YellOnClaim.Location)i);
    //        }
    //    }



    //    return ret;
    //}

}
