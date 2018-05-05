/*
 * Class responsible for managing bit toys plugin.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BitToys))]
public class BitToysManager : MonoBehaviour
{
  private long simpleHash(string input)
  {
    // x65599: http://www.cse.yorku.ca/~oz/hash.html
    long hash = 5381;

    foreach(char i in input)
    {
      hash = ((hash << 5) + hash) + i;
    }

    return hash;
  }

  // Use this for initialization
  void Awake ()
  {
    BitToys.inst.Init("digipenGameOnBellevueKiosk");
    if (BitToys.inst.ble_IsBluetoothEnabled())
      BitToys.inst.ble_QuickConnectReader();

    BitToysWrapper.inst = new LiteralBitToys();
  }

  void OnDestroy()
  {
    if (BitToys.inst.ble_IsBluetoothEnabled())
      BitToys.inst.ble_Disconnect();
  }

  // Update is called once per frame
  void Update ()
  {
    
  }
}
