using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(Text))]
public class YellOnClaim : MonoBehaviour
{
    public float TimeBeforeNextScan;
    const int TIME_DELAY_FOR_QUEST = 6;
    public GameObject ScanScreen;
    public GameObject ScanCardSuccess;
    public GameObject ScanCardTooRecent;
    //for debug text
    public Text MyText;
    public Text myLastToyText;
    //for debug
    public GameObject debugstuff;
    public GameObject debugtogglebutton;

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

    //public State currentState = State.ScanIn;

    //all background screens
    public List<GameObject> Backgrounds;
    public enum Location { SANC, SWAMP, MOUNTAIN, FOREST, LAKE, WIND, FARM };

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
    public Dictionary<string, int> playerIDsForQuests; //string is playerid, int is quest they are on (0-1)
    public Dictionary<string, Dictionary<string, int>> playerInventory; //inventory for players, items referred to as strings then count is int
    public Dictionary<string, int[]> playerIDQuestsProgress; //an array of bools, one for each location, if the player is done with that quest or not
    public Dictionary<string, float> playerScanInTimes;

    public string CurrentPlayerID;

    public Text QuestCompleteText;
    public Text QuestProgressText;
    public Text BluetoothLowBatteryText;
    public GameObject EstuaryQuestProgress;
    public GameObject SanctuaryQuestProgress;
    public GameObject MountainQuestProgress;
    public GameObject ForestQuestProgress;

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
        BluetoothLowBatteryText.gameObject.SetActive(false);
        ready2scan = true;
        particleMode = true;
        leftScanParticles.Play();
        rightscanParticles.Play();
        resourceCountForText = new Dictionary<string, int>();
        //QuestCompleteText = GameObject.Find("QuestCompleteText").GetComponent<Text>();
        //QuestCompleteText.gameObject.SetActive(false);

        //ComebackLater = GameObject.Find("ComebackLater").GetComponent<Text>();
        // ComebackLater.gameObject.SetActive(false);

        // EstuaryQuestProgress = GameObject.Find("EstuaryQuestText");
        // MountainQuestProgress = GameObject.Find("MountainQuestText");
        // ForestQuestProgress = GameObject.Find("ForestQuestText");

        //QuestProgressText = GameObject.Find("QuestProgressText").GetComponent<Text>();
        //QuestProgressText.gameObject.SetActive(false);
        //QuestGiver = GameObject.Find("questgiver");
        //QuestGiver.SetActive(false);
        //debugstuff = GameObject.Find("debugstuff");
        debugmode = true;
        //origDebugPosition = debugstuff.transform.position;
        currentLocation = Location.SWAMP;
        playerIDsForQuests = new Dictionary<string, int>();
        playerIDsForQuests.Add("PlayerQuestID_Temp", 0);
        playerScanInTimes = new Dictionary<string, float>();
        playerIDQuestsProgress = new Dictionary<string, int[]>();
        //playerInventory = new Dictionary<string, Dictionary<string, int>>();//not used?

        //dragon = GameObject.Find("dragon");
        //dragon.SetActive(false);
        // gathercompletetext = GameObject.Find("GatherResourcesCompleteText").GetComponent<Text>();
        // gathercompletetext.gameObject.SetActive(false);
        // MegaScanInParticles = GameObject.Find("MegaScanInParticles").GetComponent<ParticleSystem>();
        //this is like the worst possible way of doing this, but ill fix it later
        // ResourceSpawner = GameObject.Find("ResourceSpawner");
        //QuestButton = GameObject.Find("GoQuestButton").GetComponent<Button>();
        // GatherButton = GameObject.Find("GoGatherResourcesButton").GetComponent<Button>();
        // shrek = GameObject.Find("shrek");//swamp
        //sanc = GameObject.Find("sanc");
        //forest = GameObject.Find("forest");
        //keep?//this.MyText = GameObject.Find("DebugLog").GetComponent<Text>();// keep
        //MyDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        //currentBackgroundIndex = MyDropdown.value;

