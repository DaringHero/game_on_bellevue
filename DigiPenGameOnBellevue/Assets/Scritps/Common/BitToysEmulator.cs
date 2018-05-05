/*
 * Script responsible for emulating the BitTyos and BitToysManager component 
 * stack.
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class BitToysEmulator : MonoBehaviour
{
  public EmulatedToy[] toys;

  void Start()
  {
    // Load databases
    LoadDatabases();

    // Spawns buttons to emulate toy claiming
    BitToysWrapper.inst = new EmulateBitToys();

    int counter = 0;
    GameObject canvas = GameObject.FindGameObjectWithTag("canvas");
    DontDestroyOnLoad(canvas);

    foreach(EmulatedToy toy in toys)
    {
      GameObject inst = (GameObject)Instantiate(Resources.Load("ToyButton"), new Vector3(100.0f + counter * 100, 50.0f, 0.0f), new Quaternion(), canvas.transform);
      inst.GetComponent<OnClaimEmulator>().toy = toy;
      DontDestroyOnLoad(inst);

      ++counter;
    }
  }

  void OnDestroy()
  {

    // Save databases
    SaveDatabases();
  }

  void LoadDatabases()
  {
    Debug.Log("Emulated Data Location: " + Application.persistentDataPath);
    EmulatedCustomData.db.globalDataDatabaseInts = LoadIntDatabase(GetFileAsString("globalDataInts.xml"));
    EmulatedCustomData.db.globalDataDatabaseBools = LoadBoolDatabase(GetFileAsString("globalDataBools.xml"));
    EmulatedCustomData.db.globalDataDatabaseFloats = LoadFloatDatabase(GetFileAsString("globalDataFloats.xml"));
    EmulatedCustomData.db.globalDataDatabaseStrings = LoadStringDatabase(GetFileAsString("globalDataStrings.xml"));

    EmulatedCustomData.db.privateDataDatabaseInts = LoadIntDatabase(GetFileAsString("privateDataInts.xml"));
    EmulatedCustomData.db.privateDataDatabaseBools = LoadBoolDatabase(GetFileAsString("privateDataBools.xml"));
    EmulatedCustomData.db.privateDataDatabaseFloats = LoadFloatDatabase(GetFileAsString("privateDataFloats.xml"));
    EmulatedCustomData.db.privateDataDatabaseStrings = LoadStringDatabase(GetFileAsString("privateDataStrings.xml"));
  }

  void SaveDatabases()
  {
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.globalDataDatabaseInts), "globalDataInts.xml");
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.globalDataDatabaseBools), "globalDataBools.xml");
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.globalDataDatabaseFloats), "globalDataFloats.xml");
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.globalDataDatabaseStrings), "globalDataStrings.xml");

    SetFileFromString(SaveDatabase(EmulatedCustomData.db.privateDataDatabaseInts), "privateDataInts.xml");
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.privateDataDatabaseBools), "privateDataBools.xml");
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.privateDataDatabaseFloats), "privateDataFloats.xml");
    SetFileFromString(SaveDatabase(EmulatedCustomData.db.privateDataDatabaseStrings), "privateDataStrings.xml");
  }

  public class IntItem
  {
    [XmlAttribute]
    public string Key;

    [XmlAttribute]
    public int Value;
  }

  public class BoolItem
  {
    [XmlAttribute]
    public string Key;

    [XmlAttribute]
    public bool Value;
  }

  public class FloatItem
  {
    [XmlAttribute]
    public string Key;

    [XmlAttribute]
    public float Value;
  }

  public class StringItem
  {
    [XmlAttribute]
    public string Key;

    [XmlAttribute]
    public string Value;
  }

  string SaveDatabase(Dictionary<string, int> dict)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(IntItem[]), new XmlRootAttribute() { ElementName = "items" });
    StringWriter sw = new StringWriter();
    serializer.Serialize(sw, dict.Select(kv => new IntItem() { Key = kv.Key, Value = kv.Value }).ToArray());

    return sw.ToString();
  }

  string SaveDatabase(Dictionary<string, bool> dict)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(BoolItem[]), new XmlRootAttribute() { ElementName = "items" });
    StringWriter sw = new StringWriter();
    serializer.Serialize(sw, dict.Select(kv => new BoolItem() { Key = kv.Key, Value = kv.Value }).ToArray());

    return sw.ToString();
  }

  string SaveDatabase(Dictionary<string, float> dict)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(FloatItem[]), new XmlRootAttribute() { ElementName = "items" });
    StringWriter sw = new StringWriter();
    serializer.Serialize(sw, dict.Select(kv => new FloatItem() { Key = kv.Key, Value = kv.Value }).ToArray());

    return sw.ToString();
  }

  string SaveDatabase(Dictionary<string, string> dict)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(StringItem[]), new XmlRootAttribute() { ElementName = "items" });
    StringWriter sw = new StringWriter();
    serializer.Serialize(sw, dict.Select(kv => new StringItem() { Key = kv.Key, Value = kv.Value }).ToArray());

    return sw.ToString();
  }

  Dictionary<string, int> LoadIntDatabase(string data)
  {
    try
    {
      XmlSerializer serializer = new XmlSerializer(typeof(IntItem[]), new XmlRootAttribute() { ElementName = "items" });
      StringReader sr = new StringReader(data);
      return ((IntItem[])serializer.Deserialize(sr)).ToDictionary(i => i.Key, i => i.Value);
    }
    catch (FileNotFoundException ex)
    {
      return new Dictionary<string, int>();
    }
  }

  Dictionary<string, bool> LoadBoolDatabase(string data)
  {
    try
    {
      XmlSerializer serializer = new XmlSerializer(typeof(BoolItem[]), new XmlRootAttribute() { ElementName = "items" });
      StringReader sr = new StringReader(data);
      return ((BoolItem[])serializer.Deserialize(sr)).ToDictionary(i => i.Key, i => i.Value);
    }
    catch (FileNotFoundException ex)
    {
      return new Dictionary<string, bool>();
    }
  }

  Dictionary<string, float> LoadFloatDatabase(string data)
  {
    try
    {
      XmlSerializer serializer = new XmlSerializer(typeof(FloatItem[]), new XmlRootAttribute() { ElementName = "items" });
      StringReader sr = new StringReader(data);
      return ((FloatItem[])serializer.Deserialize(sr)).ToDictionary(i => i.Key, i => i.Value);
    }
    catch (FileNotFoundException ex)
    {
      return new Dictionary<string, float>();
    }
  }

  Dictionary<string, string> LoadStringDatabase(string data)
  {
    try
    {
      XmlSerializer serializer = new XmlSerializer(typeof(StringItem[]), new XmlRootAttribute() { ElementName = "items" });
      StringReader sr = new StringReader(data);
      return ((StringItem[])serializer.Deserialize(sr)).ToDictionary(i => i.Key, i => i.Value);
    }
    catch (FileNotFoundException ex)
    {
      return new Dictionary<string, string>();
    }
  }

  string GetFileAsString(string path)
  {
    return System.IO.File.ReadAllText(Application.persistentDataPath + "/" + path);
  }

  void SetFileFromString(string data, string path)
  {
    System.IO.File.WriteAllText(Application.persistentDataPath + "/" + path, data);
  }
}
