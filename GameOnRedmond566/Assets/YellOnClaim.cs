using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellOnClaim : MonoBehaviour
{
    public bool ShowNUX = true;
    public float TimeBeforeNextScan;
    const int TIME_DELAY_FOR_QUEST = 6;
    public GameObject ScanScreen;
    public GameObject ScanCardSuccess;
    public GameObject ScanCardTooRecent;
    public GameObject ScanCardNUX;
    public GameObject ScanCardFail;
    public GameObject ScanCardWrong;
    public GameObject ScanCardNewQuests;
    //for debug text
    public Text MyText;
    public Text myLastToyText;
    //for debug
    public GameObject debugstuff;
    public GameObject debugtogglebutton;

    public int currentBackgroundIndex;

    public GameObject ResourceSpawner;

    public ParticleSystem redfireworks;
    public ParticleSystem greenfireworks;
    public ParticleSystem bluefireworks;

    public ParticleSystem leftScanParticles;
    public ParticleSystem rightscanParticles;
    public Text ScanText;


    public enum State { ScanIn, QuestProgress, ActualGather, QuestComplete };

    //public State currentState = State.ScanIn;

    //all background screens
    public List<GameObject> Backgrounds;
    public enum Location { SANC, SWAMP, MOUNTAIN, FOREST, LAKE, WIND, FARM };

    public enum Regions { CLEVELAND, RTCWEST, RTCEAST};
    public Regions currentRegion;

    public Location questLocation = Location.SANC;//default to sanc for now
    //pages
    public GameObject ScanPage_0;// defualt scan page
    public GameObject GatherPage_1;// page where players gather resources// goes to 2
    public GameObject ComebackPage_1;// page where players are told to comeback//goes to 0
    public GameObject ResourcesGathered_2;//show all resources gathered// goes to 3
    public GameObject QuestProgress_3;//show quest progress// goes to 5
    public GameObject QuestComplete_3;//show quest completed //goes to 4 or 5
    public GameObject DragonHatch_4;//when the dragon is hatched// goes to 5
    public GameObject DragonAdult_4;//when the dragon is an adult// goes to 5
    public GameObject GoToNextStation_5;// push players to go to a new station// goes to 0

    //current scanned toy
    public BitToys.Toy MyCurrentToy = null;
    public BitToys.Toy Debug_LastToyScanned = null;// last toy scanned, for erasing cards

    public GameObject dragon;

    public Text gathercompletetext;

    public ParticleSystem MegaScanInParticles;
    public Dictionary<string, float> playerScanInTimes;

    public string CurrentPlayerID;

    public Text QuestCompleteText;
    public Text QuestProgressText;
    public Text BluetoothLowBatteryText;

    public Location currentLocation;
    public GameObject mountain;
    public bool debugmode;
    public GameObject QuestGiver;

    public Vector3 origDebugPosition;

    public Text ComebackLater;

    public bool ready2scan;

    public bool particleMode;

    public bool isPlayerCurrentlyOnASpecialQuest = false;

    //maybe move this stuff somewhere else
    public Dictionary<string, int> resourceCountForText;

    public static string ErrorLog = "";

    public Dictionary<string, string> Resource2Location;
    public Dictionary<string, string> Location2Resource;
    public Dictionary<Location, string> EnumLocation2Resource;

    public void SetPageinfo()// updates the pages with player information
    {
        SetStationPages tempSetStationPages = this.gameObject.GetComponent<SetStationPages>();

        foreach (GameObject stampCard in tempSetStationPages.myResourceCards)
        {

        }
        tempSetStationPages.SetAllStationResoruces(tempSetStationPages.GetStationResource());
        foreach (GameObject stationResource in tempSetStationPages.myStationResources)
        {

        }
        foreach (GameObject dragonObject in tempSetStationPages.myDragons)
        {

        }
        foreach (GameObject progressBar in tempSetStationPages.myProgressBars)
        {

        }
    }

    public void ClearCard(BitToys.Toy cardtoclear)
    {
        cardtoclear.customData.ClearAll_Local();
        cardtoclear.customData.SendAsync();
    }

    public void ClearCardButton()
    {
        this.ClearCard(this.Debug_LastToyScanned);
    }

    // Use this for initialization
    void Start()
    {
        Resource2Location = new Dictionary<string, string>();
        Location2Resource = new Dictionary<string, string>();
        EnumLocation2Resource = new Dictionary<Location, string>();

        Resource2Location.Add("PINECONE", "FARM");
        Resource2Location.Add("REED", "WIND");
        Resource2Location.Add("ICE", "LAKE");
        Resource2Location.Add("WOOD", "FOREST");
        Resource2Location.Add("MUSH", "SWAMP");
        Resource2Location.Add("ROCK", "MOUNTAIN");

        Location2Resource.Add("FARM", "PINECONE");
        Location2Resource.Add("WIND", "REED");
        Location2Resource.Add("LAKE", "ICE");
        Location2Resource.Add("FOREST", "WOOD");
        Location2Resource.Add("SWAMP", "MUSH");
        Location2Resource.Add("MOUNTAIN", "ROCK");

        //this is needed because we can give it an int, cast it to a location, and get the resource
        EnumLocation2Resource.Add(Location.FARM,     "PINECONE");
        EnumLocation2Resource.Add(Location.WIND,     "REED");
        EnumLocation2Resource.Add(Location.LAKE,     "ICE");
        EnumLocation2Resource.Add(Location.FOREST,   "WOOD");
        EnumLocation2Resource.Add(Location.SWAMP,    "MUSH");
        EnumLocation2Resource.Add(Location.MOUNTAIN, "ROCK");

        BluetoothLowBatteryText.gameObject.SetActive(false);
        ready2scan = true;
        particleMode = true;
        leftScanParticles.Play();
        rightscanParticles.Play();
        resourceCountForText = new Dictionary<string, int>();

        debugmode = true;
        //origDebugPosition = debugstuff.transform.position;
        currentLocation = Location.SWAMP;

        playerScanInTimes = new Dictionary<string, float>();
      
  
        string temp = BitToys.inst.ToString();
  

        Debug.Log("wrapper test = " + temp);

        //scan messages
        BitToys.inst.onClaimToy_OK += this.OnClaimToy_Success;// when we scan a card and it works
        BitToys.inst.onClaimToy_Fail += this.OnClaimToy_Fail;
        BitToys.inst.onGetToy_Fail += this.OnGetToy_Fail;

        //bluetooth messages
        BitToys.inst.ble_onDeviceConnected += this.OnDeviceConnect;
        BitToys.inst.ble_onDeviceLost += this.OnDeviceConnect;
        BitToys.inst.ble_onDeviceConnectFailed += this.OnDeviceConnect;

        BitToys.inst.ble_onBatteryLow += this.OnBatteryLow;

        BitToys.inst.onFetchToyList_OK += this.OnFetchOwnedToys;
        BitToys.inst.onFetchToyList_Fail += this.OnFetchAllToysFailed;

        //async messages (update server with our custom data)
        BitToys.inst.onPutCustomData_Fail += OnPutData_Fail;
        BitToys.inst.onPutCustomData_OK += OnPutData_Success;

    }

    public void OnBatteryLow(string _id)
    {
        BluetoothLowBatteryText.gameObject.SetActive(true);
    }

    public void OnPutData_Fail(string _id, BitToys.FailReason reason, string text)
    {
        WriteToErrorLog("Updating customData for id: " + _id + " failed: " + reason + " " + text);
    }
    public void OnPutData_Success(BitToys.Toy _toy)
    {
        WriteToErrorLog("Updating customData succeeded for toy: " + _toy.bitToysId);
    }

    public void WriteToErrorLog(string message)// new function for error logging, the old error logs are kinda a hack
    {
        ErrorLog = ("/n" + " "+(System.DateTime.Now).ToString() + " : " + message) + ErrorLog;
        UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " : " + message);

        MyText.text += ("/n" + " " + (System.DateTime.Now).ToString() + " : " + message);//write to old debug
    }

    public string WriteToErrorLog(string message, bool returnString)// new function for error logging, the old error logs are kinda a hack
    {
        ErrorLog = ("/n" + " " + (System.DateTime.Now).ToString() + " : " + message) + ErrorLog;
        UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " : " + message);

        MyText.text += ("/n" + " " + (System.DateTime.Now).ToString() + " : " + message);//write to old debug

        if (returnString)
            return ("/n" + " " + (System.DateTime.Now).ToString() + " : " + message);
        else
            return null;
    }

    public void DebugToyLastScanned()
    {
        if(this.Debug_LastToyScanned != null)
        {
            this.DebugToyCustomData(this.Debug_LastToyScanned);
        }
    }

    public void DebugToyCustomData(BitToys.Toy debugtoy)
    {
        string debugstring = "toy id = "+debugtoy.bitToysId;
        debugstring  = debugstring +"\n "+debugtoy.customData.AsJSONString();
        this.WriteToErrorLog(debugstring);
    }

    public void CopyErrorLog()// should tie to button for copy buffer?
    {
        GUIUtility.systemCopyBuffer = ErrorLog;
        UniClipboard.SetText(ErrorLog);
    }
    public void Copytext()// should tie to button for copy buffer?
    {
        GUIUtility.systemCopyBuffer = this.MyText.text;
        UniClipboard.SetText(this.MyText.text);
    }

    public void SetNewToyVars(BitToys.Toy theToy, bool val)
    {
       bool needsVars = theToy.customData.GetBool("needsVars", true);

        if (needsVars)
        {
            Debug.Log("NUX!");
            theToy.customData.AddBool("needsVars", false);
            theToy.customData.AddInt("QuestsCompleted", 0);
            theToy.customData.AddInt("CurrentQuest", -1);
            theToy.customData.AddBool("DragonUpgraded", false);//did i do a quest that could upgrade the dragon?
            theToy.customData.AddBool("NewUser", true) ;// does the user need the NUX (New User eXperience)?
            //resources tracked in OnClickHarvest

        }
    }

    public void SetLastToyScanned(BitToys.Toy lastToyScanned)
    {
        this.Debug_LastToyScanned = lastToyScanned;
        this.myLastToyText.text = "Toy ID: " + lastToyScanned.bitToysId;

    }

    public void OnClaimToy_Success(BitToys.Toy theToy, bool val)
    {
        //debug for erasing cards
        SetLastToyScanned(theToy);
  
        if (!ready2scan)
        {
            UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " Tried to scan but the reader wasn't ready...");
        
            return;
        }


        ready2scan = false;
  
        this.MyCurrentToy = theToy;

        CurrentPlayerID = theToy.bitToysId;

        bool validscan = false;

        if (!playerScanInTimes.ContainsKey(CurrentPlayerID))
        {
            playerScanInTimes.Add(CurrentPlayerID, Time.time);
            validscan = true;
      
        }
        else
        {
            validscan = CheckForValidScanTime(CurrentPlayerID);
          
        }

        if (!validscan)
        {
            this.ScanScreen.SetActive(false);
            this.ScanCardTooRecent.SetActive(true); //weve seen this player before, and the time
                                                    //hasnt expired yet, so they need to come back later
                                                    //no need for return here, this goes to the end of the function just the same
            UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " Tried to scan but it was too soon...");
            StartCoroutine(EnableReady2Scan());
          
        }
        else// if valid scan
        {


            if ( this.ShowNUX && this.MyCurrentToy.customData.GetBool("NewUser", true))
            {
                this.MyCurrentToy.customData.AddBool("NewUser", false);// flag as an old user
                this.ScanCardNUX.SetActive(true);

                SetNewQuestCard(0);
            }
            else
            {


                QuestProgress myQuestProgress = this.gameObject.GetComponent<QuestProgress>();

                // which state are we in?

                if (myQuestProgress.CompletedAllQuests())
                {
                    this.ScanCardNewQuests.SetActive(true);
                    //update cards ui?
                    //update dragon ui?
                    //get new quests?
                    //get next dragon?
                    //update new quest ui?
                    //update new dragon?
                }
                else if (myQuestProgress.StationIsForQuest())
                {
                    this.ScanCardSuccess.SetActive(true);
                    //update cards ui?
                    //update dragon ui?
                }
                else
                {
                    this.ScanCardWrong.SetActive(true);
                    //update cards ui?
                    //update dragon ui?
                }

                //TODO sanctuary too early

                //TODO sanctuary
            }
            

            leftScanParticles.gameObject.SetActive(false);
            rightscanParticles.gameObject.SetActive(false);
            // if(MegaScanInParticles.gameObject.activeSelf)
            MegaScanInParticles.Play();


            //at this point it will always contain the key, this is just to prevent nulls
            if (playerScanInTimes.ContainsKey(CurrentPlayerID))
            {
                playerScanInTimes[CurrentPlayerID] = Time.time;
            }

            string tempDebug = "";
            tempDebug += ""; //clear text

            tempDebug += "\n Toy id = " + theToy.bitToysId;
            tempDebug += "\n Owner id = " + theToy.ownerId;
            tempDebug += "\n Style id = " + theToy.styleId;
            tempDebug += "\n SKU id = " + theToy.skuId;
            tempDebug += "\n customData has: " + theToy.customData.ToString();
            tempDebug += "\n " + System.DateTime.Now;
            tempDebug += "\n ******************************";
            MyText.text = tempDebug + MyText.text;// debug at the top!

            UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + tempDebug + MyText.text + " " + System.DateTime.Now + " ");

            foreach (KeyValuePair<string, float> entry in playerScanInTimes)
            {
                MyText.text += "\n Player ID: " + entry.Key.ToString() + " val is " + entry.Value.ToString();
                // do something with entry.Value or entry.Key

                

               
            }//end for each
             //reset the location timer back to 0 (dont get a new location)
            GetComponent<PickLocationTimer>().ResetTimer();
            //handle screen activations
            this.ScanScreen.SetActive(false);// deactivate scan screen
        }//end of else valid scan
    } // end of function

    /// <summary>
    /// this function will do n choose k
    /// </summary>
    /// the number [exclusive] that is the max of your range
    /// <param name="maxrange"></param>
    /// the number of intns you need
    /// <param name="numberyouneed"></param>
    /// <returns></returns>
    List<int> GetRandomUniqueNumbers(int maxrange, int numberyouneed)
    {
        if (numberyouneed >= maxrange)
            throw new System.Exception();

        List<int> list2return = new List<int>();
        List<int> originallist = new List<int>();
        for(int i = 1; i < maxrange; ++i)
        {
           originallist.Add(i);
        }

        for(int i = 0; i < numberyouneed; ++i)
        {
            bool removed = false;
            while(!removed)
            {
                int removethis = Random.Range(1, maxrange);
                if (originallist.Contains(removethis))
                {
                    originallist.Remove(removethis);
                    list2return.Add(removethis);
                    removed = true;
                }
                else
                {
                    continue;
                }
            }     
        }
      
        

        return list2return;
    }
    

    void SetNewQuestCard(int levelofdragon)
    {

        if(currentRegion == Regions.CLEVELAND)
        {
            if(levelofdragon == 0)
            {
                //choose 1 random of 6
                int randomone = Random.Range(1, 7);

                string resource = EnumLocation2Resource[(Location)randomone];
                
                int wood = MyCurrentToy.customData.GetInt(resource, -999);

                if (wood == -999)
                {
                    MyCurrentToy.customData.AddInt(resource, 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt(resource, 0);
                }

                int dragonlevel = MyCurrentToy.customData.GetInt("DragonLevel", -999);

                if (dragonlevel == -999)
                {
                    MyCurrentToy.customData.AddInt("DragonLevel", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("DragonLevel", 0);
                }

            }
            else if(levelofdragon == 1)
            {
                //choose 2 random of 6
                List<int> randomlist = GetRandomUniqueNumbers(7, 2);

                foreach(int i in randomlist)
                {
                    string resource = EnumLocation2Resource[(Location)i];

                    int resint = MyCurrentToy.customData.GetInt(resource, -999);

                    if (resint == -999)
                    {
                        MyCurrentToy.customData.AddInt(resource, 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt(resource, 0);
                    }
                }

            }
            else if(levelofdragon == 2)
            {
                //choose 4 random of 6
                List<int> randomlist = GetRandomUniqueNumbers(7, 4);

                foreach (int i in randomlist)
                {
                    string resource = EnumLocation2Resource[(Location)i];

                    int resint = MyCurrentToy.customData.GetInt(resource, -999);

                    if (resint == -999)
                    {
                        MyCurrentToy.customData.AddInt(resource, 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt(resource, 0);
                    }
                }
            }
            else if(levelofdragon == 3)
            {
                //go to sanctuary
                int scale = MyCurrentToy.customData.GetInt("SCALE", -999);

                if (scale == -999)
                {
                    MyCurrentToy.customData.AddInt("SCALE", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("SCALE", 0);
                }
            }
        }
        else if(currentRegion == Regions.RTCWEST)
        {
            if (levelofdragon == 0)
            {
                //choose 1 random of 6
                int randomone = Random.Range(1, 7);

                string resource = EnumLocation2Resource[(Location)randomone];

                int wood = MyCurrentToy.customData.GetInt(resource, -999);

                if (wood == -999)
                {
                    MyCurrentToy.customData.AddInt(resource, 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt(resource, 0);
                }

                int dragonlevel = MyCurrentToy.customData.GetInt("DragonLevel", -999);

                if (dragonlevel == -999)
                {
                    MyCurrentToy.customData.AddInt("DragonLevel", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("DragonLevel", 0);
                }
            }
            else if (levelofdragon == 1)
            {
                //choose 2 random of 6
                List<int> randomlist = GetRandomUniqueNumbers(7, 2);

                foreach (int i in randomlist)
                {
                    string resource = EnumLocation2Resource[(Location)i];

                    int resint = MyCurrentToy.customData.GetInt(resource, -999);

                    if (resint == -999)
                    {
                        MyCurrentToy.customData.AddInt(resource, 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt(resource, 0);
                    }
                }

            }
            else if (levelofdragon == 2)
            {
                //choose 4 random of 6
                List<int> randomlist = GetRandomUniqueNumbers(7, 4);

                foreach (int i in randomlist)
                {
                    string resource = EnumLocation2Resource[(Location)i];

                    int resint = MyCurrentToy.customData.GetInt(resource, -999);

                    if (resint == -999)
                    {
                        MyCurrentToy.customData.AddInt(resource, 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt(resource, 0);
                    }
                }
            }
            else if (levelofdragon == 3)
            {
                //go to sanctuary
                int quest = 0;

                int scale = MyCurrentToy.customData.GetInt("SCALE", -999);

                if (scale == -999)
                {
                    MyCurrentToy.customData.AddInt("SCALE", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("SCALE", 0);
                }
                
            }
        }
        else if(currentRegion == Regions.RTCEAST)
        {
            if (levelofdragon == 0)
            {
                //choose 1 random of 2 (rtc east)
                if(currentLocation == Location.MOUNTAIN)
                {
                    int mush = MyCurrentToy.customData.GetInt("MUSH", -999);

                    if (mush == -999)
                    {
                        MyCurrentToy.customData.AddInt("MUSH", 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt("MUSH", 0);
                    }
                }
                else if(currentLocation == Location.SWAMP)
                {
                    int mush = MyCurrentToy.customData.GetInt("ROCK", -999);

                    if (mush == -999)
                    {
                        MyCurrentToy.customData.AddInt("ROCK", 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt("ROCK", 0);
                    }
                }


                int dragonlevel = MyCurrentToy.customData.GetInt("DragonLevel", -999);

                if (dragonlevel == -999)
                {
                    MyCurrentToy.customData.AddInt("DragonLevel", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("DragonLevel", 0);
                }

            }
            else if (levelofdragon == 1)
            {
                //choose 2 of 4 from RTC WEST
                List<int> randomlist = GetRandomUniqueNumbers(5, 2);

                foreach (int i in randomlist)
                {
                    string resource = EnumLocation2Resource[(Location)i];

                    int resint = MyCurrentToy.customData.GetInt(resource, -999);

                    if (resint == -999)
                    {
                        MyCurrentToy.customData.AddInt(resource, 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt(resource, 0);
                    }
                }

            }
            else if (levelofdragon == 2)
            {
                //choose 4 of 4 (rtc west)
                int ice = MyCurrentToy.customData.GetInt("ICE", -999);

                if (ice == -999)
                {
                    MyCurrentToy.customData.AddInt("ICE", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("ICE", 0);
                }

                int mush = MyCurrentToy.customData.GetInt("MUSH", -999);

                if (mush == -999)
                {
                    MyCurrentToy.customData.AddInt("MUSH", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("MUSH", 0);
                }

                int rock = MyCurrentToy.customData.GetInt("ROCK", -999);

                if (rock == -999)
                {
                    MyCurrentToy.customData.AddInt("ROCK", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("ROCK", 0);
                }
                int wood = MyCurrentToy.customData.GetInt("WOOD", -999);

                if (wood == -999)
                {
                    MyCurrentToy.customData.AddInt("WOOD", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("WOOD", 0);
                }


            }
            else if (levelofdragon == 3)
            {
                //go to sanctuary
                int scale = MyCurrentToy.customData.GetInt("SCALE", -999);

                if (scale == -999)
                {
                    MyCurrentToy.customData.AddInt("SCALE", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("SCALE", 0);
                }
            }
        }


    }

    IEnumerator EnableReady2Scan()
    {
        yield return new WaitForSeconds(3.0f);
        ready2scan = true;
    }

    public void ChangeRegion(int index)
    {
        currentRegion = (Regions)index;
    }

    public void ChangeBackground(int index)
    {
        this.TurnOffAllBackgrounds();// set all backgrounds to inactive

        Backgrounds[index].SetActive(true);

        this.currentLocation = (Location)index;

        Debug.Log("Background = "+this.currentLocation.ToString());
        
    }
    public void TurnOffAllBackgrounds()
    {
        foreach (GameObject bg in this.Backgrounds)
        {
            bg.SetActive(false);
        }
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
                //ComebackLater.gameObject.SetActive(true);
                //StartCoroutine(LateTurnOffComeBackLaterText());
                return false;
            }
            else
            {
                return true; //its a valid scan
            }
        }

        return false; //didnt find the key
    }


    public void ToggleDebugStuff()
    {

        if (debugmode)
        {
            //debugstuff.transform.position = new Vector3(origDebugPosition.x + 1000, 11600, origDebugPosition.z);
            debugstuff.SetActive(false);
            debugtogglebutton.GetComponent<Image>().color = Color.clear;
    }
        else
        {
            //debugstuff.transform.position = origDebugPosition;
            debugstuff.SetActive(true);
            debugtogglebutton.GetComponent<Image>().color = Color.white;

        }


        debugmode = !debugmode;//toggle
    }
    //for now this function will only work at scan page
    public void ToggleParticles()
    {
        //all particles:
        //left and right scan in particles
        //mega scan particles
        //victory fireworks
        //
        particleMode = !particleMode;

        if(!particleMode)
        {

                leftScanParticles.gameObject.SetActive(false);
            rightscanParticles.gameObject.SetActive(false);
            MegaScanInParticles.gameObject.SetActive(false);
        }
        else
        {
            if(!ready2scan)
            {
                MegaScanInParticles.gameObject.SetActive(true);
            }
            else
            {
                MegaScanInParticles.gameObject.SetActive(true);
                leftScanParticles.gameObject.SetActive(true);
                rightscanParticles.gameObject.SetActive(true);
            }
        }

    }

    public void FakeScanNUX()
    { 

        this.CurrentPlayerID = "player1NUXtest";
        this.MyCurrentToy = new BitToys.Toy();
        this.MyCurrentToy.customData.ClearAll_Local();// clear all data?
        this.MyCurrentToy.bitToysId = this.CurrentPlayerID;
        //this.MyCurrentToy.customData.SendAsync();

        this.OnClaimToy_Success(this.MyCurrentToy, true);// fake the scan
    }

    public void FakeFailScan()
    {
        this.OnClaimToy_Fail(BitToys.FailReason.NETWORK_ERROR, "test fail");
    }

    public void FakeScan()
    {
        
        //this.MyCurrentToy.customData.ClearAll_Local();// clear all data?
        this.MyCurrentToy.bitToysId = this.CurrentPlayerID;

        this.OnClaimToy_Success(this.MyCurrentToy, true);// fake the scan
    }
    public void ResetFakeScan()
    {
        this.CurrentPlayerID = "player1test";
        this.MyCurrentToy = new BitToys.Toy();
        
        this.MyCurrentToy.bitToysId = this.CurrentPlayerID;
        this.MyCurrentToy.customData.ClearAll_Local();// clear all data?
        this.MyCurrentToy.customData.SendAsync();
    }

    public void SetLEDs()
    {
        // Method: SetLED_Connected(Red, Green, Blue, Red Pulse, Green Pulse, Blue Pulse).
                // When device is connected and idle, the light is solid bright blue.
        BitToys.inst.ble_SetLED_Connected(0, 0, 255, 0, 0, 0);
        // When a tag is detected and being read, change the light to a bright pulsing green
        BitToys.inst.ble_SetLED_nfcTagDetected(0, 255, 0, 0, 15, 0);
        // If a tag fails to read properly, change it to bright pulsing Red.
        BitToys.inst.ble_SetLED_nfcTagError(255, 0, 0, 15, 0, 0);
       // If a tag is read correctly, change the light to bright solid green.

        BitToys.inst.ble_SetLED_nfcTagOK(0, 255, 0, 0, 0, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            FakeScanNUX();
            MyCurrentToy.customData.AddInt("Amethyst", 69);
            MyCurrentToy.customData.AddInt("Apple", 420);
            TestJson();
            
        }
           
    }

    public void OnClaimToy_Fail(BitToys.FailReason reason, string mytext)
    {
        string debugstring = "";
        debugstring += "OnClaimToy_Fail" + "\n"; //clear text
        debugstring += reason.ToString() + "\n";
        debugstring += mytext + "\n";
        debugstring += "\n ******************************";

        this.MyText.text += debugstring;
        UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " " + debugstring);

        if (!ready2scan)
            return;

        ready2scan = false;
        StartCoroutine(EnableReady2Scan());

        this.ScanCardFail.SetActive(true);
        this.leftScanParticles.gameObject.SetActive(false);
        this.rightscanParticles.gameObject.SetActive(false);
    }

    public void TestJson()
    {
        //send to doug's server
        GetComponent<JSONPost>().POST(MyCurrentToy.customData.AsJSONString());
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

        UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " Connected!");
    }

    public void OnFetchOwnedToys(List<BitToys.Toy> myToys, bool mybool)
    {
        foreach (BitToys.Toy toy in myToys)
        {
            
            MyText.text += "\n Toy id = " + toy.bitToysId;
            MyText.text += "\n Owner id = " + toy.ownerId;
            MyText.text += "\n Style id = " + toy.styleId;
            MyText.text += "\n SKU id = " + toy.skuId;
            MyText.text += "\n ***********************";
        }
    }

    public void OnFetchAllToysFailed(BitToys.FailReason reason, string mytext)
    {
        this.MyText.text += "OnFetchAllToysFailed" + "\n"; //clear text
        this.MyText.text += reason.ToString() + "\n";
        this.MyText.text += mytext + "\n";
        this.MyText.text += "\n **********************";
    }

    public void TextReset1()
    {
        this.MyText.text = "Reset : Auth key = "+ BitToys.inst.authenticationKey;
    }
    
}
