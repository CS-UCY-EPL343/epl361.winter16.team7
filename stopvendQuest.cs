using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Stops the vendor quest.
/// </summary>
public class stopvendQuest : MonoBehaviour {
	/// <summary>
	/// The vendor object.
	/// </summary>
	public GameObject v;

	void OnEnable(){
	
		if (DialogueLua.GetVariable ("vendDone").AsBool) {
			v.SetActive (false);
		}
	
	
	}


}
