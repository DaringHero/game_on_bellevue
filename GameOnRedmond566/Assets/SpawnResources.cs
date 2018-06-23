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

    public ParticleSystem blue1f;
    public ParticleSystem red1f;
    public ParticleSystem blue2f;
    public ParticleSystem red12;
    public ParticleSystem blue3f;
    public ParticleSystem red3f;

    void Start () {

        blue1f = GameObject.Find("bf1").GetComponent<ParticleSystem>();
        red1f = GameObject.Find("rf1").GetComponent<ParticleSystem>();
        blue2f = GameObject.Find("gf1").GetComponent<ParticleSystem>();
        red12 = GameObject.Find("gf2").GetComponent<ParticleSystem>();
        blue3f = GameObject.Find("bf2").GetComponent<ParticleSystem>();
        red3f = GameObject.Find("bf3").GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if(HasSpawnedAtLeastOnce && (CurrentlyAvailableResources < 3))
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
            int randomIndex = Random.Range(0, lengthofresources-1);
            Instantiate(resources[randomIndex], locations[i].transform.position, Quaternion.identity);
            CurrentlyAvailableResources++;
        }
        HasSpawnedAtLeastOnce = true;
    }

    //celebratory function
    public void PlayFireWorks()
    {
        Debug.Log("PLAYING FIREWORKS");
        blue1f.Play() ;
     red1f.Play();
     blue2f.Play();
     red12.Play();
     blue3f.Play();
     red3f.Play();
}
}
