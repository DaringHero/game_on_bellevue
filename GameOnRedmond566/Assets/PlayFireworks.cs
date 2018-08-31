using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFireworks : MonoBehaviour {

    public List<ParticleSystem> f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        PlayTheFireWorks();
    }

    public void PlayTheFireWorks()
    {
        foreach(ParticleSystem work in f)
        {
            work.Play();
        }
    }
}
