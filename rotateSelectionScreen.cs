using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rotate the characters at selection screen.
/// </summary>
public class rotateSelectionScreen : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)   || Input.GetMouseButton(1)    )    
		transform.Rotate(new Vector3(0.0f, -Input.GetAxis("Mouse X")*5,0.0f));
	}
}
