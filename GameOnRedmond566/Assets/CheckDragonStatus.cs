using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDragonStatus : MonoBehaviour {

    public static int HatchOnQuest = 1;
    public static int JuvinileOnQuest = 3;// -1 is never
    public static int AdultOnQuest = 6;

    public float waittime = 3.0f;
    public GameObject DragonHatch;
    public GameObject DragonJuvinile;
    public GameObject DragonAdult;

    public GameObject DragonNothing;
    private GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;


    private void OnEnable()
    {
        Debug.Log("___ CheckDragonStatus Enabled ___");


        int QuestsCompleted = this.myYellOnClaim.MyCurrentToy.customData.GetInt("QuestsCompleted", 0);

        bool ShowUpgrade = this.myYellOnClaim.MyCurrentToy.customData.GetBool("DragonUpgraded", false);
        

        this.Activate = this.DragonNothing;// default to no dragon upgrade *sadface*
        if (ShowUpgrade)
        {
            if (QuestsCompleted == HatchOnQuest)// time for the dragon to hatch
            {
                this.Activate = this.DragonHatch;// set correct next screen
                this.myYellOnClaim.MyCurrentToy.customData.SetBool("DragonUpgraded", false);
            }
            if (QuestsCompleted == JuvinileOnQuest)// time for the dragon to become an adult
            {

                this.Activate = this.DragonAdult;//set correct next page
                this.myYellOnClaim.MyCurrentToy.customData.SetBool("DragonUpgraded", false);
            }
            if (QuestsCompleted == AdultOnQuest)// time for the dragon to become an adult
            {

                this.Activate = this.DragonAdult;//set correct next page
                this.myYellOnClaim.MyCurrentToy.customData.SetBool("DragonUpgraded", false);
            }
        }
        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
