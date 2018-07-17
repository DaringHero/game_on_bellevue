using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndActivate : MonoBehaviour {

    public bool StartOnEnable = false;
    public float waittime = 3.0f;
    public GameObject Activate;
    public GameObject Deactivate;

    public void OnEnable()
    {
        if (StartOnEnable)
        {
            StartCoroutine(this.WaitAndThenActivate());
        }
    }

    IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }
}
