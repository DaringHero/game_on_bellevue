using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyaftertime : MonoBehaviour {

    public float TimeUntilKill;

    float startTime;
    
	// Use this for initialization
	void Start () {

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > (startTime + TimeUntilKill))
            Destroy(gameObject);

		
	}
}
