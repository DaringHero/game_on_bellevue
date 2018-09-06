using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCorrectDragonLevel : MonoBehaviour {

    public YellOnClaim myYellOnClaim;
    public Text t;

    private void OnEnable()
    {
        //
        int dragonlevel = myYellOnClaim.MyCurrentToy.customData.GetInt("DragonLevel", 0);
        if (dragonlevel == 0)
        {
            t.text = "an egg";
        }
            else if(dragonlevel == 1)
        {
            t.text = "a hatchling";
        }
            else if(dragonlevel == 2)
        {
            t.text = "an adult";
        }
    }
		
}
