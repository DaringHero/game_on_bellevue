using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceSpawner2 : MonoBehaviour {

    public YellOnClaim myYellOnClaim;
    public YellOnClaim.Location location;
    public List<GameObject> SpawnPoints;
    private List<GameObject> SpawnedResources = new List<GameObject>();
    public GameObject uniqueResource;
    public GameObject Resource1;
    public GameObject Resource2;

    public int CollectedResource = 0;
    public int MaxResourcesToCollect = 4;

    public GameObject Nextpage;
    public GameObject Deactivate;

    public Text resourceCountText;

    public void OnEnable()
    {
        Debug.Log("loc ="+ this.location.ToString()+ " currentLocation = "+ myYellOnClaim.currentLocation.ToString());
        this.CollectedResource = 0;


        if (this.location == myYellOnClaim.currentLocation)//if this is the correct location
        {
            myYellOnClaim.resourceCountForText.Clear();
            Debug.Log("******************************CorrectLocation");
            //spawn resources
            for (int i = 0; i < this.SpawnPoints.Count; i++)
            {
                //Debug.Log("boop");

                if ((myYellOnClaim.MyCurrentToy.customData.GetInt("CurrentQuest",0) == (int)this.location) && (i < 2))//if current quest
                {

                    GameObject resource = Object.Instantiate(this.uniqueResource, SpawnPoints[i].transform);
                    OnClickHarvest temp = resource.GetComponent<OnClickHarvest>();
                    temp.mySpawner = this;
                    temp.myYellOnClaim = this.myYellOnClaim;
                    this.SpawnedResources.Add(resource);
                }
                else
                {
                    Debug.Log("boop");

                    int roll = Random.Range(0, 2);//need to make less random HACK
                    if (roll == 1)
                    {
                        GameObject resource = Object.Instantiate(this.Resource1, SpawnPoints[i].transform);
                        OnClickHarvest temp = resource.GetComponent<OnClickHarvest>();
                        temp.mySpawner = this;
                        temp.myYellOnClaim = this.myYellOnClaim;
                        this.SpawnedResources.Add(resource);
                    }
                    else
                    {
                        GameObject resource = Object.Instantiate(this.Resource2, SpawnPoints[i].transform);
                        OnClickHarvest temp = resource.GetComponent<OnClickHarvest>();
                        temp.mySpawner = this;
                        temp.myYellOnClaim = this.myYellOnClaim;
                        this.SpawnedResources.Add(resource);
                    }

                }

                
            }
        }
    }

    public void OnCollectedResource()//called by spawned resources
    {
        this.CollectedResource++;

        if (this.MaxResourcesToCollect <= this.CollectedResource)
        {
            this.CollectedResource = 0;//make sure this var is reset
            //goto next page
            resourceCountText.text = "";
            //update the text
            foreach(KeyValuePair<string,int> a in myYellOnClaim.resourceCountForText)
            {
                resourceCountText.text += "\n " + a.Value + " " + a.Key;
            }

            this.Nextpage.SetActive(true);
          
            this.Deactivate.SetActive(false);
        }

    }

    public void OnDisable()
    {
        this.CollectedResource = 0;//make sure this var is reset
        foreach (GameObject resource in this.SpawnedResources)
        {
            Destroy(resource);
        }
    }

}
