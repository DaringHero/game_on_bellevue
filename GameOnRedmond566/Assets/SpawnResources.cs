using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnResources : MonoBehaviour {

    public bool HasSpawnedAtLeastOnce = false;
    public GameObject[] resources;
    public int CurrentlyAvailableResources = 0;
    public Text gatherdonetext;
    public List<GameObject> listOfSpawnedResources;
    public GameObject MainManager;
    // public GameObject scales;
    // public GameObject braidedcord;
    // public GameObject wood;
    // public GameObject happypoo;
    // public GameObject mushrooms;
    // Use this for initialization
    public GameObject[] locations;

    public GameObject[] ForestResources;
    public GameObject[] SwampResources;
    public GameObject[] MountainResources;


    public ParticleSystem blue1f;
    public ParticleSystem red1f;
    public ParticleSystem blue2f;
    public ParticleSystem red12;
    public ParticleSystem blue3f;
    public ParticleSystem red3f;
    public int MinResourceNumber;
    void Start () {

        gatherdonetext = GameObject.Find("GatherResourcesCompleteText").GetComponent<Text>();
        MainManager = GameObject.Find("DebugLog");
        blue1f = GameObject.Find("bf1").GetComponent<ParticleSystem>();
        red1f = GameObject.Find("rf1").GetComponent<ParticleSystem>();
        blue2f = GameObject.Find("gf1").GetComponent<ParticleSystem>();
        red12 = GameObject.Find("gf2").GetComponent<ParticleSystem>();
        blue3f = GameObject.Find("bf2").GetComponent<ParticleSystem>();
        red3f = GameObject.Find("bf3").GetComponent<ParticleSystem>();

    
    }
	
	// Update is called once per frame
	void Update () {

        int resourcesStillOnScreen = 0;
        foreach (GameObject g in listOfSpawnedResources)
            {
            if (g.activeSelf)
                resourcesStillOnScreen++;
        }

        if(HasSpawnedAtLeastOnce && (resourcesStillOnScreen < MinResourceNumber))
        {
            PlayFireWorks();
            HasSpawnedAtLeastOnce = false;
        }

	}

    public void Spawn()
    {
        //grab resources randomly and spawn them at predefined locations
        //right now this will be called from the mega file YellOnClaim (lol, maybe fix that later to actually be reasonable)
   
        int numOfLocs = locations.Length;
        for (int i = 0; i < numOfLocs; ++i)
        {
            int lengthofresources = resources.Length;
            int randomIndex = Random.Range(0, lengthofresources);
            listOfSpawnedResources.Add(Instantiate(resources[randomIndex], locations[i].transform.position, Quaternion.identity));
            CurrentlyAvailableResources++;
        }
        HasSpawnedAtLeastOnce = true;
    }

    public void SpawnForForest()
    {
        //grab resources randomly and spawn them at predefined locations
        //right now this will be called from the mega file YellOnClaim (lol, maybe fix that later to actually be reasonable)

        int numOfLocs = locations.Length;
        for (int i = 0; i < numOfLocs; ++i)
        {
            int lengthofresources = ForestResources.Length;
            int randomIndex = Random.Range(0, lengthofresources);
            listOfSpawnedResources.Add(Instantiate(ForestResources[randomIndex], locations[i].transform.position, Quaternion.identity));
            CurrentlyAvailableResources++;
        }
        HasSpawnedAtLeastOnce = true;
    }

    public void SpawnForMountain()
    {
        //grab resources randomly and spawn them at predefined locations
        //right now this will be called from the mega file YellOnClaim (lol, maybe fix that later to actually be reasonable)

        int numOfLocs = locations.Length;
        for (int i = 0; i < numOfLocs; ++i)
        {
            int lengthofresources = MountainResources.Length;
            int randomIndex = Random.Range(0, lengthofresources);
            listOfSpawnedResources.Add(Instantiate(MountainResources[randomIndex], locations[i].transform.position, Quaternion.identity));
            CurrentlyAvailableResources++;
        }
        HasSpawnedAtLeastOnce = true;
    }

    public void SpawnForSwamp()
    {
        //grab resources randomly and spawn them at predefined locations
        //right now this will be called from the mega file YellOnClaim (lol, maybe fix that later to actually be reasonable)

        int numOfLocs = locations.Length;
        for (int i = 0; i < numOfLocs; ++i)
        {
            int lengthofresources = SwampResources.Length;
            int randomIndex = Random.Range(0, lengthofresources);
            listOfSpawnedResources.Add(Instantiate(SwampResources[randomIndex], locations[i].transform.position, Quaternion.identity));
            CurrentlyAvailableResources++;
        }
        HasSpawnedAtLeastOnce = true;
    }


    //celebratory function
    public void PlayFireWorks()
    {
        foreach(GameObject g in listOfSpawnedResources)
        {
            if (g.activeSelf)
                g.SetActive(false);
        }

        //gatherdonetext.gameObject.SetActive(true);

 //       StartCoroutine(LateCall());
     Debug.Log("PLAYING FIREWORKS");
     blue1f.Play();
     red1f.Play();
     blue2f.Play();
     red12.Play();
     blue3f.Play();
     red3f.Play();

     //   MainManager.GetComponent<YellOnClaim>().ShowQuestProgress();
        MainManager.GetComponent<YellOnClaim>().ShowQuestPerLocation();
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(3);
      //  gatherdonetext.gameObject.SetActive(false);

        
        //Do Function here...
    }

}
