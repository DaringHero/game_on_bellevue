using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
  public Image Checkmark;
  public Text ItemText;
  public Image ItemImage;

  public Sprite CheckOn;
  public Sprite CheckOff;

  private bool completeData;

  public bool complete
  {
    set
    {
      completeData = value;

      if (value)
        Checkmark.sprite = CheckOn;
      else
        Checkmark.sprite = CheckOff;
    }

    get
    {
      return completeData;
    }
  }

  public void setItem(int count, Item i)
  {
    ItemText.text = count + " " +ItemManager.getName(i);
    ItemImage.sprite = Resources.Load<Sprite>(ItemManager.getSpritePath(i));
  }
}
