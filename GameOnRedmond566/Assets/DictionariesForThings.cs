using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionariesForThings : MonoBehaviour {

    public Dictionary<string, string> Resource2Location;
    public Dictionary<string, string> Location2Resource;
    public Dictionary<YellOnClaim.Location, string> EnumLocation2Resource;
    public Dictionary<YellOnClaim.Location, GameObject> Location2ResourceObject;
    public Dictionary<string, GameObject> Resource2ResourceObject;
	public Dictionary<string, Sprite> Resource2Sprite;//

    public GameObject[] resourceobjects;

    public GameObject GetResourceFromLocation(YellOnClaim.Location loc)
    {
        if (Location2ResourceObject.ContainsKey(loc))
            return Location2ResourceObject[loc];
        else
            return null;
    }

    // Use this for initialization
    void Start() {
        Resource2Location = new Dictionary<string, string>();
        Location2Resource = new Dictionary<string, string>();
        EnumLocation2Resource = new Dictionary<YellOnClaim.Location, string>();
        Location2ResourceObject = new Dictionary<YellOnClaim.Location, GameObject>();
        Resource2ResourceObject = new Dictionary<string, GameObject>();

		Resource2Sprite = new Dictionary<string, Sprite>();

        int i = 0;
        foreach (GameObject a in resourceobjects)
        {
            Location2ResourceObject.Add((YellOnClaim.Location)i, a);
            i++;
        }

        Resource2ResourceObject.Add("SCALE", resourceobjects[0]);
        Resource2ResourceObject.Add("MUSH", resourceobjects[1]);
        Resource2ResourceObject.Add("ROCK", resourceobjects[2]);
        Resource2ResourceObject.Add("WOOD", resourceobjects[3]);
        Resource2ResourceObject.Add("ICE", resourceobjects[4]);
        Resource2ResourceObject.Add("REED", resourceobjects[5]);
        Resource2ResourceObject.Add("PINECONE", resourceobjects[6]);
 
		Resource2Sprite.Add("SCALE", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/ScintScales"));
		Resource2Sprite.Add("MUSH", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/MystMushroom"));
		Resource2Sprite.Add("ROCK", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/RollingRocks"));
		Resource2Sprite.Add("WOOD", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/SuperCedar"));
		Resource2Sprite.Add("ICE", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/glacier_ice"));
		Resource2Sprite.Add("REED", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/Reeds"));
		Resource2Sprite.Add("PINECONE", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/PricklyPinecone"));


        Resource2Location.Add("PINECONE", "FARM");
        Resource2Location.Add("REED", "WIND");
        Resource2Location.Add("ICE", "LAKE");
        Resource2Location.Add("WOOD", "FOREST");
        Resource2Location.Add("MUSH", "SWAMP");
        Resource2Location.Add("ROCK", "MOUNTAIN");
        Resource2Location.Add("SCALE", "SANC");

        Location2Resource.Add("FARM", "PINECONE");
        Location2Resource.Add("WIND", "REED");
        Location2Resource.Add("LAKE", "ICE");
        Location2Resource.Add("FOREST", "WOOD");
        Location2Resource.Add("SWAMP", "MUSH");
        Location2Resource.Add("MOUNTAIN", "ROCK");
        Location2Resource.Add("SANC", "SCALE");

        //this is needed because we can give it an int, cast it to a location, and get the resource
        EnumLocation2Resource.Add(YellOnClaim.Location.FARM, "PINECONE");
        EnumLocation2Resource.Add(YellOnClaim.Location.WIND, "REED");
        EnumLocation2Resource.Add(YellOnClaim.Location.LAKE, "ICE");
        EnumLocation2Resource.Add(YellOnClaim.Location.FOREST, "WOOD");
        EnumLocation2Resource.Add(YellOnClaim.Location.SWAMP, "MUSH");
        EnumLocation2Resource.Add(YellOnClaim.Location.MOUNTAIN, "ROCK");
        EnumLocation2Resource.Add(YellOnClaim.Location.SANC, "SCALE");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
