using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation2D : MonoBehaviour {

  public float angularVelocity = 360;

  private void OnEnable()
  {
    gameObject.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
  }

  // Use this for initialization
  void Start () {

  }
	
	// Update is called once per frame
	void Update () {
    
  }
}
