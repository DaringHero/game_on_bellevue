//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
//public class OnClaimEmulator : MonoBehaviour
//{
//  public ToyWrapper toy;
//  public Button button;

//  void Start()
//  {
//    Button btn = GetComponent<Button>();
//    btn.onClick.AddListener(OnMouseDown);
//  }

//  void OnMouseDown()
//  {
//    // ((EmulateBitToys)BitToysWrapper.inst).emulateDetectToy("AAAA");
//    ((EmulateBitToys)BitToysWrapper.inst).emulateToyClaim(toy, false);
//  }
//}
