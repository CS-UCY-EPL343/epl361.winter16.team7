using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// OnScreen manager.
/// </summary>
public class ShowOnScreen : MonoBehaviour {

	/// <summary>
	/// The message texture.
	/// </summary>
	public Texture MessageTexture;
	/// <summary>
	/// The phone screen texture.
	/// </summary>
	public Texture PhoneScreenTexture;
	/// <summary>
	/// The phone with message screen texture.
	/// </summary>
	public Texture PhoneWithMessageScreenTexture;
	/// <summary>
	/// The object to show.
	/// </summary>
	public GameObject showtest;
	/// <summary>
	/// The showing phone variable.
	/// </summary>
	bool sPhone = false;
	/// <summary>
	/// The is showing variable.
	/// </summary>
	bool showing = false;
	/// <summary>
	/// The show phone variable.
	/// </summary>
	bool showPhone = false;
	/// <summary>
	/// The message icon to show variable.
	/// </summary>
	bool messageIcon = false;
	/// <summary>
	/// The GUI style.
	/// </summary>
	private GUIStyle guiStyle = new GUIStyle();



	/// <summary>
	/// Raises the GU event.
	/// </summary>
	void OnGUI() {
		if (!fullmapshowing) {
			
			guiStyle.fontSize = 40;
			int pscore = DialogueLua.GetVariable ("Score").AsInt;
			// GUI.Label(new Rect(10, 10, 200, 30), "Player Score");
			GUILayout.Label ("ΣΚΟΡ: " + pscore, guiStyle);
			guiStyle.fontSize = 30;
			GUILayout.Label ("Πάτησε 'Χ' για τον χάρτη", guiStyle);
			GUILayout.Label ("Πάτησε 'ESC' για το μενού", guiStyle);


			//get show phone
			showPhone = DialogueLua.GetVariable ("showPhone").AsBool;


			//show phone
			if (sPhone && showPhone) {
				
				///show normal phone screen
				if (!DialogueLua.GetVariable ("messageIcon").AsBool) {
					float width = 268.11f;
					float height = 518.4f;
					GUI.DrawTexture (new Rect ((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height / 2), width, height), PhoneScreenTexture);
				} else {
					//show phone with message
					float width1 = 511.2f;
					float height1 = 535.5f;
					GUI.DrawTexture (new Rect ((Screen.width / 2) - (width1 / 2), (Screen.height / 2) - (height1 / 2), width1, height1), PhoneWithMessageScreenTexture);

				}
			}

		}
	}

	/// <summary>
	/// The fullmap gameobject.
	/// </summary>
	public GameObject fullmap;

	/// <summary>
	/// is fullmapshowing var.
	/// </summary>
    bool fullmapshowing = false;

	void Update(){
		toggleMapOnKey ();
	}

	/// <summary>
	/// Toggles the map or the phone on key pressed.
	/// </summary>
	void toggleMapOnKey(){


		if (Input.GetButtonDown ("fullmShow")) {
			if (fullmapshowing == false) {

				fullmap.SetActive (true);

				fullmapshowing = true;



			} else {

				fullmap.SetActive (false);

				fullmapshowing = false;

			}


		}

		/*show phone*/
		messageIcon = DialogueLua.GetVariable ("messageIcon").AsBool;

		if (messageIcon) {

			showtest.SetActive (true);
		} else {
			showtest.SetActive (false);
		}

		if (Input.GetButtonDown ("mShow")) {

			if (!showing && showPhone) {

				sPhone = true;
				showing = true;


			} else {

				sPhone = false;
				showing = false;

			}

		}


	}
}
