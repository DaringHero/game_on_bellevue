//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class CustomDataWrapper
//{
//  public abstract void SetBool(string name, bool data);
//  public abstract void SetString(string name, string data);
//  public abstract void SetFloat(string name, float data);
//  public abstract void SetInt(string name, int data);

//  public abstract bool GetBool(string name, bool defaultVal);
//  public abstract string GetString(string name, string defaultVal);
//  public abstract float GetFloat(string name, float defaultVal);
//  public abstract int GetInt(string name, int defaultVal);

//  public abstract void SetBool_Global(string name, bool data);
//  public abstract void SetString_Global(string name, string data);
//  public abstract void SetFloat_Global(string name, float data);
//  public abstract void SetInt_Global(string name, int data);

//  public abstract bool GetBool_Global(string name, bool defaultVal);
//  public abstract string GetString_Global(string name, string defaultVal);
//  public abstract float GetFloat_Global(string name, float defaultVal);
//  public abstract int GetInt_Global(string name, int defaultVal);

//  public abstract void SendAsync();
//  public abstract void FullClear();
//  public abstract void ServerFullClear();
//}

//public class LiteralCustomData : CustomDataWrapper
//{
//  public LiteralCustomData(BitToys.CustomData customData)
//  {
//    cData = customData;
//  }

//  public override void SetBool(string name, bool data)
//  {
//    cData.SetBool(name, data);
//  }

//  public override void SetString(string name, string data)
//  {
//    cData.SetString(name, data);
//  }

//  public override void SetFloat(string name, float data)
//  {
//    cData.SetFloat(name, data);
//  }

//  public override void SetInt(string name, int data)
//  {
//    cData.SetInt(name, data);
//  }

//  public override bool GetBool(string name, bool defaultVal)
//  {
//    return cData.GetBool(name, defaultVal);
//  }

//  public override string GetString(string name, string defaultVal)
//  {
//    return cData.GetString(name, defaultVal);
//  }

//  public override float GetFloat(string name, float defaultVal)
//  {
//    return cData.GetFloat(name, defaultVal);
//  }

//  public override int GetInt(string name, int defaultVal)
//  {
//    return cData.GetInt(name, defaultVal);
//  }

//  public override void SetBool_Global(string name, bool data)
//  {
//    cData.SetBool_Global(name, data);
//  }

//  public override void SetString_Global(string name, string data)
//  {
//    cData.SetString_Global(name, data);
//  }

//  public override void SetFloat_Global(string name, float data)
//  {
//    cData.SetFloat_Global(name, data);
//  }

//  public override void SetInt_Global(string name, int data)
//  {
//    cData.SetInt_Global(name, data);
//  }

//  public override bool GetBool_Global(string name, bool defaultVal)
//  {
//    return cData.GetBool_Global(name, defaultVal);
//  }

//  public override string GetString_Global(string name, string defaultVal)
//  {
//    return cData.GetString_Global(name, defaultVal);
//  }

//  public override float GetFloat_Global(string name, float defaultVal)
//  {
//    return cData.GetFloat_Global(name, defaultVal);
//  }

//  public override int GetInt_Global(string name, int defaultVal)
//  {
//    return cData.GetInt_Global(name, defaultVal);
//  }

//  public override void SendAsync()
//  {
//    cData.SendAsync();
//  }

//  public override void FullClear()
//  {
//    cData.ClearAll_Local();
//  }

//  public override void ServerFullClear()
//  {
//    cData.ClearAll_Global();
//  }

//  private BitToys.CustomData cData;
//}

//public class EmulatedDatabase
//{
//  public Dictionary<string, int> privateDataDatabaseInts = new Dictionary<string, int>();
//  public Dictionary<string, bool> privateDataDatabaseBools = new Dictionary<string, bool>();
//  public Dictionary<string, float> privateDataDatabaseFloats = new Dictionary<string, float>();
//  public Dictionary<string, string> privateDataDatabaseStrings = new Dictionary<string, string>();

//  public Dictionary<string, int> globalDataDatabaseInts = new Dictionary<string, int>();
//  public Dictionary<string, bool> globalDataDatabaseBools = new Dictionary<string, bool>();
//  public Dictionary<string, float> globalDataDatabaseFloats = new Dictionary<string, float>();
//  public Dictionary<string, string> globalDataDatabaseStrings = new Dictionary<string, string>();
//}

