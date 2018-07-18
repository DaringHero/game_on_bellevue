using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateServer : MonoBehaviour {

    public float waittime = 1.0f;
    public float retrytime = 3.0f; //for server
    public GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;

    public void Start()
    {
        BitToys.inst.onPutCustomData_Fail += OnPutData_Fail;
        BitToys.inst.onPutCustomData_OK += OnPutData_Success;
    }

    public void OnPutData_Fail(string _id, BitToys.FailReason reason, string text)
    {
        Debug.Log(_id + " Reason: " + reason + " " + text);
        //send again
    }

    public void OnPutData_Success(BitToys.Toy _toy)
    {
        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    private void OnEnable()
    {
        
        bool sentsuccess = this.myYellOnClaim.MyCurrentToy.customData.SendAsync();


    }

    IEnumerator WaitAndSendAgain()
    {
        yield return new WaitForSeconds(retrytime);
        this.myYellOnClaim.MyCurrentToy.customData.SendAsync();
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
