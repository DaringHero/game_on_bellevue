using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStationPages : MonoBehaviour {

    public List<GameObject> myStationResources;
    public List<GameObject> myResourceCards;
    public List<GameObject> myProgressBars;//progress bar
    public List<GameObject> myDragons;// dragon

 

    public List<GameObject> myLevelUpResourceCards;// corner case for levelup

    public Dictionary<YellOnClaim.Location, string> Location2ResourceDic;

    public enum Resource {

        WOOD,
        MUSH,
    REED,
    ROCK,
    PINECONE,
    ICE,
    };

    public void Start()
    {
        Location2ResourceDic = new Dictionary<YellOnClaim.Location, string>();
        Location2ResourceDic.Add(YellOnClaim.Location.FARM, "GRASS");
        Location2ResourceDic.Add(YellOnClaim.Location.WIND, "FEATHER");
        Location2ResourceDic.Add(YellOnClaim.Location.LAKE, "SALMON");
        Location2ResourceDic.Add(YellOnClaim.Location.FOREST, "PINECONE");
        Location2ResourceDic.Add(YellOnClaim.Location.MOUNTAIN, "ICE");
		Location2ResourceDic.Add(YellOnClaim.Location.SWAMP, "REED");
        Location2ResourceDic.Add(YellOnClaim.Location.HUNTING, "SCALE");
        Location2ResourceDic.Add(YellOnClaim.Location.SANC, "AMETHYST");
        Location2ResourceDic.Add(YellOnClaim.Location.ORCHARD, "APPLE");
        Location2ResourceDic.Add(YellOnClaim.Location.MARKET, "BASKET");
        Location2ResourceDic.Add(YellOnClaim.Location.LUMBER, "WOOD");

    }

	public List<KeyValuePair<string, int>> GetQuestData()
	{
		List<KeyValuePair<string, int>> ret = new List<KeyValuePair<string, int>>();

		YellOnClaim myYellOnClaim = this.gameObject.GetComponent<YellOnClaim>();
		ret.Add( new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.FARM], 		myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.FARM],-1)));
		ret.Add( new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.WIND], 		myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.WIND],-1)));
		ret.Add( new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.LAKE],	 		myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.LAKE],-1)));	
		ret.Add( new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.FOREST], 		myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.FOREST],-1)));
		ret.Add( new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.MOUNTAIN], 	myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.MOUNTAIN],-1)));	
		ret.Add( new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.SWAMP], 		myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.SWAMP],-1)));

        ret.Add(new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.ORCHARD], myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.ORCHARD], -1)));
        ret.Add(new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.HUNTING], myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.HUNTING], -1)));
        ret.Add(new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.MARKET], myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.MARKET], -1)));
        ret.Add(new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.LUMBER], myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.LUMBER], -1)));

        ret.Add(new KeyValuePair<string, int>(Location2ResourceDic[YellOnClaim.Location.SANC], myYellOnClaim.MyCurrentToy.customData.GetInt(Location2ResourceDic[YellOnClaim.Location.SANC], -1)));
        return ret;

	}

    public GameObject GetStationResource()
    {
        // get the resource associated with this station!!!
        YellOnClaim myYellOnClaim = this.gameObject.GetComponent<YellOnClaim>();
        DictionariesForThings d = this.gameObject.GetComponent<DictionariesForThings>();


        return d.GetResourceFromLocation(myYellOnClaim.currentLocation);
    }

	public void TunOffAllStampsOnAllCards()
	{
		foreach (GameObject card in myResourceCards)
		{
			StampCard myStampcard = card.GetComponent<StampCard>();
			myStampcard.TurnOffAllStamps();
		}

	}

    public void UpdateAllCards(BitToys.Toy myToy)//TODO
    {
		List<KeyValuePair<string, int>> questData = this.GetQuestData();

        foreach (GameObject card in myResourceCards)
        {
			int d = 0;
            //update each stamp on each card
            for (int i = 0; i < 8; i++)
            {
				StampCard myStampcard = card.GetComponent<StampCard>();

				if(questData[d].Value > -1)
				{
					//display correct resource
					myStampcard.SetCompletedStamp(d, questData[d].Key);

					//display stamp if obtained

					d++;
				}
				else
				{
					d++;
				}
            }
        }
    }
    public void LevelUpCards()// special case for cards on level up
    {
		YellOnClaim myYellOnClaim = this.gameObject.GetComponent<YellOnClaim>();

        foreach (GameObject card in myLevelUpResourceCards)
        {
			StampCard myStampCard = card.GetComponent<StampCard>();


        }
    }


	public void SetAllStationResoruces(Sprite setto)// set all station resource sprites to specific sprite
    {
        foreach (GameObject stationresource in myStationResources)
        {
            SpriteRenderer mySprite = stationresource.GetComponent<SpriteRenderer>();
			mySprite.sprite = setto;
        }
    }

    public void SetAllStationResoruces2(BitToys.Toy myToy)//unused?
    {
        for (int i =0; i< this.myResourceCards.Count; i++) 
        {
           
            string temp = myToy.customData.GetString("CompletedQuests",i, "");

            if (temp != "")
            {
                StampCard myStampCard = this.myResourceCards[i].GetComponent<StampCard>();

                //update each stamp on each card
                for (int j = 0; j < 8; j++)
                {
                    myStampCard.GetComponent<StampCard>().SetCompletedStamp(i, "temp");
                }
            }
        }
    }
}
