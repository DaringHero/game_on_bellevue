using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnDragonLevel : MonoBehaviour {


	public YellOnClaim myYellOnClaim;
	public int dragonLevel = 0;

	public GameObject Activate;


	public void OnEnable()
	{

		if(myYellOnClaim.MyCurrentToy.customData.GetInt("DragonLevel", 0) == this.dragonLevel)
		{
			Activate.SetActive(true);
		}
		else
		{
			Activate.SetActive(false);
		}
	}
}
