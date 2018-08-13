using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScanPage : MonoBehaviour {

    public YellOnClaim myYellOnClaim;

    public  void OnEnable()
    {
       this.myYellOnClaim.leftScanParticles.gameObject.SetActive(true);
       this.myYellOnClaim.rightscanParticles.gameObject.SetActive(true);
    }
}