//public class EmulatedCustomData : CustomDataWrapper
//{
//  public EmulatedCustomData(string databaseID)
//  {
//    id = databaseID;
//  }

//  private string id;

//  public static EmulatedDatabase db = new EmulatedDatabase();

//  public override void SetBool(string name, bool data)
//  {
//    string index = id + ":" + name;
//    db.privateDataDatabaseBools[index] = data;
//  }

//  public override void SetString(string name, string data)
//  {
//    string index = id + ":" + name;
//    db.privateDataDatabaseStrings[index] = data;
//  }

//  public override void SetFloat(string name, float data)
//  {
//    string index = id + ":" + name;
//    db.privateDataDatabaseFloats[index] = data;
//  }

//  public override void SetInt(string name, int data)
//  {
//    string index = id + ":" + name;
//    db.privateDataDatabaseInts[index] = data;
//  }

//  public override bool GetBool(string name, bool defaultVal)
//  {
//    string index = id + ":" + name;
//    if (db.privateDataDatabaseBools.ContainsKey(index) == false)
//    {
//      return defaultVal;
//    }

//    return db.privateDataDatabaseBools[index];
//  }

//  public override string GetString(string name, string defaultVal)
//  {
//    string index = id + ":" + name;
//    if (db.privateDataDatabaseStrings.ContainsKey(index) == false)
//    {
//      return defaultVal;
//    }

//    return db.privateDataDatabaseStrings[index];
//  }

//  public override float GetFloat(string name, float defaultVal)
//  {
//    string index = id + ":" + name;
//    if (db.privateDataDatabaseFloats.ContainsKey(index) == false)
//    {
//      return defaultVal;
//    }

//    return db.privateDataDatabaseFloats[index];
//  }

//  public override int GetInt(string name, int defaultVal)
//  {
//    string index = id + ":" + name;
//    if (db.privateDataDatabaseInts.ContainsKey(index) == false)
//    {
//      return defaultVal;
//    }

//    return db.privateDataDatabaseInts[index];
//  }

//  public override void SetBool_Global(string name, bool data)
//  {
//    db.globalDataDatabaseBools[name] = data;
//  }

//  public override void SetString_Global(string name, string data)
//  {
//    db.globalDataDatabaseStrings[name] = data;
//  }

//  public override void SetFloat_Global(string name, float data)
//  {
//    db.globalDataDatabaseFloats[name] = data;
//  }

//  public override void SetInt_Global(string name, int data)
//  {
//    db.globalDataDatabaseInts[name] = data;
//  }

//  public override bool GetBool_Global(string name, bool defaultVal)
//  {
//    if(db.globalDataDatabaseBools.ContainsKey(name) == false)
//    {
//      return defaultVal;
//    }

//    return db.globalDataDatabaseBools[name];
//  }

//  public override string GetString_Global(string name, string defaultVal)
//  {
//    if (db.globalDataDatabaseStrings.ContainsKey(name) == false)
//    {
//      return defaultVal;
//    }

//    return db.globalDataDatabaseStrings[name];
//  }

//  public override float GetFloat_Global(string name, float defaultVal)
//  {
//    if (db.globalDataDatabaseFloats.ContainsKey(name) == false)
//    {
//      return defaultVal;
//    }

//    return db.globalDataDatabaseFloats[name];
//  }

//  public override int GetInt_Global(string name, int defaultVal)
//  {
//    if (db.globalDataDatabaseInts.ContainsKey(name) == false)
//    {
//      return defaultVal;
//    }

//    return db.globalDataDatabaseInts[name];
//  }

//  public override void SendAsync()
//  {
//  }

//  public override void FullClear()
//  {
//  }

//  public override void ServerFullClear()
//  {
//  }
//}

//public class ToyWrapper
//{
//  public static ToyWrapper EmulateToy(string bitToysId, string styleId, string ownerId, string skuId)
//  {
//    ToyWrapper tw = new ToyWrapper();

