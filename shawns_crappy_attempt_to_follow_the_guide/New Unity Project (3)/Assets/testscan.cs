using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BitToys))]
public class testscan : MonoBehaviour {

    public Text text;
    
    // Use this for initialization
    void Start () {

        BitToys.inst.Init("testprojectlol");
        BitToys.inst.ble_QuickConnectReader();
        BitToys.inst.onClaimToy_OK += OnClaimToy_OKEventHandler;
        BitToys.inst.onClaimToy_Fail += OnClaimToy_FAIL;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnClaimToy_OKEventHandler(BitToys.Toy toy, bool TLL_val)
    {

    }

    private void OnClaimToy_FAIL(BitToys.FailReason reason, string texto)
    {
        text.text = reason.ToString();
        text.text += "\n";
        text.text += texto;
    }


}
