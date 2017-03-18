using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class enableMark : MonoBehaviour {

	public string[] questsNames;
	public string[] states;

	private string[] returnedStates;
	string state1;
	string state0;
	public GameObject mark;
	public GameObject exclamationMark;
	public bool check=true;
	// Update is called once per frame
	void Update () {

		int slength = states.Length;
		returnedStates = new string[slength];
		for (int i = 0; i < slength; i++) {
			returnedStates[i]=DialogueLua.GetQuestField(questsNames[i], "State").AsString;
			if (returnedStates [i] != states [i]) {
				mark.SetActive (false);
				exclamationMark.SetActive (false);
				break;
			}else {
				exclamationMark.SetActive (true);
				mark.SetActive (true);
				check = true;
			}
		}


		/**
		state0 = DialogueLua.GetQuestField("q1", "State").AsString;
		state1 = DialogueLua.GetQuestField("q2", "State").AsString;

		if (state0 == "success" && state1 == "unassigned") {
		
			mark.SetActive (true);
		} else
			mark.SetActive (false);
*/


	}
}
