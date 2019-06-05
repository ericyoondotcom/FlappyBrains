using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstMove : MonoBehaviour {

    public Vector3 speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += speed * Time.deltaTime;
	}
}
