using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
  public Text targetText;
  public Image targetImage;

  public string text
  {
    get
    {
      return targetText.text;
    }

    set
    {
      targetText.text = value;
    }
  }

  public Sprite image
  {
    set
    {
      targetImage.sprite = value;
    }
  }
}
