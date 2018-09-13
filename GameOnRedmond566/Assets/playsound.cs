using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsound : MonoBehaviour {

    public AudioClip dragonsound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        AudioSource.PlayClipAtPoint(dragonsound, Vector3.zero);
    }
}
