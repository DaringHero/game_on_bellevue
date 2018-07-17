using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHarvest : MonoBehaviour {

    public string ResourceType = "DebugResourceName";
    public YellOnClaim myYellOnClaim = null;
    public ResourceSpawner2 mySpawner = null;


    private void OnMouseDown()
    {
        if (this.myYellOnClaim.MyCurrentToy != null)
        {
            if (this.myYellOnClaim == null) { 
            this.myYellOnClaim = GameObject.Find("DebugLog").GetComponent<YellOnClaim>();//hack since these are instances?
            }

            //if this toy does have the resource to the toy
            int resourceCounttemp = this.myYellOnClaim.MyCurrentToy.customData.GetInt(this.ResourceType, 0);
            if (resourceCounttemp == 0)
            {
                this.myYellOnClaim.MyCurrentToy.customData.AddInt(this.ResourceType, 1);
            }
            else
            {
                this.myYellOnClaim.MyCurrentToy.customData.SetInt(this.ResourceType, resourceCounttemp + 1);
            }

            this.mySpawner.OnCollectedResource();//
            this.gameObject.SetActive(false);
        }
    }
}
