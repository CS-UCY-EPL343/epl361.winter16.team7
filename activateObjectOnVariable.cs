using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Activates the given object on variable selected condition.
/// </summary>
public class activateObjectOnVariable : MonoBehaviour {

	/// <summary>
	/// The variable to check.
	/// </summary>
	public string variable;
	/// <summary>
	/// The condition for variable.
	/// </summary>
	public bool condition;
	/// <summary>
	/// The object to enable.
	/// </summary>
	public GameObject obj;


	// Update is called once per frame
	void Update () {
		enable ();

	}

	/// <summary>
	/// Enable the object if condition.
	/// </summary>
	void enable(){
		if (DialogueLua.GetVariable (variable).AsBool==condition){
			obj.SetActive (true);
		}

	}

}
