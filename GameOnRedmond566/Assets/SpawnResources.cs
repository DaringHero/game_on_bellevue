using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnResources : MonoBehaviour {

    public bool HasSpawnedAtLeastOnce = false;
    public GameObject[] resources;
    public int CurrentlyAvailableResources = 0;
    // public GameObject scales;
    // public GameObject braidedcord;
    // public GameObject wood;
    // public GameObject happypoo;
    // public GameObject mushrooms;
    // Use this for initialization
    public GameObject[] locations;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(HasSpawnedAtLeastOnce && CurrentlyAvailableResources < 3)
        {
            PlayFireWorks();
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
            int randomIndex = Random.Range(0, lengthofresources-1);
            Instantiate(resources[randomIndex], locations[i].transform.position, Quaternion.identity);
            CurrentlyAvailableResources++;
        }
        HasSpawnedAtLeastOnce = true;
    }

    //celebratory function
    public void PlayFireWorks()
    {
        
    }
}
