using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation *= Quaternion.Euler(0, 90.0f * Time.deltaTime, 0);
    }
}
