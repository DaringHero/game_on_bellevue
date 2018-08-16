using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getdragonbasedonlevel : MonoBehaviour {

    //access by using:
    //YellOnClaim.gameObject.GetComponent<getdragonbasedonlevel>().dragonlevel2Object[dragonlevel];

    public GameObject[] objects;
    public Dictionary<int, GameObject> dragonlevel2Object;
	// Use this for initialization
	void Start () {

        dragonlevel2Object = new Dictionary<int, GameObject>();

        int i = 0;

        foreach(GameObject g in objects)
        {
            dragonlevel2Object.Add(i, g);
            ++i;
        }



    }

}
