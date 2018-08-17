using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateForNewQuests : MonoBehaviour {
	public YellOnClaim myYellOnClaim;
	//bool run = false;
	// Update is called once per frame
	void OnEnable () {


			myYellOnClaim.SetPageInfoEX();
		
	}
}
