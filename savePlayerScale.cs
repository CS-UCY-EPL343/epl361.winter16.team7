using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Saves players scale.
/// </summary>
public class savePlayerScale : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		DialogueLua.SetVariable("PlayerX", GameObject.FindWithTag ("Player").transform.localScale.x);
		Debug.Log (DialogueLua.GetVariable ("PlayerX").AsFloat);
	}
}
