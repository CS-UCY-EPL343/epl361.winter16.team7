using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessageInteract : MonoBehaviour {

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
