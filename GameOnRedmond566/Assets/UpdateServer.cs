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
        this.myYellOnClaim.MyCurrentToy.customData.SendAsync();//send data to the server?

        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);
        myYellOnClaim.ready2scan = true;
        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
