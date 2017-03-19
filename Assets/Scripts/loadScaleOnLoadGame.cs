using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class loadScaleOnLoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject.FindWithTag ("Player").transform.localScale = 
			new Vector3 (
				DialogueLua.GetVariable ("PlayerX").AsFloat,
				DialogueLua.GetVariable ("PlayerY").AsFloat,
				DialogueLua.GetVariable ("PlayerZ").AsFloat);
	}
	

}
