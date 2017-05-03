using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Deactivate object on variable condition.
/// </summary>
public class deactivateObjectOnVariable : MonoBehaviour {

	/// <summary>
	/// The variable to check.
	/// </summary>
	public string variable;
	/// <summary>
	/// The condition for variable.
	/// </summary>
	public bool condition;
	/// <summary>
	/// The object to disable.
	/// </summary>
	public GameObject obj;


	// Update is called once per frame
	void Update () {

		disable ();

	}

	/// <summary>
	/// Disable the object if condition.
	/// </summary>
	void disable(){
		if (DialogueLua.GetVariable (variable).AsBool==condition){
			obj.SetActive (false);
		}
	}
}
