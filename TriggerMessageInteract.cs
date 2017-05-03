using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger to display the message.
/// </summary>
public class TriggerMessageInteract : MonoBehaviour {


    /// <summary>
    /// message to display
    /// </summary>
    public GameObject message;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {

			message.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			
			message.SetActive (false);
		}
	}
}
