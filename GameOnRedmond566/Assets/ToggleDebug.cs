using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDebug : MonoBehaviour {

    public bool debugActive;
    public GameObject debugstuff;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (debugActive == false)
            debugstuff.SetActive(false);
        else
            debugstuff.SetActive(true);
            
	}
}
