using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIsReadyForScan : MonoBehaviour {

	public YellOnClaim myYellOnClaim;

	public void OnEnable()
	{
		myYellOnClaim.ready2scan = true;
        myYellOnClaim.leftScanParticles.gameObject.SetActive(true);
        myYellOnClaim.leftScanParticles.Play();
        myYellOnClaim.rightscanParticles.gameObject.SetActive(true);
        myYellOnClaim.rightscanParticles.Play();
    }
}
