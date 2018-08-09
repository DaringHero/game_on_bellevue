using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPlayerDataToJson : MonoBehaviour {
    public YellOnClaim myYellOnClaim;
    public Text OutputText;

    // Update is called once per frame
    void FixedUpdate () {
        if(this.myYellOnClaim.MyCurrentToy != null)
        {
            this.OutputText.text = this.myYellOnClaim.MyCurrentToy.customData.AsJSONString();

        }
    }
}
