using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setready2scantrue : MonoBehaviour {

    public YellOnClaim myYellOnClaim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
      
    }

    public void setit(bool set)
    {
        myYellOnClaim.ready2scan = set;
    }
}
