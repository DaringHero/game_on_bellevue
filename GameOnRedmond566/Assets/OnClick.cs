using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

    GameObject Resourcespawner;
	// Use this for initialization
	void Start () {
        //Resourcespawner = GameObject.Find("ResourceSpawner");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        
        //Resourcespawner.GetComponent<SpawnResources>().CurrentlyAvailableResources--;
        //gameObject.SetActive(false);
    }
}
