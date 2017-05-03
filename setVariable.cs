using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Set variable on trigger enter.
/// </summary>
public class setVariable : MonoBehaviour {

	/// <summary>
	/// Object to enable
	/// </summary>
	public GameObject toEnable;
	/// <summary>
	/// variable to set
	/// </summary>
	public string variableName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			DialogueLua.SetVariable (variableName,true);
			toEnable.SetActive (true);
		}

	}

}
