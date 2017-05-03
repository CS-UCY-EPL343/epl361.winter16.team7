using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;


/// <summary>
/// Checks if the quest is active
/// and enables the quest marker
/// on top of the quest character
/// and on the map.
/// </summary>
public class enableMark : MonoBehaviour {

	/// <summary>
	/// The quests names.
	/// </summary>
	public string[] questsNames;
	/// <summary>
	/// The quests states.
	/// </summary>
	public string[] states;

	/// <summary>
	/// The returned states.
	/// </summary>
	private string[] returnedStates;

	/// <summary>
	/// The state1.
	/// </summary>
	string state1;
	/// <summary>
	/// The state0.
	/// </summary>
	string state0;
	/// <summary>
	/// The mark (on map).
	/// </summary>
	public GameObject mark;
	/// <summary>
	/// The exclamation mark (on npc head).
	/// </summary>
	public GameObject exclamationMark;
	/// <summary>
	/// check variable.
	/// </summary>
	public bool check=true;

	// Update is called once per frame
	void Update () {
		enableOrDisable();
	}


	/// <summary>
	/// Enables or disables the marks.
	/// </summary>
void enableOrDisable(){

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


}



}