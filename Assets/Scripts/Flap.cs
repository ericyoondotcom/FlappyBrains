using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flap : MonoBehaviour {
    public Vector2 force;
    public bool human = false;
    // Use this for initialization
    Rigidbody2D rb;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
    public void DoFlap(){
        rb.velocity = force;
    }

	// Update is called once per frame
	void Update () {
        if((Input.anyKeyDown || Input.GetMouseButtonDown(0)) && human){
            DoFlap();
        }

	}
}
