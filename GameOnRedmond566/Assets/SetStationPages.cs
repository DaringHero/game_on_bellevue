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

    public void Start()
    {
        Location2ResourceDic = new Dictionary<YellOnClaim.Location, string>();
        Location2ResourceDic.Add(YellOnClaim.Location.FARM, "PINECONE");
        Location2ResourceDic.Add(YellOnClaim.Location.WIND, "FEATHER");
        Location2ResourceDic.Add(YellOnClaim.Location.LAKE, "ICE");
        Location2ResourceDic.Add(YellOnClaim.Location.FOREST, "WOOD");
        Location2ResourceDic.Add(YellOnClaim.Location.MOUNTAIN, "ROCK");
        Location2ResourceDic.Add(YellOnClaim.Location.SWAMP, "MUSHROOM");
    }

    public GameObject GetStationResource()
    {
        //TODO: get the resource associated with this station!!!

        return new GameObject();
    }

    public void UpdateAllCards(BitToys.Toy myToy)
    {
        foreach (GameObject card in myResourceCards)
        {
            card.GetComponent<StampCard>().SetStampCard(-1, "???");// TODO// call functions to update stampcards
        }
    }
    public void LevelUpCards()// special case for cards on level up
    {
        foreach (GameObject card in myLevelUpResourceCards)
        {

        }
    }


    public void SetAllStationResoruces(GameObject setto)// set all station resource sprites to specific resource
    {
        foreach (GameObject stationresource in myStationResources)
        {
            SpriteRenderer mySprite = stationresource.GetComponent<SpriteRenderer>();
            mySprite.sprite = setto.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void SetAllStationResoruces(BitToys.Toy myToy)
    {
        for (int i =0; i< this.myResourceCards.Count; i++) 
        {
           
            string temp = myToy.customData.GetString("CompletedQuests",i, "");

            if (temp != "")
            {
                StampCard myStampCard = this.myResourceCards[i].GetComponent<StampCard>();

                myStampCard.SetStampCard(i,temp);
            }
        }
    }
}
