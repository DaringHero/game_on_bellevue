using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ConfirmLocation : MonoBehaviour
{
  public Dropdown locationDropdown;

  private Button button;

  void Start ()
  {
    button = GetComponent<Button>();
    button.onClick.AddListener(OnConfirm);

    List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();
    foreach(Location loc in LocationManager.Locations)
    {
      Dropdown.OptionData option = new Dropdown.OptionData(loc.areaName + " : " + loc.specificName);
      newOptions.Add(option);
    }

    locationDropdown.AddOptions(newOptions);
  }
  
  void OnConfirm()
  {
    if(locationDropdown.value == 0)
    {
      // Registration
      SceneManager.LoadScene(SceneTransitioner.RegistrationPosition);
    }
    else
    {
      // Move to location
      SceneTransitioner.kioskLocation = locationDropdown.value - 1;
      SceneManager.LoadScene(SceneTransitioner.KioskPosition);
    }
  }
}
