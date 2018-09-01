using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkAndDing : MonoBehaviour {

    public Vector3 orig;
    public Vector3 dest;
    public AudioClip soundeffect;
    bool going = false;
    bool hasplayedsound = false;
    float theTime = 0;
    // Use this for initialization
    void Start () {
        transform.localScale = orig;
	}
	
	// Update is called once per frame
	void Update () {

        if(going)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, dest, Time.deltaTime * 2 );
            theTime += Time.deltaTime;
        }

        if(theTime >1.5f && !hasplayedsound)
        {
            hasplayedsound = true;
            AudioSource.PlayClipAtPoint(soundeffect, Vector3.zero);
        }

	}

    private void OnEnable()
    {
        transform.localScale = orig;
        theTime = 0;
        going = true;
    }
}
