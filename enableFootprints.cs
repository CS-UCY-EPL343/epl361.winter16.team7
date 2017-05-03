using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;


/// <summary>
/// Enable footprints for vendor mystery quest.
/// </summary>
public class enableFootprints : MonoBehaviour {

	/// <summary>
	/// The variable to set.
	/// </summary>
	public string variable;

	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
	/// <param name="other">player collider</param>
	void OnTriggerStay(Collider other){
		if (other.tag == "Player"&& DialogueLua.GetVariable("vendfailed").AsBool) {

			DialogueLua.SetVariable (variable, true);
		}
	}
	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="other">player collider</param>
	void OnTriggerExit(Collider other){
		if (other.tag == "Player"&& DialogueLua.GetVariable("vendfailed").AsBool) {

			DialogueLua.SetVariable (variable, true);
		}
	}
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">player collider</param>
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"&& DialogueLua.GetVariable("vendfailed").AsBool) {

			DialogueLua.SetVariable (variable, true);
		}
	}



}
