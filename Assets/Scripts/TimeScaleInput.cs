using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Time.timeScale = 2;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Time.timeScale = 3;
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Time.timeScale = 4;
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Time.timeScale = 5;
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            Time.timeScale = 10;
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            Time.timeScale = 20;
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            Time.timeScale = 30;
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            Time.timeScale = 40;
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            Time.timeScale = 0.5f;
        }
    }
}
