using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogOnEnabled : MonoBehaviour {
    public string message;
    public void OnEnable()
    {
        Debug.Log(message);
    }
}
