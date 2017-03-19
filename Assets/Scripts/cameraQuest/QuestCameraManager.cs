using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class QuestCameraManager : MonoBehaviour {

	public GameObject Male, Female;
	public GameObject objectPlayer,other,msg;
	public GameObject objectCamera;
	public GameObject finalDisable;
	public GameObject Message;
	private float sec = 4f;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerExit(){
		Message.SetActive (false);
	}
	// Update is called once per frame
	void OnTriggerStay (Collider other) {
		if (other.tag == "Player" && DialogueLua.GetVariable ("mariaPhoto").AsBool) {
			msg.SetActive (false);
			Message.SetActive (true);
			if (DialogueLua.GetVariable ("mariaPhoto").AsBool && Input.GetButtonDown ("takePhotoG")) {
				Message.SetActive (false);
				objectPlayer.SetActive (false);
				if (DialogueLua.GetVariable ("Male").AsBool) {
					Female.SetActive (false);
					Male.SetActive (true);

				} else {
					Male.SetActive (false);
					Female.SetActive (true);
				}
				objectCamera.SetActive (true);
				StartCoroutine (continueGame ());
			}
		}
	}

	IEnumerator  continueGame(){
		yield return new WaitForSeconds(sec);
		DialogueLua.SetVariable("q4", true);
		DialogueLua.SetVariable("q4q", true);
		objectPlayer.SetActive (true);
		finalDisable.SetActive (false);
	}
}