//    tw.bitToysId = bitToysId;
//    tw.styleId = styleId;
//    tw.ownerId = ownerId;
//    tw.skuId = skuId;
//    tw.customData = new EmulatedCustomData(bitToysId);

//    return tw;
//  }

//  public static ToyWrapper FromToy(BitToys.Toy toy)
//  {
//    ToyWrapper tw = new ToyWrapper();

//    tw.bitToysId = toy.bitToysId;
//    tw.styleId = toy.styleId;
//    tw.ownerId = toy.ownerId;
//    tw.skuId = toy.skuId;
//    tw.customData = new LiteralCustomData(toy.customData);

//    return tw;
//  }

//  protected ToyWrapper() { }

//  public string bitToysId;
//  public string styleId;
//  public string ownerId;
//  public string skuId;
//  public CustomDataWrapper customData;
//}

//// Allows for EmulatedToy properties
//[System.Serializable]
//public class EmulatedToy : ToyWrapper, ISerializationCallbackReceiver
//{
//  public EmulatedToy()
//  {
//  }
  
//  public void OnAfterDeserialize()
//  {
//    // Gets called twice, dunno why.
//    customData = new EmulatedCustomData(bitToysId);
//  }

//  public void OnBeforeSerialize()
//  {
//  }
//}

//public abstract class BitToysWrapper
//{
//  public delegate void OnClaimToy(ToyWrapper toy, bool TLL_val);
//  public delegate void OnDetectToy(string text);
//    public delegate void OnFailToy(BitToys.FailReason reason, string texto);

//  public abstract void registerOnClaimToy(OnClaimToy del);
//  public abstract void registerOnFailToy(OnFailToy del);
//  public abstract void registerOnDetectToy(OnDetectToy del);

//  public static BitToysWrapper inst;
//}

//public class LiteralBitToys : BitToysWrapper
//{
//  public event OnClaimToy OnClaimToy_OK;
//  public event OnFailToy OnClaimToy_Fail;
//  public event OnDetectToy OnDetectToy;

//  public LiteralBitToys()
//  {
//    Debug.Log("Initilizing Bittoy Wrapper");
//    BitToys.inst.onClaimToy_OK += OnClaimToy_OKWrapper;
//    BitToys.inst.onClaimToy_Fail += OnClaimToy_FailWrapper;
//    BitToys.inst.ble_onSawTag += onDetect;
//    BitToys.inst.nfc_onSawTag += onDetect;
//  }

//  private void onDetect()
//  {
//    onDetect("");
//  }

//  private void onDetect(string text)
//  {
//    OnDetectToy(text);
//  }

//  private void OnClaimToy_OKWrapper(BitToys.Toy toy, bool TLL_val)
//  {
//    ToyWrapper tw = ToyWrapper.FromToy(toy);
//    OnClaimToy_OK(tw, TLL_val);
//  }

//  private void OnClaimToy_FailWrapper(BitToys.FailReason reason, string text)
//  {
//    Debug.Log("Failed to claim toy: " + reason);
//    Debug.Log(text);
//  }

//  public override void registerOnClaimToy(OnClaimToy del)
//  {
//    OnClaimToy_OK += del;
//  }

//  public override void registerOnDetectToy(OnDetectToy del)
//  {
//    OnDetectToy += del;
//  }

//    public override void registerOnFailToy(OnFailToy del)
//    {
//        OnClaimToy_Fail += del;
//    }
//}

//public class EmulateBitToys : BitToysWrapper
//{
//  public event OnClaimToy OnClaimToy_OK;
//  public event OnDetectToy OnDetectToy_OK;
//    public event OnFailToy OnClaimToy_Fail;

//  public override void registerOnClaimToy(OnClaimToy del)
//  {
//    OnClaimToy_OK += del;
//  }

//  public void emulateToyClaim(ToyWrapper toy, bool TLL_val)
//  {
//    OnClaimToy_OK(toy, TLL_val);
//  }

//  public override void registerOnDetectToy(OnDetectToy del)
//  {
//    OnDetectToy_OK += del;
//  }

//  public void emulateDetectToy(string text)
//  {
//    OnDetectToy_OK(text);
//  }

//    public override void registerOnFailToy(OnFailToy del)
//    {
//        OnClaimToy_Fail += del;  
//    }
//}