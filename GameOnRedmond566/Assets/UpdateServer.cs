using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateServer : MonoBehaviour {

    public float waittime = 1.0f;
    public float retrytime = 10.0f; //for server
    public GameObject Activate;
    public GameObject Deactivate;

    public YellOnClaim myYellOnClaim;
    public int retrycount = 3;
    public int currentNumberOfRetries = 0;

    public void Start()
    {
        BitToys.inst.onPutCustomData_Fail += OnPutData_Fail;
        BitToys.inst.onPutCustomData_OK += OnPutData_Success;
    }

    public void OnPutData_Fail(string _id, BitToys.FailReason reason, string text)
    {
        Debug.Log(_id + " Reason: " + reason + " " + text);
        UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " PutData failed " + _id + " Reason: " + reason + " " + text + " ");

        currentNumberOfRetries++;
        if(currentNumberOfRetries < retrycount)
             StartCoroutine(WaitAndSendAgain());
        else
            StartCoroutine(WaitAndThenActivate());


    }

    public void OnPutData_Success(BitToys.Toy _toy)
    {
        UniClipboard.SetText(UniClipboard.GetText() + "\n" + System.DateTime.Now + " " + " data successfully updated on server ");
        StartCoroutine(WaitAndThenActivate());// bump players to next screen
    }

    private void OnEnable()
    {
        currentNumberOfRetries = 0;

        //send to doug's server
        GetComponent<JSONPost>().POST(myYellOnClaim.MyCurrentToy.customData.AsJSONString());

        if (myYellOnClaim.MyCurrentToy.bitToysId != "player1NUXtest")
            this.myYellOnClaim.MyCurrentToy.customData.SendAsync();
        else
            StartCoroutine(WaitAndThenActivate());

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
