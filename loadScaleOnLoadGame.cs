using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Load player scale on load game.
/// </summary>
public class loadScaleOnLoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		//changes player scale from Dialogue Lua values
		GameObject.FindWithTag ("Player").transform.localScale = 
			new Vector3 (
				DialogueLua.GetVariable ("PlayerX").AsFloat,
				DialogueLua.GetVariable ("PlayerY").AsFloat,
				DialogueLua.GetVariable ("PlayerZ").AsFloat);
	}
	

}
