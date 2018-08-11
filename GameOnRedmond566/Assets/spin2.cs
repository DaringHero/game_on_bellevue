using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin2 : MonoBehaviour
{

    int speed;
    // Use this for initialization
    void Start()
    {
        speed = 40;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}