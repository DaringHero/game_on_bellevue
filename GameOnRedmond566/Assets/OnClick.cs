using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

    GameObject Resourcespawner;
    public GameObject particleeffects;
  //  GameObject
    public int ID; //this is the type of resource
	// Use this for initialization
	void Start () {
        Resourcespawner = GameObject.Find("ResourceSpawner");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        
        Resourcespawner.GetComponent<SpawnResources>().CurrentlyAvailableResources--;
        //gather the resource

        Instantiate(particleeffects);
        gameObject.SetActive(false);
    }
}
