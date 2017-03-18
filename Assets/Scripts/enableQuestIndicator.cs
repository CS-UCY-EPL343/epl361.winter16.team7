using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class enableQuestIndicator : MonoBehaviour {

	public string variable;
	public bool condition;
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
