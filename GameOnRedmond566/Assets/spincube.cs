﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spincube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            transform.Rotate(Vector3.up, 10 * Time.deltaTime);
        }
    }
