using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHarvest : MonoBehaviour {

    public string ResourceType = "DebugResourceName";
    public YellOnClaim myYellOnClaim = null;
    public ResourceSpawner2 mySpawner = null;
    public AudioClip soundeffect;
    public GameObject particles;

    private void OnMouseDown()
    {
        if (this.myYellOnClaim.MyCurrentToy != null)
        {
            if (this.myYellOnClaim == null) { 
            this.myYellOnClaim = GameObject.Find("DebugLog").GetComponent<YellOnClaim>();//hack since these are instances?
            }

            Debug.Log("Resource type is: " + this.ResourceType);
            //if this toy does have the resource to the toy
            int resourceCounttemp = this.myYellOnClaim.MyCurrentToy.customData.GetInt(this.ResourceType, -999);
            if (resourceCounttemp == -999)
            {
                this.myYellOnClaim.MyCurrentToy.customData.AddInt(this.ResourceType, 1);
                resourceCounttemp = 1;
            }
            else
            {
                this.myYellOnClaim.MyCurrentToy.customData.SetInt(this.ResourceType, ++resourceCounttemp);
            }

            if (myYellOnClaim.resourceCountForText.ContainsKey(ResourceType))
                myYellOnClaim.resourceCountForText[ResourceType]++;
            else
                myYellOnClaim.resourceCountForText.Add(ResourceType, 1);

            Instantiate(particles, gameObject.GetComponentInParent<ParticleSpawn>().particlespawn.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(soundeffect, Vector3.zero);
            Debug.Log("Currently have " + resourceCounttemp  + " " + this.ResourceType);
            this.mySpawner.OnCollectedResource();//
            this.gameObject.SetActive(false);
        }
    }
}
