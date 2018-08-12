using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStationScript : WaitAndActivate {


    public override void OnEnable()
    {

        base.OnEnable();// do on enable
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
