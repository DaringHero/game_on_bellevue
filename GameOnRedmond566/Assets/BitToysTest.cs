using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitToysTest : MonoBehaviour
{
    public bool initializeBitToys = false;
    public void Start()
    {
        if(this.initializeBitToys)
        {
            BitToys.inst.Init(SystemInfo.deviceUniqueIdentifier);// start an instance of bittoys
        }
    }

    public void ScanForReader()
    {
        
        BitToys.inst.ble_QuickConnectReader();
    }
    public void StopConnectionAttemptToReader()
    {

        BitToys.inst.ble_CancelQuickConnect();
    }

    public void DisconnectFromReader()
    {

        BitToys.inst.ble_Disconnect();
    }
    public void GetCurrentToys()
    {
        Debug.Log("Getting Current Toys...");
        BitToys.inst.FetchOwnedToys("User_ID_TomTest");
    }

        //EVENTS

        /*
         BitToys . inst . onFetchToyList_OK += OnGotToyList_Success;
    BitToys . inst . onFetchToyList_Fail += OnGotToyList_Fail;
    BitToys . inst . onClaimToy_OK += OnClaimToy_Success;
    BitToys . inst . onClaimToy_Fail += OnClaimToy_Fail;
    BitToys . inst . onGetToy_Fail += OnGetToy_Fail;
    BitToys . inst . onGetToy_OK += OnGetToy_Success;
    BitToys . inst . qr_onSawQR += OnSaw_QR;
    BitToys . inst . nfc_onSawTag += OnSaw_NFC;
    BitToys . inst . audiotag_onSawTag += OnSaw_AudioTag;
    BitToys . inst . audiotag_onTagGone += OnGone_AudioTag;
    BitToys . inst . ble_onSawTag += OnSaw_BLE;
    BitToys . inst . ble_onTagGone += OnGone_BLE;
    BitToys . inst . ble_onDeviceConnected += OnDeviceConnected;
    BitToys . inst . ble_onDeviceLost += OnDeviceLost;
    BitToys . inst . ble_onDeviceConnectFailed += OnDeviceConnectFailed;
    BitToys . inst . ble_onBatteryLow += OnBleBatteryLow;
    BitToys . inst . bittoys_Alert += OnAlert;
    BitToys . inst . onGetCustomData_OK += OnGetData_Success;
    BitToys . inst . onGetCustomData_Fail += OnGetData_Fail;
    BitToys . inst . onPutCustomData_OK += OnPutData_Success;
    BitToys . inst . onPutCustomData_Fail += OnPutData_Fail;
         */


    }
