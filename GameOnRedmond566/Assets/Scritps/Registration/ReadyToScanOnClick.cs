//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
//public class ReadyToScanOnClick : MonoBehaviour
//{
//  private Button btn;
//  private Text text;
//  private bool ready = false;
//  private bool clearChip = false;
//  private bool fullClearChip = false;
//  private bool fullClearServer = false;

//  public Dropdown affinitySelection;
//  public InputField userField;
//  public Text userFeedback;

//  // Use this for initialization
//  void Start ()
//  {
//    btn = GetComponent<Button>();
//    text = GetComponentInChildren<Text>();
//    btn.onClick.AddListener(OnButtonClick);

//    BitToysWrapper.inst.registerOnClaimToy(OnClaimToy);
//        BitToysWrapper.inst.registerOnFailToy(OnFailToy);
//  }

//  void Update()
//  {

//        // Method: SetLED_Connected(Red, Green, Blue, Red Pulse, Green Pulse, Blue Pulse).

//        // When device is connected and idle, the light is solid bright blue.
//        BitToys.inst.ble_SetLED_Connected(0, 0, 255, 0, 0, 0);

//        // When a tag is detected and being read, change the light to a bright pulsing green

//        BitToys.inst.ble_SetLED_nfcTagDetected(0, 255, 0, 0, 15, 0);

//        // If a tag fails to read properly, change it to bright pulsing Red.

//        BitToys.inst.ble_SetLED_nfcTagError(255, 0, 0, 15, 0, 0);

//        // If a tag is read correctly, change the light to bright solid green.

//        BitToys.inst.ble_SetLED_nfcTagOK(0, 255, 0, 0, 0, 0);


//        if (Input.GetKeyDown(KeyCode.Escape))
//    {
//      Application.LoadLevel(3);
//    }
//  }

//  void UnlockUI()
//  {
//    affinitySelection.interactable = true;
//    userField.interactable = true;

//    text.text = "Ready To Scan!";
//    ready = false;
//    userFeedback.text = "";
//  }

//  void LockUI()
//  {
//    affinitySelection.interactable = false;
//    userField.interactable = false;

//    text.text = "Cancel";
//    ready = true;
//    userFeedback.text = "Scan your chip!";
//  }

//  void OnButtonClick()
//  {
//    if (userField.text == "_adminBoot")
//    {
//      Application.LoadLevel(0);
//      return;
//    }

//    if (ready == false)
//    {
//      // Input validation
//      if (userField.text == "_adminClearRegistration")
//      {
//        clearChip = true;
//      }
//      else if(userField.text == "_adminClearChip")
//      {
//        fullClearChip = true;
//      }
//      else if(userField.text == "_adminTacticalNuke")
//      {
//        fullClearServer = true;
//      }

//      LockUI();
//    }
//    else
//    {
//      UnlockUI();
//    }
//  }

// void OnFailToy(BitToys.FailReason reason, string texto)
//    {
//        userFeedback.text = texto;
//        text.text = reason.ToString();

//    }

//  void OnClaimToy(ToyWrapper toy, bool TLL)
//  {
//    if (clearChip)
//    {
//      clearChip = false;
//      toy.customData.SetBool("registered", false);
//      toy.customData.SendAsync();

//      UnlockUI();
//    }
//    else if(fullClearChip)
//    {
//      // fullClearChip = false;
//      toy.customData.FullClear();
//      toy.customData.SendAsync();
//    }
//    else if(fullClearServer)
//    {
//      fullClearServer = false;
//      toy.customData.ServerFullClear();

//      UnlockUI();
//    }
//    else
//    {
//      if (toy.customData.GetBool("registered", false) == true)
//      {
//        string username = toy.customData.GetString("username", "");

//        userFeedback.text = "Token is already registered to " + username + ".";
//      }
//      else if (ready == false)
//      {
//        userFeedback.text = "Please click okay button first.";
//      }
//      else
//      {
//        Debug.Log((ShardColor)affinitySelection.value + 1);
//        Player.RegisterPlayer(toy, userField.text, 1, (ShardColor) affinitySelection.value + 1);

//        UnlockUI();
//      }
//    }
//  }
//}
