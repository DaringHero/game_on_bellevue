using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JSONPost : MonoBehaviour {

    private static readonly string POSTAddUserURL = "http://www.daringhero.com/redmond/park/sanctuary/display_data.php";


    public string convertjson(string jsonstr)
    {
        string a = "";
        return a;
    }

    public WWW POST(string jsonStr)
    {
        WWW www;
        Dictionary<string, string> postHeader = new Dictionary<string, string>();
     //   postHeader.Add("Content-Type", "text/plain");


        Debug.Log("jsonstr is " + jsonStr) ;

        string newstring =  "{ \"name\":\"amethyst\",\"amount\":\"1\"}, { \"name\":\"apples\",\"amount\":\"1\"}, { \"name\":\"berries\",\"amount\":\"1\"}";
        string ultrastring = "apple";
        // convert json string to byte
        var formData = System.Text.Encoding.UTF8.GetBytes(ultrastring);
        Debug.Log("form data is " + formData.ToString());
        www = new WWW(POSTAddUserURL, formData, postHeader);
        
        

        StartCoroutine(WaitForRequest(www));
        return www;
    }
    
    public void Post2()
    {

            StartCoroutine(Upload());
   

    }


    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("gameData", "myData");
        string newstring = "[{ \"name\":\"amethyst\",\"amount\":\"1\"}, { \"name\":\"apples\",\"amount\":\"1\"}, { \"name\":\"berries\",\"amount\":\"1\"}]";


        //  using (UnityWebRequest www = UnityWebRequest.Post(POSTAddUserURL, form))
        using (UnityWebRequest www = UnityWebRequest.Put(POSTAddUserURL, System.Text.Encoding.UTF8.GetBytes(newstring)))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "text/plain");
            www.chunkedTransfer = false;
            yield return www.Send();
            
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
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
