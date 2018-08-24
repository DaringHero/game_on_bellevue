using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulse : MonoBehaviour {

    public float maxSize = 2.5f;
    public float minSize = 1.5f;
    float speed = 2.0f;

    // Use this for initialization
    void Start () {
		
	}


    void Update()
    {
        var range = maxSize - minSize;
        float thesize = (float)((Mathf.Sin(Time.time * speed) + 1.0) / 2.0 * range + minSize);
        transform.localScale = new Vector3(thesize, thesize, thesize);
    }
    // Update is called once per frame

}
