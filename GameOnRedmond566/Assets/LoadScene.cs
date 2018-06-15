using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    public float delay = 3.0f;
    public string SceneName = "";
    //public UnityEngine.SceneManagement.Scene SceneToLoad;

    // Use this for initialization
    void Start()
    {
        if (delay >= 0.0f)
        {
            StartCoroutine(this.LoadRoutine());
        }
    }

    IEnumerator LoadRoutine()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("LoadinScene :: " + this.SceneName);
        this.LoadMyScene();
    }
    public void LoadMyScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(this.SceneName);
    }
}
