using System.Collections;
using System.Collections.Generic;
using System;//for Array
using UnityEngine;
using UnityEngine.UI;

public class MaxText : MonoBehaviour {

	public int MaxLength = 5000;
	public Text myText;

	// Update is called once per frame
	void Update () {

		if(myText.text.Length> this.MaxLength)
		{
			myText.text = Reverse(Reverse(myText.text).Remove(myText.text.Length-this.MaxLength));//remove off the front
		}
		
	}

	public static string Reverse( string s )
	{
	    char[] charArray = s.ToCharArray();
	    Array.Reverse( charArray );
	    return new string( charArray );
	}

}
