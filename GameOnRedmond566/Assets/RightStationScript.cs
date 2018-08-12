using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStationScript : WaitAndActivate {


    public override void OnEnable()
    {

        base.OnEnable();// do on enable

        //get new list of stations based off custom data
        YellOnClaim.Location location;
        //


    }

    public override IEnumerator WaitAndThenActivate()
    {
        return base.WaitAndThenActivate();
    }

    public override IEnumerator WaitAndThenActivateTouch()
    {
        return base.WaitAndThenActivateTouch();
    }
}
