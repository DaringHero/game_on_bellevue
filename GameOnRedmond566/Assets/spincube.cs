﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spincube : MonoBehaviour {

    int speed;
	// Use this for initialization
	void Start () {
        speed = Random.Range(0, 69);
	}
	
	// Update is called once per frame
	void Update () {

            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
    }
