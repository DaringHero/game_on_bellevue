//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public class Location
//{
//  public string areaName;
//  public string specificName;
//  public Color frameColor;
//  public Sprite background;
//  public Item uniqueItem;
//  public int[] questIDs;
//}

//public class LocationManager : MonoBehaviour
//{
//  public Location[] location;

//  private static LocationManager inst;

//  public static Location[] Locations
//  {
//    get
//    {
//      return inst.location;
//    }
//  }

//  void Awake()
//  {
//    inst = this;
//    DontDestroyOnLoad(gameObject);
//  }

//  /* =
//  {
//    new Location("Symetra Marsh", "Secluded Fen", new Color(204.0f / 255.0f, 255.0f / 255.0f, 111.0f / 255.0f))
//  };*/
//}
