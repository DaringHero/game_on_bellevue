using System.Collections;
using System.Collections.Generic;
using System;//for Array
using UnityEngine;
using UnityEngine.UI;

public class MaxText : MonoBehaviour {

	public int MaxLength = 500;
	public Text myText;

	// Update is called once per frame
	void Update () {

		if(myText.text.Length> this.MaxLength)
		{
			myText.text = ClampText( myText.text, MaxLength);
		}
		
	}

	public static string Reverse( string s )
	{
	    char[] charArray = s.ToCharArray();
	    Array.Reverse( charArray );
	    return new string( charArray );
	}

	public static string ClampText(string s, int max)
	{
		if(s.Length> max)
		{
			return Reverse(Reverse(s).Remove(s.Length-max));//remove off the top
		}
		else
		{
			return s;
		}

	}

}
