using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateServer : MonoBehaviour {

    public float waittime = 0.1f;

    public GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;

    private void OnEnable()
    {
        bool sentsuccess = this.myYellOnClaim.MyCurrentToy.customData.SendAsync();

        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);
        myYellOnClaim.ready2scan = true;

        if(myYellOnClaim.particleMode)
        {
            myYellOnClaim.leftScanParticles.gameObject.SetActive(true);
            myYellOnClaim.leftScanParticles.Play();
            myYellOnClaim.rightscanParticles.gameObject.SetActive(true);
            myYellOnClaim.rightscanParticles.Play();

        }

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
