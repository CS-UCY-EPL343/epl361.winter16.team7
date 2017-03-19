using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
using System.IO;

public class CharacterSelectionManager : MonoBehaviour {

	public GameObject[] objs;
	public GameObject[] characters = new GameObject[2];

	private int selection;
	public GameObject toDestroy;
	public GameObject cam;

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

	public void setSelectionMale(){
		selection = 0;


	}
	public void setSelectionFemale(){
		selection = 1;

	}

	public void doExitGame()
	{
		Application.Quit();
	}


	public void loadGame(){
		if (File.Exists((Application.persistentDataPath + "/savedgame.json"))) {

			string saveData = (File.ReadAllText (Application.persistentDataPath + "/savedgame.json"));
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
			DialogueManager.ShowAlert ("Game Loaded from PlayerPrefs");

		} else {
			


		StartCoroutine(fade ());

		}
		if (selection == 0) {
			DialogueLua.SetVariable ("Male", true);
		} else {
			DialogueLua.SetVariable("Male", false);
		}
	}


	IEnumerator fade(){
		myText.SetActive( true ); 
		yield return new WaitForSeconds (1.5f);
	myText.SetActive( false ); 
	}


	public void newGame(){
	
	}

	public void letsPlay(){
		
		cam.SetActive (false);
	

		characters [selection].SetActive (true);


		for (int i = 0; i < objs.Length; i++) {

			objs [i].SetActive (true);
		}
	
		Object.Destroy (toDestroy);

		if (toDestroy == null)
			Debug.Log ("ok");


		if (selection == 0) {
			DialogueLua.SetVariable ("Male", true);
		} else {
			DialogueLua.SetVariable("Male", false);
		}
	}

}
