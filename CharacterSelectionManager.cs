using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
using System.IO;

/// <summary>
/// Character selection manager.
/// </summary>
public class CharacterSelectionManager : MonoBehaviour {

	/// <summary>
	/// The objects to disable while
	/// character selection is on.
	/// </summary>
	public GameObject[] objs;

	/// <summary>
	/// The characters to select from.
	/// </summary>
	public GameObject[] characters = new GameObject[2];

	/// <summary>
	/// The selection index.
	/// </summary>
	private int selection;

	/// <summary>
	/// GameObject to destroy.
	/// </summary>
	public GameObject toDestroy;

	/// <summary>
	/// The scene camera.
	/// </summary>
	public GameObject cam;


	/// <summary>
	/// Text to be displayed (no saved game text).
	/// </summary>
	public GameObject myText;

	// Use this for initialization
	void Awake () {

		for (int i = 0; i < objs.Length; i++) {
		
			objs [i].SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sets the selection male index.
	/// </summary>
	public void setSelectionMale(){
		selection = 0;


	}
	/// <summary>
	/// Sets the selection female index.
	/// </summary>
	public void setSelectionFemale(){
		selection = 1;

	}

	/// <summary>
	/// Exits the game.
	/// </summary>
	public void doExitGame()
	{
		Application.Quit();
	}


	/// <summary>
	/// Loads the game.
	/// </summary>
	public void loadGame(){
		if (Login.save_logged!= null && !Login.save_logged.Equals("empty")) {

			//string saveData = (File.ReadAllText (Application.persistentDataPath + "/savedgame.json"));
			string saveData = Login.save_logged;
			letsPlay ();

			//string saveData = PlayerPrefs.GetString("SavedGame");
			Debug.Log ("Load Game Data: " + saveData);
			LevelManager levelManager = FindObjectOfType<LevelManager> ();
			if (levelManager != null) {
				levelManager.LoadGame (saveData);
			} else {
				PersistentDataManager.ApplySaveData (saveData);
				DialogueManager.SendUpdateTracker ();
			}
			DialogueManager.ShowAlert("Το παιχνίδι φορτώθηκε");
			GameObject.FindWithTag ("Player").transform.localScale = 
				new Vector3 (
					DialogueLua.GetVariable ("PlayerX").AsFloat,
					DialogueLua.GetVariable ("PlayerY").AsFloat,
					DialogueLua.GetVariable ("PlayerZ").AsFloat);
		} else {
			


		StartCoroutine(fade ());

		}
		if (selection == 0) {
			DialogueLua.SetVariable ("Male", true);
		} else {
			DialogueLua.SetVariable("Male", false);
		}
	}

	/// <summary>
	/// Fade the text.
	/// </summary>
	IEnumerator fade(){
		myText.SetActive( true ); 
		yield return new WaitForSeconds (1.5f);
	myText.SetActive( false ); 
	}

	/// <summary>
	/// starts new game.
	/// </summary>
	public void newGame(){
	
	}

	/// <summary>
	/// enables the disabled GameObject,
	/// sets the character and starts the game.
	/// </summary>
	public void letsPlay(){
		
		cam.SetActive (false);
	

		characters [selection].SetActive (true);


		for (int i = 0; i < objs.Length; i++) {

			objs [i].SetActive (true);
		}
	



		if (selection == 0) {
			DialogueLua.SetVariable ("Male", true);
		} else {
			DialogueLua.SetVariable("Male", false);
		}
		Object.Destroy (toDestroy);
		//toDestroy.SetActive (false);
		//if (toDestroy != null)
			//Debug.Log ("still here");
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy(){
		//Debug.Log ("bye");
	}

}
