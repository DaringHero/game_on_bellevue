using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OnClickMoveToScene : MonoBehaviour
{
  private Button btn;

  public int NextSceneIndex;

  // Use this for initialization
  void Start ()
  {
    btn = GetComponent<Button>();
    btn.onClick.AddListener(OnClicked);
  }

  void OnClicked()
  {
    Application.LoadLevel(NextSceneIndex);
  }

  // Update is called once per frame
  void Update ()
  {
    
  }
}
