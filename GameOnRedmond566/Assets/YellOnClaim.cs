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

    public GameObject SanctuaryTooSoon;
    public GameObject SanctuaryPage;

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
    public enum Location { SANC, SWAMP, MOUNTAIN, FOREST, LAKE, WIND, FARM, LUMBER, ORCHARD, MARKET, HUNTING };

    public enum CLEVELAND_Locations { LUMBER, ORCHARD};
    public enum RTCEAST_Locations { MARKET, WIND};
    public enum RTCWEST_Locations { MOUNTAIN, LAKE };
    public enum RANDOM_Locations {  HUNTING, FARM, SWAMP, FOREST };


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


    
    public ParticleSystem idleparticles1;
    public ParticleSystem idleparticles2;

    public void SetPageInfoEX()
    {
        SetStationPages tempSetStationPages = this.gameObject.GetComponent<SetStationPages>();
		DictionariesForThings tempConverstions = this.gameObject.GetComponent<DictionariesForThings>();

		tempSetStationPages.TunOffAllStampsOnAllCards();// turn off all the stamps, setting them will turn them back on

        float ratio = 0; //use this for progress ratio

        for (int i = 0; i < tempSetStationPages.myResourceCards.Count; ++i)// for each page
        {
			List<KeyValuePair<string, int>> questData = tempSetStationPages.GetQuestData();// get all quest data

			StampCard myStampcard = tempSetStationPages.myResourceCards[i].GetComponent<StampCard>();// stampcard

			int s = 0;
			//update each stamp on each card
			for (int d = 0; d < questData.Count; d++)// for all card datas
			{

				//Debug.Log("stamp = "+s.ToString() +"\t d = "+d.ToString() +"\t questData[d].Key = "+questData[d].Key);

				if(questData[d].Value > -1)
				{
					//display correct resource
					if (questData[d].Value == 1)
					{
						myStampcard.SetCompletedStamp(s, questData[d].Key);
					}
					if (questData[d].Value == 0)
					{
						myStampcard.SetStampBasedOnResource(s, questData[d].Key);
					}
					
					//Debug.Log("Set Data on::"+myStampcard.gameObject.name +" questData["+d.ToString()+"].Key = "+questData[d].Key);
					s++; //next stamp
				}

			}
            
        }
			

		// DRAGON LEVELS

		int dragonlevel = MyCurrentToy.customData.GetInt("DragonLevel", 0);
		Debug.Log("setting DragonLevel = "+dragonlevel.ToString());
		tempSetStationPages.SetDragonLevelColor(dragonlevel);







		//does this even work? ~Tom
        foreach (GameObject progressBar in tempSetStationPages.myProgressBars)
        {
            progressBar.GetComponent<EnergyBar>().SetValueF(ratio);
        }
    }

	public void SetNewResourceCards()
	{
		SetStationPages tempSetStationPages = this.gameObject.GetComponent<SetStationPages>();
		DictionariesForThings tempConverstions = this.gameObject.GetComponent<DictionariesForThings>();

		tempSetStationPages.TunOffAllStampsOnAllNewCards();// turn off all the stamps, setting them will turn them back on

		float ratio = 0; //use this for progress ratio

		for (int i = 0; i < tempSetStationPages.myLevelUpResourceCards.Count; ++i)// for each page
		{
			List<KeyValuePair<string, int>> questData = tempSetStationPages.GetQuestData();// get all quest data

			StampCard myStampcard = tempSetStationPages.myLevelUpResourceCards[i].GetComponent<StampCard>();// stampcard

			int s = 0;
			//update each stamp on each card
			for (int d = 0; d < questData.Count; d++)// for all card datas
			{

				//Debug.Log("stamp = "+s.ToString() +"\t d = "+d.ToString() +"\t questData[d].Key = "+questData[d].Key);

				if(questData[d].Value > -1)
				{
					//display correct resource
					if (questData[d].Value == 1)
					{
						myStampcard.SetCompletedStamp(s, questData[d].Key);
					}
					if (questData[d].Value == 0)
					{
						myStampcard.SetStampBasedOnResource(s, questData[d].Key);
					}

					//Debug.Log("Set Data on::"+myStampcard.gameObject.name +" questData["+d.ToString()+"].Key = "+questData[d].Key);
					s++; //next stamp
				}

			}

		}

	}
	public void SetNewResourceCards2()//level up resource cards
	{
		SetStationPages tempSetStationPages = this.gameObject.GetComponent<SetStationPages>();

		tempSetStationPages.TunOffAllStampsOnAllCards();// turn off all the stamps, setting them will turn them back on

		float ratio = 0; //use this for progress ratio

		for (int i = 0; i < tempSetStationPages.myLevelUpResourceCards.Count; ++i)// for each page
		{
			List<KeyValuePair<string, int>> questData = tempSetStationPages.GetQuestData();// get all quest data

			StampCard myStampcard = tempSetStationPages.myLevelUpResourceCards[i].GetComponent<StampCard>();// stampcard

			int d = 0;
			//update each stamp on each card
			for (int j = 0; j < 8; j++)
			{
				if(d < questData.Count)// dont try to grab questdata that doesn't exist
				{
					Debug.Log("d = "+d.ToString() +"\t questData[d].Key = "+questData[d].Key);
					if(questData[d].Value > -1)
					{
						//display correct resource
						myStampcard.SetCompletedStamp(d, questData[d].Key);

						//display stamp if obtained

						d++;
					}
					else
					{
						//myStampcard.SetStampBasedOnResource(d, questData[d].Key);
						d++;
					}
				}
			}

		}
	}

	/*
    public void SetPageinfo2()// updates the pages with player information
    {
        SetStationPages tempSetStationPages = this.gameObject.GetComponent<SetStationPages>();

        float ratio = 0; //use this for progress ratio

        foreach (GameObject stampCard in tempSetStationPages.myResourceCards)
        {

        }
        //tempSetStationPages.SetAllStationResoruces(tempSetStationPages.GetStationResource());
        foreach (GameObject stationResource in tempSetStationPages.myStationResources)
        {

        }
        foreach (GameObject dragonObject in tempSetStationPages.myDragons)
        {
            int dragonlevel = MyCurrentToy.customData.GetInt("DragonLevel", 0);
      //      dragonObject = GetComponent<getdragonbasedonlevel>().dragonlevel2Object[dragonlevel];
        }
        foreach (GameObject progressBar in tempSetStationPages.myProgressBars)
        {
            progressBar.GetComponent<EnergyBar>().SetValueF(ratio);
        }
    }*/

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

        debugmode = true;
        //origDebugPosition = debugstuff.transform.position;
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

       BitToys.inst.ble_onSawTag += OnSawTag;

    }

    public void ParticlesPlay()
    {
        idleparticles1.gameObject.SetActive(true);
        idleparticles2.gameObject.SetActive(true);
        idleparticles1.Play();
        idleparticles2.Play();
    }

    public void OnSawTag(string _id)
    {
        //ParticlesPlay();
        WriteToErrorLog("Saw tag id="+_id);
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
        ErrorLog = ("\n" + " "+(System.DateTime.Now).ToString() + " : " + message) + ErrorLog;
       // UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " : " + message);
        Debug.Log(ErrorLog);
        MyText.text += ("\n" + " " + (System.DateTime.Now).ToString() + " : " + message);//write to old debug

		ErrorLog = MaxText.ClampText(ErrorLog, 2000);// clamp text to prevent too much in the error log
    }

    public string WriteToErrorLog(string message, bool returnString)// new function for error logging, the old error logs are kinda a hack
    {
        ErrorLog = ("\n" + " " + (System.DateTime.Now).ToString() + " : " + message) + ErrorLog;
    //    UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " : " + message);
        Debug.Log(ErrorLog);
        MyText.text += ("\n" + " " + (System.DateTime.Now).ToString() + " : " + message);//write to old debug

		ErrorLog = MaxText.ClampText(ErrorLog, 2000);// clamp text to prevent too much in the error log

        if (returnString)
            return ("n" + " " + (System.DateTime.Now).ToString() + " : " + message);
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
 //       UniClipboard.SetText(ErrorLog);
    }
    public void Copytext()// should tie to button for copy buffer?
    {
        GUIUtility.systemCopyBuffer = this.MyText.text;
 //       UniClipboard.SetText(this.MyText.text);
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
        WriteToErrorLog("ClaimToySuccess");

        //debug for erasing cards
        SetLastToyScanned(theToy);

        //ready2scan = true;// override ready to scan non-sense
        if (!ready2scan)
        {
            //UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " Tried to scan but the reader wasn't ready...");
			this.WriteToErrorLog("ClaimToySuccess but was not ready");
            return;
        }
        ready2scan = false;// only take one scan at a time// reset in SetIsReadyForScan.cs
  
        this.MyCurrentToy = theToy;

        CurrentPlayerID = theToy.bitToysId;

		bool validscan = true;//was false// Scans always Active (unless a player is in progress)
        /*
        if (!playerScanInTimes.ContainsKey(CurrentPlayerID))
        {
            playerScanInTimes.Add(CurrentPlayerID, Time.time);
            validscan = true;
      
        }
        else
        {
            validscan = CheckForValidScanTime(CurrentPlayerID);
          
        }*/

        if (!validscan)
        {
            this.ScanScreen.SetActive(false);
            this.ScanCardTooRecent.SetActive(true); //weve seen this player before, and the time
                                                    //hasnt expired yet, so they need to come back later
                                                    //no need for return here, this goes to the end of the function just the same
         //   UniClipboard.SetText(UniClipboard.GetText() + "\n" + " " + System.DateTime.Now + " Tried to scan but it was too soon...");
            StartCoroutine(EnableReady2Scan());
          
        }
        else// if valid scan
        {
            WriteToErrorLog("GotValidScan");

			//we always need to update the station resources!
			SetStationPages tempSetStationPages = this.gameObject.GetComponent<SetStationPages>();
			DictionariesForThings tempConverstions = this.gameObject.GetComponent<DictionariesForThings>();
			Debug.Log("location = " + this.currentLocation.ToString() + "\t Location2ResourceDic = "+tempSetStationPages.Location2ResourceDic[this.currentLocation].ToString());
			Debug.Log("location = " + this.currentLocation.ToString() + "\t Location2Resource = "+tempConverstions.Location2Resource[this.currentLocation.ToString()].ToString());
			tempSetStationPages.SetAllStationResoruces(tempConverstions.Resource2Sprite[tempConverstions.Location2Resource[this.currentLocation.ToString()]]);
			//

            if ( this.ShowNUX && this.MyCurrentToy.customData.GetBool("NewUser", true))
            {
                this.MyCurrentToy.customData.AddInt("DragonsReleased", 0);
                this.MyCurrentToy.customData.AddBool("NewUser", false);// flag as an old user
                this.ScanCardNUX.SetActive(true);
                //first set of quests
                this.MyCurrentToy.customData.AddInt("DragonLevel", 0);
                this.SetNewQuestCard(this.MyCurrentToy.customData.GetInt("DragonLevel", 0));//rando some quests?

                WriteToErrorLog("Scan = New User Experience");
				this.SetPageInfoEX();// we need to set page infor for a correct scan
            }
            else
            {


                QuestProgress myQuestProgress = this.gameObject.GetComponent<QuestProgress>();

                // which state are we in?


                if (myQuestProgress.StationIsForQuest() && currentLocation != Location.SANC)
                {
					

                    //increment data for going to this station// ie collecting the resource
                    this.MyCurrentToy.customData.SetInt(this.GetComponent<DictionariesForThings>().Location2Resource[this.currentLocation.ToString()], 1);
	
                    this.SetPageInfoEX();// we need to set cards after new resource
                    if (myQuestProgress.CompletedAllQuests())
                    {
                        this.ScanCardNewQuests.SetActive(true);

                        int currentDragonLevel = this.MyCurrentToy.customData.GetInt("DragonLevel", 0);
                        currentDragonLevel += 1;
                        MyCurrentToy.customData.SetInt("DragonLevel", currentDragonLevel);

                        //HACK for getting new locations
                        //this.GetComponent<QuestProgress>().SetQuests(this.LocationsListHack());
                        this.SetNewQuestCard(currentDragonLevel);
						SetNewResourceCards();// should update the new cards with new quests
                        //update new quest ui?
                        //update new dragon?              
                        //get new quests?
                        //get next dragon?
                        WriteToErrorLog("Scan = New Quests");


                       
                    }
                    else// haven't completed all quests
                    {
						this.SetPageInfoEX();// we need to set up cards even if not right
                        this.ScanCardSuccess.SetActive(true);
                        WriteToErrorLog("Scan = Progress Quests");
                    }



                }
                else if(currentLocation != Location.SANC)// wrong station
                {
					this.SetPageInfoEX();// we need to set up cards even if not right
                    this.ScanCardWrong.SetActive(true);
                    WriteToErrorLog("Scan = Wrong Station");
                    //update cards ui?
                    //update dragon ui?
                }

                //TODO sanctuary too early
                if (currentLocation == Location.SANC)
                {
                    int currentDragonLevel = this.MyCurrentToy.customData.GetInt("DragonLevel", 0);

                    //release it
                    if(currentDragonLevel > 1)
                    {
                       int currentDragonsReleased =  this.MyCurrentToy.customData.GetInt("DragonsReleased", 0);
                        //
                        GetComponent<JSONPost>().POST(MyCurrentToy.customData.AsJSONString());
                        currentDragonsReleased += 1;
                        this.MyCurrentToy.customData.SetInt("DragonsReleased", currentDragonsReleased);

                        MyCurrentToy.customData.Remove("NewUser");
                        MyCurrentToy.customData.Remove("DragonLevel");

                        //setactive
                        SanctuaryPage.SetActive(true);

                    }
                    else if(currentDragonLevel < 2)//too early
                    {
                        SanctuaryTooSoon.SetActive(true);
                    }


                }
                //TODO sanctuary
            }
            

            leftScanParticles.gameObject.SetActive(false);
            rightscanParticles.gameObject.SetActive(false);
            idleparticles1.gameObject.SetActive(false);
            idleparticles2.gameObject.SetActive(false);
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
            this.WriteToErrorLog(tempDebug);

            foreach (KeyValuePair<string, float> entry in playerScanInTimes)
            {
                this.WriteToErrorLog( "\n Player ID: " + entry.Key.ToString() + " val is " + entry.Value.ToString());
            }//end for each
             //reset the location timer back to 0 (dont get a new location)
            GetComponent<PickLocationTimer>().ResetTimer();
            //handle screen activations
            this.ScanScreen.SetActive(false);// deactivate scan screen

            //UPDATE CUSTOM DATA!!!
            int currentDragonLevel2 = this.MyCurrentToy.customData.GetInt("DragonLevel", 0);
            this.MyCurrentToy.customData.SendAsync();//update that toy data!

        }//end of else valid scan
    } // end of function

    List<Location> LocationsListHack()
    {
        List<YellOnClaim.Location> temp = new List<Location>();
        temp.Add(Location.FARM);
        temp.Add(Location.LAKE);
        temp.Add(Location.WIND);
        temp.Add(Location.FOREST);

        return temp;
    }


    /// <summary>
    /// this function is exactly like the other functions but we now include 0 (we didnt before beaucse of the sanctuary)
    /// </summary>
    /// <param name="maxrange"></param>
    /// <param name="numberyouneed"></param>
    /// <returns></returns>
    public List<int> GetRandomUniqueNumbers0Indexed(int maxrange, int numberyouneed)
    {
        List<int> list2return = new List<int>();
        List<int> originallist = new List<int>();
        for (int i = 0; i < maxrange; ++i)
        {
            originallist.Add(i);
        }

        for (int i = 0; i < numberyouneed; ++i)
        {
            bool removed = false;
            while (!removed)
            {
                int removethis = Random.Range(0, maxrange);
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

    /// <summary>
    /// this function will do n choose k
    /// </summary>
    /// the number [exclusive] that is the max of your range
    /// <param name="maxrange"></param>
    /// the number of intns you need
    /// <param name="numberyouneed"></param>
    /// <returns></returns>
    public List<int> GetRandomUniqueNumbers(int maxrange, int numberyouneed)
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
            }     //end while
        } //end for
        return list2return;
    }
    //version with removing 1 so that we dont have our own location
    public List<int> GetRandomUniqueNumbers(int maxrange, int numberyouneed, int dontincludethisone)
    {
        if (numberyouneed >= maxrange)
            throw new System.Exception();

        List<int> list2return = new List<int>();
        List<int> originallist = new List<int>();
        //starting from 1 because we dont want the sanctuary
        for (int i = 1; i < maxrange; ++i)
        {
            if(i != dontincludethisone)
            originallist.Add(i);
        }

        for (int i = 0; i < numberyouneed; ++i)
        {
            bool removed = false;
            while (!removed)
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
            }//end while
        }//end for
        return list2return;
    }

    /// <summary>
    /// this function sets the custom data for whatever quests we want to 0 (-1 is not used, 1 is complete, 0 is need to complete)
    /// </summary>
    /// <param name="levelofdragon"></param>
    void SetNewQuestCard(int levelofdragon)
    {

            if ((levelofdragon == 0) || (levelofdragon == 1)) //egg, we need 4 scans to get to hatchling
            {
            //get 1 quest from each region, and 1 from random
            int clevelandChoice = Random.Range(0, 2);

            int rtceastChoice = Random.Range(0, 2);

            int rtcwestChoice = Random.Range(0, 2);

            int randoChoic = Random.Range(0, 4);
           // int randoChoic = 2;

            List<string> listofstrings = new List<string>();

            //clear all the previous quests
            foreach(string resources in GetComponent<DictionariesForThings>().Resource2Location.Keys)
            {
                int resint = MyCurrentToy.customData.GetInt(resources, -999);

                if (resint != -999)
                {
                    MyCurrentToy.customData.SetInt(resources, -1);
                }
            }

            //TODO-make it so that players do not repeat quests

            listofstrings.Add(GetComponent<DictionariesForThings>().CLEVELAND_EnumLocation2Resource[(CLEVELAND_Locations)clevelandChoice]);
            listofstrings.Add(GetComponent<DictionariesForThings>().RTCEAST_EnumLocation2Resource[(RTCEAST_Locations)rtceastChoice]);
            listofstrings.Add(GetComponent<DictionariesForThings>().RTCWEST_EnumLocation2Resource[(RTCWEST_Locations)rtcwestChoice]);
            listofstrings.Add(GetComponent<DictionariesForThings>().RANDOM_EnumLocation2Resource[(RANDOM_Locations)randoChoic]);

            foreach (string i in listofstrings)
                {
                    int resint = MyCurrentToy.customData.GetInt(i, -999);

                    if (resint == -999)
                    {
                        MyCurrentToy.customData.AddInt(i, 0);
                    }
                    else
                    {
                        MyCurrentToy.customData.SetInt(i, 0);
                    }
                }

            }
            else if (levelofdragon == 2) //adult, it is now time to release at the sanctuary
            {
            //clear all the previous quests
            foreach (string resources in GetComponent<DictionariesForThings>().Resource2Location.Keys)
            {
                int resint = MyCurrentToy.customData.GetInt(resources, -999);

                if (resint != -999)
                {
                    MyCurrentToy.customData.SetInt(resources, -1);
                }
            }

            int scale = MyCurrentToy.customData.GetInt("AMETHYST", -999);

                if (scale == -999)
                {
                    MyCurrentToy.customData.AddInt("AMETHYST", 0);
                }
                else
                {
                    MyCurrentToy.customData.SetInt("AMETHYST", 0);
                }
            }

        }

    /// <summary>
    /// this function enables the bool ready2scan in yellonclaim, which was designed to prevent "double" scans and other prorblems like that
    /// </summary>
    /// <returns></returns>
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
        WriteToErrorLog("Background = " + this.currentLocation.ToString());
        
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
            WriteToErrorLog("\n current time is " + Time.time.ToString());
            WriteToErrorLog("\n Last scan in time for this id was  = " + playerScanInTimes[CurrentPlayerID].ToString());
            WriteToErrorLog("\n ********************************");

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

        //first set of quests
        //this.MyCurrentToy.customData.AddInt("DragonLevel", 0);
       // this.MyCurrentToy.customData.AddInt("")
        //this.MyCurrentToy.customData.SendAsync();

        this.OnClaimToy_Success(this.MyCurrentToy, true);// fake the scan
        WriteToErrorLog("Fake NUX");
    }

    public void FakeFailScan()
    {
        this.OnClaimToy_Fail(BitToys.FailReason.NETWORK_ERROR, "test fail");
        WriteToErrorLog("Fake Fail");
    }

    public void FakeScan()
    {
        this.CurrentPlayerID = "player1NUXtest";
        this.MyCurrentToy.bitToysId = this.CurrentPlayerID;

        this.OnClaimToy_Success(this.MyCurrentToy, true);// fake the scan

        WriteToErrorLog("Fake Scan");
    }
    public void ResetFakeScan()
    {
        this.CurrentPlayerID = "player1test";
        this.MyCurrentToy = new BitToys.Toy();
        
        this.MyCurrentToy.bitToysId = this.CurrentPlayerID;
        this.MyCurrentToy.customData.ClearAll_Local();// clear all data?
        this.MyCurrentToy.customData.SendAsync();

        WriteToErrorLog("Reset Fake Scan");
    }

    public void SetLEDs()
    {/*
        // Method: SetLED_Connected(Red, Green, Blue, Red Pulse, Green Pulse, Blue Pulse).
                // When device is connected and idle, the light is solid bright blue.
        BitToys.inst.ble_SetLED_Connected(0, 0, 255, 0, 0, 0);
        // When a tag is detected and being read, change the light to a bright pulsing green
        BitToys.inst.ble_SetLED_nfcTagDetected(0, 255, 0, 0, 15, 0);
        // If a tag fails to read properly, change it to bright pulsing Red.
        BitToys.inst.ble_SetLED_nfcTagError(255, 0, 0, 15, 0, 0);
       // If a tag is read correctly, change the light to bright solid green.

        BitToys.inst.ble_SetLED_nfcTagOK(0, 255, 0, 0, 0, 0);
    */}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            FakeScanNUX();
            MyCurrentToy.customData.AddInt("Amethyst", 30);
            MyCurrentToy.customData.AddInt("Apple", 30);
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

        WriteToErrorLog( debugstring);
  //      UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " " + debugstring);

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
        string text = "OnGetToy_Fail" + "\n"; //clear text
       text += reason.ToString() + "\n";
       text += mytext + "\n";
       text += "\n ******************************";
        WriteToErrorLog(text);
    }               
    public void OnDeviceConnect( string mytext)//used for connected/lost/failed (will break into seperate functions if needed)
    {
        string text = "OnDeviceConnect" + "\n"; //clear text
       text += mytext + "\n";
       text += "\n ******************************";
        WriteToErrorLog(text);
  //      UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " Connected!");
    }

    public void OnFetchOwnedToys(List<BitToys.Toy> myToys, bool mybool)
    {
        foreach (BitToys.Toy toy in myToys)
        {
            
            string text = "\n Toy id = " + toy.bitToysId;
            text += "\n Owner id = " + toy.ownerId;
            text += "\n Style id = " + toy.styleId;
            text += "\n SKU id = " + toy.skuId;
            text += "\n ***********************";
            WriteToErrorLog(text);
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
