using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ShowOnScreen : MonoBehaviour {
	public Texture PhoneTexture;
	public Texture MessageTexture;
	public Texture MessageScreenTexture;
	public GameObject showtest;
	bool sPhone = false;
	bool showing = false;
	bool showPhone = false;
	bool messageIcon = false;
	private GUIStyle guiStyle = new GUIStyle();
	void OnGUI() {
		if (!PhoneTexture) {
			Debug.LogError("Assign a Texture in the inspector.");
			return;
		}
		guiStyle.fontSize = 45;
		int pscore = DialogueLua.GetVariable ("Score").AsInt;
		// GUI.Label(new Rect(10, 10, 200, 30), "Player Score");
		GUILayout.Label("ΣΚΟΡ: "+pscore, guiStyle);


		showPhone = DialogueLua.GetVariable ("showPhone").AsBool;


		//if (showPhone) {
		//	GUI.DrawTexture (new Rect (10, 500, 100, 100), PhoneTexture);
		//}





		
		if (sPhone && showPhone) {
			
			GUI.DrawTexture (new Rect (400, 120, 331, 640), MessageScreenTexture);
		}



	}


	public GameObject fullmap;
	public GameObject minimap;
	public bool minimapshowing = true;
	public bool fullmapshowing = false;
	void Update(){

		if (Input.GetButtonDown ("fullmShow")) {
			if (fullmapshowing == false) {
			
				fullmap.SetActive (true);
				minimap.SetActive (false);
				fullmapshowing = true;
				minimapshowing = false;
			

			} else {

				fullmap.SetActive (false);
				minimap.SetActive (true);
				fullmapshowing = false;
				minimapshowing = true;
			}


		}


		messageIcon = DialogueLua.GetVariable ("messageIcon").AsBool;

		if (messageIcon) {
			//GUI.DrawTexture (new Rect (10, 400, 50, 50), MessageTexture);
			showtest.SetActive (true);
		} else {
			showtest.SetActive (false);
		}

			if (Input.GetButtonDown ("mShow")) {

				if (!showing) {
					//Debug.LogError ("show");
					sPhone = true;
					showing = true;
		
		
				} else {
					//Debug.LogError ("not show");
					sPhone = false;
					showing = false;

				}

			}

	}
}
