using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controling : MonoBehaviour {
    public Transform ship;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ship.Rotate(ship.rotation.eulerAngles);

        }
	}
}
