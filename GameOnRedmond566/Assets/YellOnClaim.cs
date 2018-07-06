using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class YellOnClaim : MonoBehaviour
{
    public float TimeBeforeNextScan;
    const int TIME_DELAY_FOR_QUEST = 6;
    //lets put everything in One big File, one big scene! THis is the way of the programming gods
  public Text MyText;

    public GameObject debugstuff;

    public Dropdown MyDropdown;
    public int currentBackgroundIndex;

    public GameObject ResourceSpawner;

    public ParticleSystem redfireworks;
    public ParticleSystem greenfireworks;
    public ParticleSystem bluefireworks;

    public ParticleSystem leftScanParticles;
    public ParticleSystem rightscanParticles;
    public Text ScanText;

    public bool somethingWasScanned = false;

    public Button QuestButton;
    public Button GatherButton;

    public enum State { ScanIn, QuestProgress, ActualGather, QuestComplete };

    public State currentState = State.ScanIn;

    public enum Location { SWAMP, MOUNTAIN, FOREST };

    public GameObject shrek;
    public GameObject sanc;
    public GameObject forest;

    public GameObject dragon;

    public Text gathercompletetext;

    public ParticleSystem MegaScanInParticles;
    public Dictionary<string,int> playerIDsForQuests; //string is playerid, int is quest they are on (0-1)
    public Dictionary<string, Dictionary<string, int>> playerInventory; //inventory for players, items referred to as strings then count is int
    public Dictionary<string, bool[]> playerIDQuestsCompleteOrNot; //an array of bools, one for each location, if the player is done with that quest or not
    public Dictionary<string, float> playerScanInTimes;

    public string CurrentPlayerID;

    public BitToys.Toy CurrentToy;

    public Text QuestCompleteText;
    public Text QuestProgressText;
    public GameObject EstuaryQuestProgress;
    public GameObject SanctuaryQuestProgress;
    public GameObject MountainQuestProgress;
    public GameObject ForestQuestProgress;

    public Location currentLocation;
    public GameObject mountain;
    public bool debugmode;
    public GameObject QuestGiver;

    public Vector3 origDebugPosition;

    public int[] CurrentPlayerResources;

    public Text Fuckofftext; //this is displayed if the player tries to scan in too soon

    //idea for structure of custom data
    //inventory - array of integers, indexed by enum

        //TODO put this in a separate file
        enum Resources
    {
        AMETHYST,
        APPLE,
        BERRY,
        GRASS,
        CEDAR,
        CRYSTAL,
        FEATHER,
        ICE,
        MUSHROOM,
        ROCKS,
        SCALE,
        TOTALNUMBER

    };


    //these are debug things for testing
    public bool unquestedplayerscan = false;
    public bool questedplayerscan = false;

    public void ChangeBackground(int index)
    {
        switch(index)
        {
            case 0:
                DisplayShrekBacground();
                currentLocation = Location.SWAMP;
                break;

            case 1:
                DisplaySancBackground();
                currentLocation = Location.MOUNTAIN;
                break;

            case 2:
                DisplayForestBackground();
                currentLocation = Location.FOREST;
                break;
        }
    }

    public void DisplayForestBackground()
    {
        shrek.SetActive(false);
        sanc.SetActive(false);
        forest.SetActive(true);
    }

    public void DisplayShrekBacground()
    {
        shrek.SetActive(true);
        sanc.SetActive(false);
        forest.SetActive(false);
    }

    public void DisplaySancBackground()
    {
        shrek.SetActive(false);
        sanc.SetActive(true);
        forest.SetActive(false);
    }

    public void DisplayMountainBackground()
    {
        shrek.SetActive(false);
        sanc.SetActive(false);
        forest.SetActive(false);
        mountain.SetActive(true);
    }


    void UnHideScanStuff(bool visible)
    {
        leftScanParticles.gameObject.SetActive(visible);
        rightscanParticles.gameObject.SetActive(visible);
        ScanText.gameObject.SetActive(visible);
    }

    void UnHideQuestAndGatherButts(bool visible)
    {
        QuestButton.gameObject.SetActive(visible);
        GatherButton.gameObject.SetActive(visible);
    }

    void ChangeFromScanToQuest()
    {

    }

    public void ChangeFromGatherToScan()
    {
        UnHideQuestAndGatherButts(false);
        UnHideScanStuff(true);
        currentState = State.ScanIn;
        
    }

    void ChangeFromScanToGather()
    {
        UnHideQuestAndGatherButts(false);
        UnHideScanStuff(false);
        currentState = State.ActualGather;

        UnHideQuestAndGatherButts(false);
        //SPAWN BASED ON LOCATION

        switch (currentLocation)
        {
            case Location.FOREST:
                ResourceSpawner.GetComponent<SpawnResources>().SpawnForForest();
                break;
            case Location.MOUNTAIN:
                ResourceSpawner.GetComponent<SpawnResources>().SpawnForMountain();
                break;
            case Location.SWAMP:
                ResourceSpawner.GetComponent<SpawnResources>().SpawnForSwamp();
                break;

                //TODO: add mountain shit here
                //    break;
        }


        currentState = State.ActualGather;

    }

    public void ChangeFromQuestGatherToQuest()
    {
        UnHideQuestAndGatherButts(false);
     //   currentState = State.ActualQuest;
    }

    public void ChangeFromGatherToQuestGather()
    {
        // UnHideQuestAndGatherButts(true);
        //update player quest- if done, do celebration, if only halfway done, tell it 
        if(playerIDQuestsCompleteOrNot[CurrentPlayerID][(int)currentLocation] == false)
        {
            switch(currentLocation)
            {
                case Location.SWAMP:
                    EstuaryQuestProgress.SetActive(true);
                    break;

                case Location.MOUNTAIN:
                    MountainQuestProgress.SetActive(true);
                    break;

                case Location.FOREST:
                    ForestQuestProgress.SetActive(true);
                    break;
            }
            QuestGiver.SetActive(true);
        }
        else
        {
            QuestCompleteText.gameObject.SetActive(true);
            dragon.SetActive(true);
        }
        //go back to scan
        StartCoroutine(LateSetToScan());
    }

    //TODO: probably delete this and related functions
    public void ChangeFromQuestToQuestGather()
    {
        UnHideQuestAndGatherButts(true);
      //  currentState = State.QuestOrGather;
    }

    //after gather, go to quest update/complete, then scan
    public void ChangeFromQuestToScan()
    {
        currentState = State.ScanIn;

    }

    public void ChangeFromQuestGatherToGather()
    {

    }

    void MoveToQuestOrResourceScreen()
    {
        //hide scan particles and text
        UnHideScanStuff(false);
        //unhide buttons for quest and gather
        UnHideQuestAndGatherButts(true);
 //       currentState = State.QuestOrGather;
    }

    void HideQuestAndGatherShit(bool visible)
    {
        QuestButton.gameObject.SetActive(visible);
        GatherButton.gameObject.SetActive(visible);
    }

    //TODO: wtf is this function?
    void HideActualGatherShit(bool shrekIsVisible)
    {
        shrek.SetActive(shrekIsVisible);
        sanc.SetActive(!shrekIsVisible);
    }

    void MoveToActualGather()
    {
        UnHideScanStuff(false);
        HideQuestAndGatherShit(false);
        HideActualGatherShit(true);
        
        currentState = State.ActualGather;
    }
  // Use this for initialization
  void Start ()
  {
        QuestCompleteText = GameObject.Find("QuestCompleteText").GetComponent<Text>();
        QuestCompleteText.gameObject.SetActive(false);

        Fuckofftext = GameObject.Find("Fuckofftext").GetComponent<Text>();
        Fuckofftext.gameObject.SetActive(false);

        EstuaryQuestProgress = GameObject.Find("EstuaryQuestText");
        MountainQuestProgress = GameObject.Find("MountainQuestText");
        ForestQuestProgress = GameObject.Find("ForestQuestText");

        QuestProgressText = GameObject.Find("QuestProgressText").GetComponent<Text>();
        QuestProgressText.gameObject.SetActive(false);
        QuestGiver = GameObject.Find("questgiver");
        QuestGiver.SetActive(false);
        debugstuff = GameObject.Find("debugstuff");
        debugmode = true;
        origDebugPosition = debugstuff.transform.position;
        currentLocation = Location.SWAMP;
        playerIDsForQuests = new Dictionary<string, int>();
        playerIDsForQuests.Add("gay", 0);
        playerScanInTimes = new Dictionary<string, float>();
        playerIDQuestsCompleteOrNot = new Dictionary<string, bool[]>();
        playerInventory = new Dictionary<string, Dictionary<string, int>>();

        dragon = GameObject.Find("dragon");
        dragon.SetActive(false);
        gathercompletetext = GameObject.Find("GatherResourcesCompleteText").GetComponent<Text>();
        gathercompletetext.gameObject.SetActive(false);
        MegaScanInParticles = GameObject.Find("MegaScanInParticles").GetComponent<ParticleSystem>();
        //this is like the worst possible way of doing this, but ill fix it later
        ResourceSpawner = GameObject.Find("ResourceSpawner");
        QuestButton = GameObject.Find("GoQuestButton").GetComponent<Button>();
        GatherButton = GameObject.Find("GoGatherResourcesButton").GetComponent<Button>();
            shrek = GameObject.Find("shrek");
        sanc = GameObject.Find("sanc");
        forest = GameObject.Find("forest");
    this.MyText = GetComponent<Text>();
        MyDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        currentBackgroundIndex = MyDropdown.value;

        ChangeBackground(0);

        redfireworks = GameObject.Find("redfireworks").GetComponent<ParticleSystem>();
        greenfireworks = GameObject.Find("greenfireworks").GetComponent<ParticleSystem>();
        bluefireworks = GameObject.Find("bluefireworks").GetComponent<ParticleSystem>();

        ScanText = GameObject.Find("ScanText").GetComponent<Text>();
        leftScanParticles = GameObject.Find("scantext_particles_left").GetComponent<ParticleSystem>();
        rightscanParticles = GameObject.Find("scantext_particles_right").GetComponent<ParticleSystem>();

        UnHideQuestAndGatherButts(false);

        string temp = BitToys.inst.ToString();
    //BitToysWrapper.inst.registerOnClaimToy(this.OnClaim);
        // BitToys.inst.nfc_onSawTag += OnSawTag;
        // BitToys.inst.onClaimToy_OK += claim_ok;
        // BitToys.inst.onClaimToy_Fail += claim_failed;

        Debug.Log("wrapper test = " + temp);

        BitToys.inst.onClaimToy_OK += this.OnClaimToy_Success;// when we scan a card and it works
        BitToys.inst.onClaimToy_Fail += this.OnClaimToy_Fail;
        BitToys.inst.onGetToy_Fail += this.OnGetToy_Fail;

        BitToys.inst.ble_onDeviceConnected += this.OnDeviceConnect;
        BitToys.inst.ble_onDeviceLost += this.OnDeviceConnect;
        BitToys.inst.ble_onDeviceConnectFailed += this.OnDeviceConnect;

        BitToys.inst.onFetchToyList_OK += this.OnFetchOwnedToys;
        BitToys.inst.onFetchToyList_Fail += this.OnFetchAllToysFailed;

        BitToys.inst.onPutCustomData_Fail += this.OnPutCustomData_Fail;
        BitToys.inst.onPutCustomData_OK += this.OnPutCustomData_OK;

    }

    public bool CheckForValidScanTime(string id)
    {
        if (playerScanInTimes.ContainsKey(CurrentPlayerID))
        {
            //time must be at least x secs after last scan
            float validtime = playerScanInTimes[CurrentPlayerID] + TimeBeforeNextScan;

            MyText.text += "\n current time is " + Time.time.ToString();
            MyText.text += "\n Last scan in time for this id was  = " + playerScanInTimes[CurrentPlayerID].ToString();
            MyText.text += "\n ********************************";

            if (validtime > Time.time)
            {
                Fuckofftext.gameObject.SetActive(true);
                StartCoroutine(LateTurnOffFuckOffText());
                return false;
            }
        }

        return true;
    }

    //method for controlling player data:
    //when scanning in, download data
    //when quest complete, save data

    public void OnClaimToy_Success(BitToys.Toy theToy, bool val)
    {
        MyText.text += "\n current state is " + currentState.ToString();
        //only one player, one scan at a time
        if (currentState != State.ScanIn)
            return;

        //get the data



       // theToy.customData.SendAsync();

        CurrentPlayerID = theToy.bitToysId;
        CurrentToy = theToy;

        bool validscan = CheckForValidScanTime(CurrentPlayerID);
        if (!validscan)
            return;

        //populate the resources (this, along with the quests, is our custom data)
       // CurrentPlayerResources =

        //now populate quests

        //watchdog timer

        //Two global data entries under "intMultiple". toyExample​.​customData​.​AddInt_Global​(​"intMultiple"​,​ ​2​);
        //toyExample​.​customData​.​AddInt_Global​(​"intMultiple"​,​ ​3​);


        MegaScanInParticles.Play();
      
        if (playerIDsForQuests.ContainsKey(CurrentPlayerID))
        {
            playerIDsForQuests[CurrentPlayerID]++;
            playerScanInTimes[CurrentPlayerID] = Time.time;
        }
        else //add new player
        {
            playerIDsForQuests.Add(CurrentPlayerID, 0);
            playerScanInTimes.Add(CurrentPlayerID, Time.time);
            bool[] quests = new bool[3];
            for(int i = 0; i < quests.Length; ++i)
            {
                quests[i] = false;
            }
            playerIDQuestsCompleteOrNot.Add(theToy.bitToysId, quests);
        }
        

        MyText.text += ""; //clear text

        MyText.text += "\n Toy id = " + theToy.bitToysId;
        MyText.text += "\n Owner id = " + theToy.ownerId;
        MyText.text += "\n Style id = " + theToy.styleId;
        MyText.text += "\n SKU id = " + theToy.skuId;
        MyText.text += "\n ******************************";
        MyText.text += "\n Dictionary has: ";

        foreach (KeyValuePair<string, int> entry in playerIDsForQuests)
        {
            MyText.text += "\n Player ID: " + entry.Key.ToString() + " val is " + entry.Value.ToString();
            // do something with entry.Value or entry.Key
        }

      //  somethingWasScanned = true;
        //  MoveToQuestOrResourceScreen();
        // MoveToActualGather();
        ChangeFromScanToGather();
        //if(theToy.styleId == "gameon_red")
        //{
        //    redfireworks.Stop();
        //    redfireworks.Play();
        //}

        //if(theToy.styleId == "gameon_green")
        //{
        //    greenfireworks.Stop();
        //    greenfireworks.Play();
        //}

        //if((theToy.styleId == "gameon_gray") || (theToy.styleId == "gameon_blue"))
        //{
        //    bluefireworks.Stop();
        //    bluefireworks.Play();
        //}

    }

    public void ShowQuestProgress()
    {
        //save data
        CurrentToy.customData.SendAsync();

        if(playerIDsForQuests.ContainsKey(CurrentPlayerID) && playerIDsForQuests[CurrentPlayerID] == 0)
        {

            ShowContinueQuest();
        }
        else
        {
            ShowQuestComplete();
        }
        StartCoroutine(LateSetToScan());
    }

    public void ClearServer()
    {
        CurrentToy.customData.ClearAll_Local();
    }

    public void ShowQuestPerLocation()
    {
        if (playerIDQuestsCompleteOrNot[CurrentPlayerID][(int)currentLocation] == false)
        {
            playerIDQuestsCompleteOrNot[CurrentPlayerID][(int)currentLocation] = true;
            ShowContinueQuest();
        }
        else
        {
            ShowQuestComplete();
        }
        StartCoroutine(LateSetToScan());
    }

    public void ShowContinueQuest()
    {
        QuestGiver.SetActive(true);
        QuestProgressText.gameObject.SetActive(true);
    }

    public void ShowQuestComplete()
    {
        dragon.SetActive(true);
        QuestCompleteText.gameObject.SetActive(true);

    }

    IEnumerator LateTurnOffFuckOffText()
    {
        yield return new WaitForSeconds(3);
        Fuckofftext.gameObject.SetActive(false);
    }

    IEnumerator LateSetToScan()
    {
        yield return new WaitForSeconds(TIME_DELAY_FOR_QUEST);
        QuestGiver.SetActive(false);
        dragon.SetActive(false);
        EstuaryQuestProgress.SetActive(false);
      //  MountainQuestProgress.SetActive(false);
      //  ForestQuestProgress.SetActive(false);

        QuestCompleteText.gameObject.SetActive(false);
        QuestProgressText.gameObject.SetActive(false);
        currentState = State.ScanIn;
        UnHideScanStuff(true);
    }

    public void ToggleDebugStuff()
    {
        // debugstuff.SetActive(!debugmode);
        //just move it offscreen for now
        if (debugmode)
            debugstuff.transform.position = new Vector3(origDebugPosition.x + 1000, 11600, origDebugPosition.z);
        else
            debugstuff.transform.position = origDebugPosition;

        debugmode = !debugmode;
    }

    private void Update()
    {
        if(debugstuff.activeSelf && (currentBackgroundIndex != MyDropdown.value))
        {
            //inside the change background function current location is set currentlocation
            ChangeBackground(MyDropdown.value);
            currentBackgroundIndex = MyDropdown.value;

        }


        //g will be for a new player with no finished quests
        if(currentState == State.ScanIn && Input.GetKeyDown(KeyCode.Space))
        {
            MegaScanInParticles.Play();
            CurrentPlayerID = "g_THISISATEST";

            if(!unquestedplayerscan)
            {

           bool[] bools = new bool[3];
            bools[0] = false;
            bools[1] = false;
            bools[2] = false;


                playerIDQuestsCompleteOrNot.Add("g_THISISATEST", bools);

                playerScanInTimes.Add("g_THISISATEST", Time.time);

                unquestedplayerscan = true;
            }

  

           if(unquestedplayerscan)
            {
                bool validscan = CheckForValidScanTime(CurrentPlayerID);

                if (!validscan)
                    return;


                playerScanInTimes[CurrentPlayerID] = Time.time;
            }

            ChangeFromScanToGather();
        }

        //b will be for a player who has all level 1 quests done
        //b_level1done
        if (currentState == State.ScanIn && Input.GetKeyDown(KeyCode.M))
        {
            MegaScanInParticles.Play();
            CurrentPlayerID = "b_level1done";

            if (!questedplayerscan)
            {
                
                bool[] bools = new bool[3];
                bools[0] = false;
                bools[1] = false;
                bools[2] = false;


                playerIDQuestsCompleteOrNot.Add("b_level1done", bools);

                playerScanInTimes.Add("b_level1done", Time.time);

                questedplayerscan = true;
            }



            if (questedplayerscan)
            {
                bool validscan = CheckForValidScanTime(CurrentPlayerID);

                if (!validscan)
                    return;


                playerScanInTimes[CurrentPlayerID] = Time.time;
            }

            ChangeFromScanToGather();
        }


//future TODO: add some sort of scanin function so that we can cut down on copy paste code, and abstract out from the claimtoy success

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("current state is " + currentState.ToString());
      //      GoBack();
        }
    }

    //this is a test function
    public void ScanIn(string id)
    {
        //check for basic quest
        if(playerIDsForQuests.ContainsKey(id))
        {

        }

        //check for location quests
        if(playerIDQuestsCompleteOrNot.ContainsKey(id))
        {

        }

        //check for scan time
        if(playerScanInTimes.ContainsKey(id))
        {
            playerScanInTimes[id] = Time.time;
            
        }



    }

    public void GoBack()
    {
        if(currentState == State.ActualGather)
        ChangeFromGatherToScan();
    }

    public void OnClaimToy_Fail(BitToys.FailReason reason, string mytext)
    {
        this.MyText.text += "OnClaimToy_Fail" + "\n"; //clear text
        this.MyText.text += reason.ToString() + "\n";
        this.MyText.text += mytext + "\n";
        this.MyText.text += "\n ******************************";
    }
    public void OnGetToy_Fail(BitToys.FailReason reason, string mytext)
    {
        this.MyText.text += "OnGetToy_Fail" + "\n"; //clear text
        this.MyText.text += reason.ToString() + "\n";
        this.MyText.text += mytext + "\n";
        this.MyText.text += "\n ******************************";
    }
    public void OnDeviceConnect( string mytext)//used for connected/lost/failed (will break into seperate functions if needed)
    {
        this.MyText.text += "OnDeviceConnect" + "\n"; //clear text
        this.MyText.text += mytext + "\n";
        this.MyText.text += "\n ******************************";
    }

    public void OnFetchOwnedToys(List<BitToys.Toy> myToys, bool mybool)
    {
        foreach (BitToys.Toy toy in myToys)
        {
            
            MyText.text += "\n Toy id = " + toy.bitToysId;
            MyText.text += "\n Owner id = " + toy.ownerId;
            MyText.text += "\n Style id = " + toy.styleId;
            MyText.text += "\n SKU id = " + toy.skuId;
            MyText.text += "\n ******************************";
        }
    }

    public void OnFetchAllToysFailed(BitToys.FailReason reason, string mytext)
    {
        this.MyText.text += "OnFetchAllToysFailed" + "\n"; //clear text
        this.MyText.text += reason.ToString() + "\n";
        this.MyText.text += mytext + "\n";
        this.MyText.text += "\n ******************************";
    }

    public void OnPutCustomData_OK(BitToys.Toy toy)
    {
        Debug.Log("Updating customData succeeded for toy: " + toy.bitToysId);
    }

    public void OnPutCustomData_Fail(string _id, BitToys.FailReason reason, string text)
    {
        Debug.Log("Updating cust data for id: " + _id + " failed : " + reason + " " + text);
    }

    //TODO: future improvement, somehow batch up updates and send as a bunch, instead of just immediately sending/receiving data (strains network and if digipen wifi is bad it could be trouble)

    public void TextReset1()
    {
        this.MyText.text = "Reset text1";
    }
    public void TextReset2()
    {
        //this.MyText.text = "Reset text2";
        this.MyText.text = BitToys.inst.authenticationKey;
    }
    /*
    public void OnClaim(ToyWrapper toy, bool TLL)
  {
        mytext.text = ""; //clear text

        mytext.text += "\n Toy id = " + toy.bitToysId;
        mytext.text += "\n Owner id = " + toy.ownerId;
        mytext.text += "\n Style id = " + toy.styleId;
        mytext.text += "\n SKU id = " + toy.skuId;

     if (toy.customData.GetBool_Global("installed", false) == false)
    {
      toy.customData.SetInt_Global("Blue_score", 15);
      toy.customData.SetInt_Global("Red_score", 5);
      toy.customData.SetInt_Global("Yellow_score", 30);

      toy.customData.SetBool_Global("installed", true);
    }

    if(toy.customData.GetBool("registered", false) == false)
    {
      if(toy.styleId == "dtc_trex00")
      {
        toy.customData.SetString("username", "Jacob");
        toy.customData.SetInt("score", 15);
        toy.customData.SetString("team", "Yellow");
      }
      else if(toy.styleId == "dtc_lexov00")
      {
        toy.customData.SetString("username", "Geoffrey");
        toy.customData.SetInt("score", 15);
        toy.customData.SetString("team", "Yellow");
      }
      else if (toy.styleId == "dtc_ttops00")
      {
        toy.customData.SetString("username", "Jeffrey");
        toy.customData.SetInt("score", 15);
        toy.customData.SetString("team", "Blue");
      }
      else if (toy.styleId == "dtc_paras00")
      {
        toy.customData.SetString("username", "Doug");
        toy.customData.SetInt("score", 5);
        toy.customData.SetString("team", "Red");
      }

      toy.customData.SetBool("registered", true);
    }

    string username = toy.customData.GetString("username", "DEFAULT_USER_ERROR");
    int score = toy.customData.GetInt("score", -1);
    string team = toy.customData.GetString("team", "DEFAULT_TEAM_ERROR");
    int team_score = toy.customData.GetInt_Global(team + "_score", -1);

    mytext.text += "Welcome, " + username + ". You're score is: " + score + "\n";
    mytext.text += "You're team, " + team + ", has " + team_score + " points.";
  }*/
}
