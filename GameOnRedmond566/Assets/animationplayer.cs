using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationplayer : MonoBehaviour {

    public Animation a;
    public string animation2play;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        a.PlayQueued(animation2play);
    }
}
