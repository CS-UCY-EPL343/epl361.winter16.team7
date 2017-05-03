using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Setters for username and password in Dialogue Lua so we can use them later.
/// </summary>
public class Setters : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Login.username_logged != null) {
			DialogueLua.SetVariable ("Username", Login.username_logged);
			DialogueLua.SetVariable ("userpass", Login.password_logged);
		} else {
			//for offline check
			DialogueLua.SetVariable ("Username", "@@@");
			DialogueLua.SetVariable ("userpass", "@@@");
		}

		Debug.Log(DialogueLua.GetVariable ("Username").AsString);
		Debug.Log(DialogueLua.GetVariable ("userpass").AsString);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
