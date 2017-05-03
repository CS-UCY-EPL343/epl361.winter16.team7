using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Show offline message.
/// </summary>
public class showofflinemessage : MonoBehaviour {
	
	/// <summary>
	/// The message.
	/// </summary>
	public GameObject message;

	// Use this for initialization
	void Start () {
		
		if (Login.password_logged == null && Login.username_logged == null && Login.save_logged == null) {
			message.SetActive (true);
		}

	}
	

}
