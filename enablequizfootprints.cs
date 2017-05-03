using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Enable quiz footprints if condition.
/// </summary>
public class enablequizfootprints : MonoBehaviour {

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">player collider</param>
	void OnTriggerEnter(Collider other){

		//if quiz is done and the score is less than perfect, it means that player done atleast 1 wrong answer, so we enable footprints
		if (other.tag == "Player" && DialogueLua.GetVariable("quizDone").AsBool && !DialogueLua.GetVariable("quizfailed").AsBool) {
			int oldcore = DialogueLua.GetVariable ("Score").AsInt;
			if (RandomNodeLuaFunctions.NODES_TO_SELECT*100+oldcore>oldcore)
			DialogueLua.SetVariable ("quizfailed",true);
	
	}
}


}