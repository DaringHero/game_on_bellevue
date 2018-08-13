using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndActivate : MonoBehaviour {

    public bool CanTouchToSkip = true;
    public bool StartOnEnable = false;
    public float waittime = 3.0f;
    public GameObject Activate;
    public GameObject Deactivate;
    public GameObject TouchToSkipMessage;
    public float timer = 0.0f;
    private bool DoubleTap = false;
    private int touches = 0;

    public virtual void OnEnable()
    {
        //reset timer vars
         timer = 0.0f;
         DoubleTap = false;
         touches = 0;

        if (StartOnEnable)
        {
            if (this.CanTouchToSkip)
            {
                Debug.Log("can touch to skip");
                StartCoroutine(this.WaitAndThenActivateTouch());
            }
            else {
                StartCoroutine(this.WaitAndThenActivate());
            }
        }

        if (GetComponent<PlayFireworks>())
        {
            GetComponent<PlayFireworks>().PlayTheFireWorks();
        }
    }

    public virtual IEnumerator WaitAndThenActivate()
    {
        yield return new WaitForSeconds(waittime);

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
    }

    public virtual IEnumerator WaitAndThenActivateTouch()
    {

        while (timer < this.waittime && !DoubleTap)// waiting and hasnt doubletapped
        {
            if ( ((Input.touches.Length>0) && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetKeyUp(KeyCode.Return))// check for taps
            {
                if (TouchToSkipMessage != null)// show tap message if there is tapping
                {
                    this.TouchToSkipMessage.SetActive(true);
                }

                touches += 1;
                if (touches > 1)
                {
                    DoubleTap = true;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }// timer

        if (TouchToSkipMessage != null)// unshow tap message
        {
            this.TouchToSkipMessage.SetActive(false);
        }

        this.Activate.SetActive(true);
        this.Deactivate.SetActive(false);
        if(GetComponent<setready2scantrue>())
        {
            GetComponent<setready2scantrue>().setit(true);
        }
    }
}
