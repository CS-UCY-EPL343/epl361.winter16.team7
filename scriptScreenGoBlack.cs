using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Script screen go black and xaros appears.
/// </summary>
public class scriptScreenGoBlack : MonoBehaviour {

	/// <summary>
	/// The object.
	/// </summary>
	public GameObject obj;
	/// <summary>
	/// The sound to be played.
	/// </summary>
	public GameObject sound;

	/// <summary>
	/// trigger to disable if xaros appears so it wont trigger again.
	/// </summary>
	public GameObject disableTheTrigger;

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">player collider</param>
	void OnTriggerEnter(Collider other){
		if (!DialogueLua.GetVariable("xarosAppeared").AsBool && other.tag == "Player") {
			DialogueLua.SetVariable ("xarosAppeared", true);

		
			StartCoroutine (LateCall ());
		}
	}

	/// <summary>
	/// Coroutine to show the black screen and play the music.
	/// </summary>
	IEnumerator LateCall()
	{

		sound.SetActive (true);

		obj.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (true);
		yield return new WaitForSeconds(0.5f);
		obj.SetActive (false);
		yield return new WaitForSeconds(0.5f);
		sound.SetActive (false);

	}
}
