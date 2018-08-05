using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickLocationTimer : MonoBehaviour {

    public float currentTime;
    public float timeUntilReset; //30
    public GameObject getQuestObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        currentTime += Time.deltaTime;
       
        if(currentTime > timeUntilReset)
        {
            ResetTimer();
            ChooseLocation();
        }

	}

    public void ChooseLocation()
    {
        getQuestObject.GetComponent<GetQuest>().ChooseANewRandomLocation();
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }
}
