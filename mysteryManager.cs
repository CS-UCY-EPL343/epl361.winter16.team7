using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Mystery Quest manager.
/// </summary>
public class mysteryManager : MonoBehaviour {

	/// <summary>
	/// The vendor 1 conv.
	/// </summary>
	public GameObject vend1conv;
	/// <summary>
	/// The vendor 2 conv.
	/// </summary>
	public GameObject vend2conv;
	/// <summary>
	/// The vendor dmsg.
	/// </summary>
	public GameObject vendmsg;
	/// <summary>
	/// The game (guess who) conv.
	/// </summary>
	public GameObject gameconv;
	/// <summary>
	/// The game (guess who) msg.
	/// </summary>
	public GameObject gamemsg;




	// Use this for initialization
	void OnEnable () {
		//if vendor quest is done, disable it so it wont be started again
		if (DialogueLua.GetVariable ("vendDone").AsBool){
			vend1conv.SetActive (false);
			vend2conv.SetActive (false);
			vendmsg.SetActive (false);
		}
		//if game quest is done, disable it so it wont be started again
		if (DialogueLua.GetVariable ("gameDone").AsBool) {
			gameconv.SetActive (false);
			gamemsg.SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
