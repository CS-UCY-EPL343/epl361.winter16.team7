using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;


/// <summary>
/// Enable quest indicator(question mark if the condition 
/// is the same).
/// </summary>
public class enableQuestIndicator : MonoBehaviour {

	/// <summary>
	/// The variable to check.
	/// </summary>
	public string variable;
	/// <summary>
	/// The condition for variable.
	/// </summary>
	public bool condition;
	/// <summary>
	/// The object to enable/disable.
	/// </summary>
	public GameObject obj;

	
	// Update is called once per frame
	void Update () {

		if (DialogueLua.GetVariable (variable).AsBool==condition){
			obj.SetActive (true);
		}else{
			obj.SetActive (false);
		}

	}
}
