using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickToPOST : MonoBehaviour {

    private static readonly string POSTAddUserURL = "INSERTLATER";

    var speed = 1.0f; //how fast it shakes
    var amount = 1.0f; //how much it shakes
    public bool interacted = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(this.)

            if(interacted)
                transform.position.x = Mathf.Sin(Time.time * speed) * amount;
    }

    public WWW POST()
    {
        WWW www;
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        string jsonStr = "\"\" "; //dont put newlines


        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

        www = new WWW(POSTAddUserURL, formData, postHeader);
        StartCoroutine(WaitForRequest(www));
        return www;
    }


    IEnumerator WaitForRequest(WWW data)
    {
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            Debug.Log("There was an error sending request: " + data.error);
        }
        else
        {
            Debug.Log("WWW Request: " + data.text);
        }
    }
}
