using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStationResourcesOnEnable : MonoBehaviour {

	public YellOnClaim myYellOnClaim;

	public void OnEnable()
	{
		//StartCoroutine(this.UpdateStationResources());// hack to make sure things are initialized
	}

	public void OnDisable()
	{
		//StopCoroutine(this.UpdateStationResources());// something else will handle this
	}

	public void RefreshTheStationResource()
	{
		if(this.myYellOnClaim.currentLocation != YellOnClaim.Location.SANC)
		{
			this.gameObject.SetActive(true);
		//we always need to update the station resources!
		SetStationPages tempSetStationPages = myYellOnClaim.gameObject.GetComponent<SetStationPages>();
		DictionariesForThings tempConverstions = myYellOnClaim.gameObject.GetComponent<DictionariesForThings>();
		tempSetStationPages.SetAllStationResoruces(tempConverstions.Resource2Sprite[tempConverstions.Location2Resource[myYellOnClaim.currentLocation.ToString()]]);
		//
			Debug.Log("Set resource for location = " + myYellOnClaim.currentLocation.ToString() +"\t Location2Resource = "+tempConverstions.Location2Resource[myYellOnClaim.currentLocation.ToString()].ToString());

		}
		else
		{
			this.gameObject.SetActive(false);
		}

	}

	public IEnumerator UpdateStationResources()
	{
		yield return new WaitForSeconds(1.0f);

		//we always need to update the station resources!
		SetStationPages tempSetStationPages = myYellOnClaim.gameObject.GetComponent<SetStationPages>();
		DictionariesForThings tempConverstions = myYellOnClaim.gameObject.GetComponent<DictionariesForThings>();
		tempSetStationPages.SetAllStationResoruces(tempConverstions.Resource2Sprite[tempConverstions.Location2Resource[myYellOnClaim.currentLocation.ToString()]]);
		//
		Debug.Log("Set resource for location = " + myYellOnClaim.currentLocation.ToString() +"\t Location2Resource = "+tempConverstions.Location2Resource[myYellOnClaim.currentLocation.ToString()].ToString());

		yield return null;
	}
}
