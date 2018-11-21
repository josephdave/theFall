using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour {

	// Use this for initializatin
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(1, 0, 0, Space.Self);

    }
}
