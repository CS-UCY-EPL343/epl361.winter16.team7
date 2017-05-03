using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Destroy the sender quest gameobject.
/// </summary>
public class destroySenderQuest : MonoBehaviour {

	/// <summary>
	/// GameObject to  disable.
	/// </summary>
	public GameObject toDisable;
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">player collider</param>
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" && DialogueLua.GetVariable("stillInside").AsBool) {
			DialogueLua.SetVariable ("xarosDone", true);
			//toDisable.SetActive (false);
		}

	}
}
