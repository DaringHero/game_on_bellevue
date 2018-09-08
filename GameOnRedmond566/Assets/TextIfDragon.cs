using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextIfDragon : MonoBehaviour {

	public YellOnClaim myYellOnClaim;
	public int dragonLevel = 0;

	public  Text myText;

	[HideInInspector]
	public string ReplaceWith;

	string originalText;


	public void OnEnable()
	{
		this.originalText = myText.text;

		if(myYellOnClaim.MyCurrentToy.customData.GetInt("DragonLevel", 0) == this.dragonLevel)
		{
			myText.text = ReplaceWith;
			Debug.Log("set ReplaceWith text = "+ReplaceWith);
		}
		else if(myYellOnClaim.changing_from_egg_2_hatchling)
        {
            myText.text = ReplaceWith;
            Debug.Log("set ReplaceWith text = " + ReplaceWith);
            myYellOnClaim.changing_from_egg_2_hatchling = false;
        }
        else
		{
			myText.text = this.originalText;
			Debug.Log("set originalText = "+originalText);
		}
	}
	public void OnDisable()
	{
		myText.text = this.originalText;
	}
}
