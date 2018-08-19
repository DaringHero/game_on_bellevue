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

    public Dictionary<YellOnClaim.CLEVELAND_Locations, string> CLEVELAND_EnumLocation2Resource;
    public Dictionary<YellOnClaim.RTCEAST_Locations, string> RTCEAST_EnumLocation2Resource;
    public Dictionary<YellOnClaim.RTCWEST_Locations, string> RTCWEST_EnumLocation2Resource;
    public Dictionary<YellOnClaim.RANDOM_Locations, string> RANDOM_EnumLocation2Resource;

    public enum RESOURCE_ENUM
    {
        AMETHYST,
        APPLE,
        WOOD,
        BASKET,
        FEATHER,
        ICE,
        SALMON,
        REED,
        PINECONE,
        GRASS,
        SCALE
    };
    
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

        CLEVELAND_EnumLocation2Resource = new Dictionary<YellOnClaim.CLEVELAND_Locations, string>();
        RTCEAST_EnumLocation2Resource = new Dictionary<YellOnClaim.RTCEAST_Locations, string>();
        RTCWEST_EnumLocation2Resource = new Dictionary<YellOnClaim.RTCWEST_Locations, string>();
        RANDOM_EnumLocation2Resource = new Dictionary<YellOnClaim.RANDOM_Locations, string>();

        {
            CLEVELAND_EnumLocation2Resource.Add(YellOnClaim.CLEVELAND_Locations.ORCHARD, "APPLE");
            CLEVELAND_EnumLocation2Resource.Add(YellOnClaim.CLEVELAND_Locations.LUMBER, "WOOD");
        }

        {
            RTCEAST_EnumLocation2Resource.Add(YellOnClaim.RTCEAST_Locations.WIND, "FEATHER");
            RTCEAST_EnumLocation2Resource.Add(YellOnClaim.RTCEAST_Locations.MARKET, "BASKET");
        }

        {
            RTCWEST_EnumLocation2Resource.Add(YellOnClaim.RTCWEST_Locations.MOUNTAIN, "SNOW");
            RTCWEST_EnumLocation2Resource.Add(YellOnClaim.RTCWEST_Locations.LAKE, "SALMON");
        }

        {
            RANDOM_EnumLocation2Resource.Add(YellOnClaim.RANDOM_Locations.FARM, "GRASS");
            RANDOM_EnumLocation2Resource.Add(YellOnClaim.RANDOM_Locations.HUNTING, "SCALE");
            RANDOM_EnumLocation2Resource.Add(YellOnClaim.RANDOM_Locations.FOREST, "PINECONE");
            RANDOM_EnumLocation2Resource.Add(YellOnClaim.RANDOM_Locations.SWAMP, "REED");
        }





        Resource2Sprite = new Dictionary<string, Sprite>();

        int i = 0;
        foreach (GameObject a in resourceobjects)
        {
            Location2ResourceObject.Add((YellOnClaim.Location)i, a);
            i++;
        }

        Resource2ResourceObject.Add("AMETHYST", resourceobjects[0]);
        Resource2ResourceObject.Add("REED", resourceobjects[1]);
        Resource2ResourceObject.Add("SNOW", resourceobjects[2]);
        Resource2ResourceObject.Add("PINECONE", resourceobjects[3]);
        Resource2ResourceObject.Add("SALMON", resourceobjects[4]);
        Resource2ResourceObject.Add("FEATHER", resourceobjects[5]);
        Resource2ResourceObject.Add("GRASS", resourceobjects[6]);

        Resource2ResourceObject.Add("WOOD", resourceobjects[7]);
        Resource2ResourceObject.Add("APPLE", resourceobjects[8]);
        Resource2ResourceObject.Add("BASKET", resourceobjects[9]);
        Resource2ResourceObject.Add("SCALE", resourceobjects[10]);
        /*
		Resource2Sprite.Add("SCALE", 	Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/ScintScales"));
		Resource2Sprite.Add("MUSH", 	Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/MystMushroom"));
		Resource2Sprite.Add("ROCK", 	Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/RollingRocks"));
		Resource2Sprite.Add("WOOD", 	Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/SuperCedar"));
		Resource2Sprite.Add("ICE", 		Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/glacier_ice"));
		Resource2Sprite.Add("REED", 	Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/Reeds"));
		Resource2Sprite.Add("PINECONE", Resources.Load<Sprite>("Assets/Resources/ResourcePrefabs/Sprites/PricklyPinecone"));
		*/

        Resource2Sprite.Add("SCALE", 	Resources.Load<Sprite>("ResourcePrefabs/Sprites/ScintScales"));
		Resource2Sprite.Add("ROCK", 	Resources.Load<Sprite>("ResourcePrefabs/Sprites/RollingRocks"));
		Resource2Sprite.Add("WOOD", 	Resources.Load<Sprite>("ResourcePrefabs/Sprites/SuperCedar"));
		Resource2Sprite.Add("SNOW", 		Resources.Load<Sprite>("ResourcePrefabs/Sprites/Snow"));
		Resource2Sprite.Add("REED", 	Resources.Load<Sprite>("ResourcePrefabs/Sprites/Reeds"));

		Resource2Sprite.Add("APPLE", Resources.Load<Sprite>("ResourcePrefabs/Sprites/AppealingApple"));
        Resource2Sprite.Add("SALMON", Resources.Load<Sprite>("ResourcePrefabs/Sprites/slipperysalmon"));
        Resource2Sprite.Add("FEATHER", Resources.Load<Sprite>("ResourcePrefabs/Sprites/eagle_feather"));
        Resource2Sprite.Add("GRASS", Resources.Load<Sprite>("ResourcePrefabs/Sprites/braidedgrass"));
        Resource2Sprite.Add("AMETHYST", Resources.Load<Sprite>("ResourcePrefabs/Sprites/amazingamethyst"));
        Resource2Sprite.Add("BASKET", Resources.Load<Sprite>("ResourcePrefabs/Sprites/Basket"));
		Resource2Sprite.Add("PINECONE", Resources.Load<Sprite>("ResourcePrefabs/Sprites/PricklyPinecone"));//pinecones are important!


        Location2Resource.Add("FARM", "GRASS");
        Location2Resource.Add("WIND", "FEATHER");
        Location2Resource.Add("LAKE", "SALMON");
        Location2Resource.Add("FOREST", "PINECONE");
        Location2Resource.Add("SWAMP", "REED");
        Location2Resource.Add("MOUNTAIN", "SNOW");
        Location2Resource.Add("SANC", "AMETHYST");
        Location2Resource.Add("HUNTING", "SCALE");
        Location2Resource.Add("MARKET" , "BASKET");
        Location2Resource.Add("LUMBER" , "WOOD");
        Location2Resource.Add("ORCHARD", "APPLE");

        Resource2Location.Add("GRASS", "FARM");
        Resource2Location.Add("FEATHER", "WIND");
        Resource2Location.Add("SALMON", "LAKE");
        Resource2Location.Add("PINECONE", "FOREST");
        Resource2Location.Add("REED", "SWAMP");
        Resource2Location.Add("SNOW", "MOUNTAIN");
        Resource2Location.Add("AMETHYST", "SANC");
        Resource2Location.Add("SCALE", "HUNTING");
        Resource2Location.Add("BASKET", "MARKET");
        Resource2Location.Add("WOOD", "LUMBER");
        Resource2Location.Add("APPLE", "ORCHARD");


        //this is needed because we can give it an int, cast it to a location, and get the resource
        EnumLocation2Resource.Add(YellOnClaim.Location.FARM, "GRASS");
        EnumLocation2Resource.Add(YellOnClaim.Location.WIND, "FEATHER");
        EnumLocation2Resource.Add(YellOnClaim.Location.LAKE, "SALMON");
        EnumLocation2Resource.Add(YellOnClaim.Location.FOREST, "PINECONE");
        EnumLocation2Resource.Add(YellOnClaim.Location.SWAMP, "REED");
        EnumLocation2Resource.Add(YellOnClaim.Location.MOUNTAIN, "SNOW");
        EnumLocation2Resource.Add(YellOnClaim.Location.SANC, "AMETHYST");
        EnumLocation2Resource.Add(YellOnClaim.Location.HUNTING, "SCALE");
        EnumLocation2Resource.Add(YellOnClaim.Location.MARKET, "BASKET");
        EnumLocation2Resource.Add(YellOnClaim.Location.LUMBER, "WOOD");
        EnumLocation2Resource.Add(YellOnClaim.Location.ORCHARD, "APPLE");


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
