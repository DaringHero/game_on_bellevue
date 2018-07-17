using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDragonStatus : MonoBehaviour {

    public static int HatchOnQuest = 1;
    public static int AdultOnQuest = 4;

    public float waittime = 3.0f;
    public GameObject DragonHatch;
    public GameObject DragonAdult;

    public GameObject DragonNothing;
    private GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;


    private void OnEnable()
    {
        int QuestsCompleted = this.myYellOnClaim.MyCurrentToy.customData.GetInt("QuestsCompleted", 0);

        

        this.Activate = this.DragonNothing;// default to no dragon upgrade *sadface*

        if (QuestsCompleted == HatchOnQuest)// time for the dragon to hatch
        {
            this.Activate = this.DragonHatch;// set correct next screen
        }
        if (QuestsCompleted == AdultOnQuest)// time for the dragon to become an adult
        {
            
            this.Activate = this.DragonAdult;//set correct next page
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
