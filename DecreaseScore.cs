using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Decrease score for unknownsender quest.
/// </summary>
public class DecreaseScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	/// <summary>
	/// The interval.
	/// </summary>
	int interval = 1; 
	/// <summary>
	/// The next time.
	/// </summary>
	float nextTime = 0;
	// Update is called once per frame
	void Update () {
		
		if (Time.time >= nextTime) {

			//If still inside call to decrease
			if (DialogueLua.GetVariable ("stillInside").AsBool) {
				decrease ();
			}

			nextTime += interval; 

		}

	}

	/// <summary>
	/// Decrease the variable from Dialogue Lue.
	/// </summary>
	void decrease(){
	//	yield return new WaitForSeconds(1);
		DialogueLua.SetVariable ("Score", DialogueLua.GetVariable ("Score").AsInt - 2);

	}

}
