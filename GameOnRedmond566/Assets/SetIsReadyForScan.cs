using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIsReadyForScan : MonoBehaviour {

	public YellOnClaim myYellOnClaim;

	public void OnEnable()
	{
		myYellOnClaim.ready2scan = true;

	}
}
