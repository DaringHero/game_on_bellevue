using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class YellOnClaim : MonoBehaviour
{
  Text text;

  // Use this for initialization
  void Start ()
  {
    text = GetComponent<Text>();
    BitToysWrapper.inst.registerOnClaimToy(OnClaim);
    // BitToys.inst.nfc_onSawTag += OnSawTag;
    // BitToys.inst.onClaimToy_OK += claim_ok;
    // BitToys.inst.onClaimToy_Fail += claim_failed;
  }

  void OnClaim(ToyWrapper toy, bool TLL)
  {
    if(toy.customData.GetBool_Global("installed", false) == false)
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

    text.text = "Welcome, " + username + ". You're score is: " + score + "\n";
    text.text += "You're team, " + team + ", has " + team_score + " points.";
  }
}