        //ChangeBackground(0);

        // redfireworks = GameObject.Find("redfireworks").GetComponent<ParticleSystem>();
        // greenfireworks = GameObject.Find("greenfireworks").GetComponent<ParticleSystem>();
        // bluefireworks = GameObject.Find("bluefireworks").GetComponent<ParticleSystem>();

        //ScanText = GameObject.Find("ScanText").GetComponent<Text>();
        // leftScanParticles = GameObject.Find("scantext_particles_left").GetComponent<ParticleSystem>();
        // rightscanParticles = GameObject.Find("scantext_particles_right").GetComponent<ParticleSystem>();

        //UnhideQuestAndGatherItems(false);

        string temp = BitToys.inst.ToString();
        //BitToysWrapper.inst.registerOnClaimToy(this.OnClaim);
        // BitToys.inst.nfc_onSawTag += OnSawTag;
        // BitToys.inst.onClaimToy_OK += claim_ok;
        // BitToys.inst.onClaimToy_Fail += claim_failed;

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
        //MyText.text += "\n current state is " + currentState.ToString();
        //only one player, one scan at a time
        //if (currentState != State.ScanIn)
        // return;
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
        else
        {
            this.ScanCardSuccess.SetActive(true);

            leftScanParticles.gameObject.SetActive(false);
            rightscanParticles.gameObject.SetActive(false);
            // if(MegaScanInParticles.gameObject.activeSelf)
            MegaScanInParticles.Play();


            //at this point it will always contain the key, this is just to prevent nulls
            if (playerScanInTimes.ContainsKey(CurrentPlayerID))
            {
                //playerIDsForQuests[CurrentPlayerID]++;
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

                //reset the location timer back to 0 (dont get a new location)
                GetComponent<PickLocationTimer>().ResetTimer();

                //handle screen activations
                this.ScanScreen.SetActive(false);// deactivate scan screen
            }// valid scan
        }//end of else
    } // end of function

    IEnumerator EnableReady2Scan()
    {
        yield return new WaitForSeconds(3.0f);
        ready2scan = true;
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

    /*
    void UnHideScanStuff(bool visible)
    {
        leftScanParticles.gameObject.SetActive(visible);
        rightscanParticles.gameObject.SetActive(visible);
        ScanText.gameObject.SetActive(visible);
    }

    void UnhideQuestAndGatherItems(bool visible)
    {
        QuestButton.gameObject.SetActive(visible);
        GatherButton.gameObject.SetActive(visible);
    }

    void ChangeFromScanToQuest()
    {

    }

    public void ChangeFromGatherToScan()
    {
        UnhideQuestAndGatherItems(false);
        UnHideScanStuff(true);
        //currentState = State.ScanIn;
        
    }

    void ChangeFromScanToGather()
    {
        UnhideQuestAndGatherItems(false);
        UnHideScanStuff(false);
        //currentState = State.ActualGather;

        UnhideQuestAndGatherItems(false);
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

                //TODO: add mountain  here
                //    break;
        }


        //currentState = State.ActualGather;

    }

    public void ChangeFromQuestGatherToQuest()
    {
        UnhideQuestAndGatherItems(false);
     //   currentState = State.ActualQuest;
    }*/
    /*
    public void ChangeFromGatherToQuestGather()
    {
        // UnHideQuestAndGatherButts(true);
        //update player quest- if done, do celebration, if only halfway done, tell it 
        if(playerIDQuestsProgress[CurrentPlayerID][(int)currentLocation] == 0)
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
    }*/
    /*
    //TODO: probably delete this and related functions
    public void ChangeFromQuestToQuestGather()
    {
        UnhideQuestAndGatherItems(true);
      //  currentState = State.QuestOrGather;
    }

    //after gather, go to quest update/complete, then scan
    public void ChangeFromQuestToScan()
    {
        //currentState = State.ScanIn;

    }

    public void ChangeFromQuestGatherToGather()
    {

    }

    void MoveToQuestOrResourceScreen()
    {
        //hide scan particles and text
        UnHideScanStuff(false);
        //unhide buttons for quest and gather
        UnhideQuestAndGatherItems(true);
 //       currentState = State.QuestOrGather;
    }

    void HideQuestAndGatherShit(bool visible)
    {
        QuestButton.gameObject.SetActive(visible);
        GatherButton.gameObject.SetActive(visible);
    }

    //TODO: wtf is this function?
    void HideActualGatherStuff(bool shrekIsVisible)
    {
       // shrek.SetActive(shrekIsVisible);
        //sanc.SetActive(!shrekIsVisible);
    }

    void MoveToActualGather()
    {
        UnHideScanStuff(false);
        HideQuestAndGatherShit(false);
        HideActualGatherStuff(true);
        
        //currentState = State.ActualGather;
    }
    */
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

    
    /*
    public void ShowQuestProgress()
    {
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

    public void ShowQuestPerLocation()// this is where resource tracking should be checked
    {
        if (playerIDQuestsProgress[CurrentPlayerID][(int)currentLocation] == 0)
        {
            playerIDQuestsProgress[CurrentPlayerID][(int)currentLocation] = 1;
            ShowContinueQuest();
        }
        else
        {
            ShowQuestComplete();
            playerIDQuestsProgress[CurrentPlayerID][(int)currentLocation] = 3;
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

    IEnumerator LateTurnOffComeBackLaterText()
    {
        yield return new WaitForSeconds(3);
        ComebackLater.gameObject.SetActive(false);
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
        //currentState = State.ScanIn;
        UnHideScanStuff(true);
    }*/

    public void ToggleDebugStuff()
    {
        // debugstuff.SetActive(!debugmode);
        //just move it offscreen for now

        //GameObject debugtogglebutton = GameObject.Find("debugtogglebutton");


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

    /* private void Update2()
      {
          if(debugstuff.activeSelf && (currentBackgroundIndex != MyDropdown.value))
          {
              //inside the change background function current location is set currentlocation
              ChangeBackground(MyDropdown.value);
              currentBackgroundIndex = MyDropdown.value;

          }

          if(currentState == State.ScanIn && Input.GetKeyDown(KeyCode.Space))//for debugging on teh PC
          {
              MegaScanInParticles.Play();
              CurrentPlayerID = "player1";

              int[] progress = new int[3];
              progress[0] = 0;
              progress[1] = 0;
              progress[2] = 0;

              if(!playerIDQuestsProgress.ContainsKey("player1"))
              {

                  playerIDQuestsProgress.Add("player1", progress);
              }

              if(!playerScanInTimes.ContainsKey("player1"))
              {
                  playerScanInTimes.Add("player1", Time.time);
              }

              bool validscan = CheckForValidScanTime(CurrentPlayerID);

              if (!validscan)
                  return;


              ChangeFromScanToGather();
          }


          if (currentState == State.ScanIn && Input.GetKeyDown(KeyCode.M))//for debuggin on the PC
          {
              MegaScanInParticles.Play();
              if(!playerIDsForQuests.ContainsKey("player2"))
              {
                  playerIDsForQuests.Add("player2", 1);
              }

              if(!playerIDQuestsProgress.ContainsKey("player2"))
              {
                  int[] progress = new int[3];
                  progress[0] = 0;
                  progress[1] = 0;
                  progress[2] = 0;

                  playerIDQuestsProgress.Add("player2", progress);
              }
              CurrentPlayerID = "player2";


              ChangeFromScanToGather();
          }

          //if (Input.GetKeyDown(KeyCode.Escape))
          {
              Debug.Log("current state is " + currentState.ToString());
        //      GoBack();
          }
      }*/

    public void GoBack()
    {
        //if(currentState == State.ActualGather)
        //ChangeFromGatherToScan();
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
