using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampCard : MonoBehaviour {

    public GameObject resource_1;
    public GameObject stampBack1;
    public GameObject complete_1;

    public GameObject resource_2;
    public GameObject stampBack2;
    public GameObject complete_2;

    public GameObject resource_3;
    public GameObject stampBack3;
    public GameObject complete_3;

    public GameObject resource_4;
    public GameObject stampBack4;
    public GameObject complete_4;

    public GameObject resource_5;
    public GameObject stampBack5;
    public GameObject complete_5;

    public GameObject resource_6;
    public GameObject stampBack6;
    public GameObject complete_6;

    public GameObject resource_7;
    public GameObject stampBack7;
    public GameObject complete_7;

    public GameObject resource_8;
    public GameObject stampBack8;
    public GameObject complete_8;

    public YellOnClaim myYellOnClaim;

	public void SetCompletedStamp(int i, string resource)//Sets a completed resoruce stamp
    {
		
		this.SetStampBasedOnResource(i, resource);

		//show completed resource
		List<GameObject> Stamp = GetStamp(i);
		Stamp[2].SetActive(true);//is complete*/

    }

    public void SetStampBasedOnLocation(int stampindex, YellOnClaim.Location loc)//old?
    {
		List<GameObject> Stamp = GetStamp(stampindex);
		Stamp[1].SetActive(true);
		Stamp[0].GetComponent<SpriteRenderer>().sprite = myYellOnClaim.gameObject.GetComponent<DictionariesForThings>().Location2ResourceObject[loc].GetComponent<SpriteRenderer>().sprite;
		Stamp[0].SetActive(true);
        //might have to instantiate

    }

    public void SetStampBasedOnResource(int stampindex, string resource)// sets uncompleted resource
    {
        List<GameObject> Stamp = GetStamp(stampindex);
		Stamp[1].SetActive(true);
		//Stamp[0].SetActive(true);
		//GameObject old = Stamp[0];
		//Stamp[0].GetComponent<SpriteRenderer>().sprite = myYellOnClaim.gameObject.GetComponent<DictionariesForThings>().Resource2ResourceObject[resource].GetComponent<SpriteRenderer>().sprite;
		Debug.Log("resource = "+resource);
		Sprite temp = myYellOnClaim.gameObject.GetComponent<DictionariesForThings>().Resource2Sprite[resource];
		//Debug.Log("Get Sprite = "+temp.name);
		Stamp[0].GetComponent<SpriteRenderer>().sprite = temp;

		//Destroy(old);
		Stamp[0].SetActive(true);
        //might have to instantiate
    }

	public void TurnOffAllStamps()
	{

		for (int i = 0; i<8; i++)
		{

			List<GameObject> temp = this.GetStamp(i);

			foreach(GameObject go in temp)
			{
				go.SetActive(false);
			}
		}
	}


    public List<GameObject> GetStamp(int i)// gets all parts for a stamp 1-8
    {
		i++;// I started at 1 instead of 0

        List<GameObject> ret = new List<GameObject>();

        switch (i)
        {
            case 1:
                ret.Add(this.resource_1);
                ret.Add(this.stampBack1);
                ret.Add(this.complete_1);
                break;
            case 2:
                ret.Add(this.resource_2);
                ret.Add(this.stampBack2);
                ret.Add(this.complete_2);
                break;
            case 3:
                ret.Add(this.resource_3);
                ret.Add(this.stampBack3);
                ret.Add(this.complete_3);
                break;
            case 4:
                ret.Add(this.resource_4);
                ret.Add(this.stampBack4);
                ret.Add(this.complete_4);
                break;
            case 5:
                ret.Add(this.resource_5);
                ret.Add(this.stampBack5);
                ret.Add(this.complete_5);
                break;
            case 6:
                ret.Add(this.resource_6);
                ret.Add(this.stampBack6);
                ret.Add(this.complete_6);
                break;
            case 7:
                ret.Add(this.resource_7);
                ret.Add(this.stampBack7);
                ret.Add(this.complete_7);
                break;
            case 8:
                ret.Add(this.resource_8);
                ret.Add(this.stampBack8);
                ret.Add(this.complete_8);
                break;

            default:
                break;
        }

        return ret;

    }
}
