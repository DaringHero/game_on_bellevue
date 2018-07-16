using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashFeedback : MonoBehaviour
{
  public Image image;

  public Color affirmative;
  public Color denial;
  public Color idle;

  public float fadeInTime;
  public float fadeOutTime;

  private float counter = 0;

  public bool update(float dt, Color toColor)
  {
    counter += dt;

    if(counter <= fadeInTime)
    {
      // fade in
      float t = counter / fadeInTime;
      image.color = (1 - t) * idle + (t) * toColor;
    }
    else if(counter <= fadeInTime + fadeOutTime)
    {
      // fade out
      float t = (counter - fadeInTime) / fadeOutTime;
      image.color = (1 - t) * toColor + (t) * idle;
    }
    else
    {
      image.color = idle;
      counter = 0.0f;
      return true;
    }

    return false;
  }
}
