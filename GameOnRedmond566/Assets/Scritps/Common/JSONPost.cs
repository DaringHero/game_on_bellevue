﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JSONPost : MonoBehaviour {

    private static readonly string POSTAddUserURL = "http://www.daringhero.com/redmond/park/sanctuary/post_update_resources.php";




    public WWW POST(string jsonStr)
    {
        WWW www;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
        postHeader.Add("Content-Type", "application/json");


        Debug.Log("jsonstr is " + jsonStr) ;

        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        Debug.Log("form data is " + formData.ToString());
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
