using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStationScript : WaitAndActivate {

    public bool debugnewquests = false;
    public GameObject NewQuestsActivate;

    public override void OnEnable()
    {

        if (debugnewquests)//check if i need to show new quests
        {
            this.Activate = this.NewQuestsActivate;
        }

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
