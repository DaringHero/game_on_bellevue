using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class YellOnClaim : MonoBehaviour
{
  public Text MyText;

    public ParticleSystem redfireworks;
    public ParticleSystem greenfireworks;
    public ParticleSystem bluefireworks;

  // Use this for initialization
  void Start ()
  {
    this.MyText = GetComponent<Text>();

        redfireworks = GameObject.Find("redfireworks").GetComponent<ParticleSystem>();
        greenfireworks = GameObject.Find("greenfireworks").GetComponent<ParticleSystem>();
        bluefireworks = GameObject.Find("bluefireworks").GetComponent<ParticleSystem>();

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

    }
    public void OnClaimToy_Success(BitToys.Toy theToy, bool val)
    {
        MyText.text += ""; //clear text

        MyText.text += "\n Toy id = " + theToy.bitToysId;
        MyText.text += "\n Owner id = " + theToy.ownerId;
        MyText.text += "\n Style id = " + theToy.styleId;
        MyText.text += "\n SKU id = " + theToy.skuId;
        MyText.text += "\n ******************************";

        if(theToy.styleId == "gameon_red")
        {
            redfireworks.Stop();
            redfireworks.Play();
        }

        if(theToy.styleId == "gameon_green")
        {
            greenfireworks.Stop();
            greenfireworks.Play();
        }

        if((theToy.styleId == "gameon_gray") || (theToy.styleId == "gameon_blue"))
        {
            bluefireworks.Stop();
            bluefireworks.Play();
        }

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
