using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;



	
	/// <summary>
	/// This script provides a main menu with some settings.
	/// </summary>
	public class PauseMenuManager : MonoBehaviour {

		/// <summary>
		/// The menu key.
		/// </summary>
        public KeyCode menuKey = KeyCode.Escape;
		/// <summary>
		/// The GUI skin.
		/// </summary>
		public GUISkin guiSkin;
		/// <summary>
		/// The quest log window.
		/// </summary>
		public QuestLogWindow questLogWindow;
		/// <summary>
		/// GameObjects to disable when menu is open.
		/// </summary>
		public GameObject[] toDisableWhenMenuIsOpen;
		/// <summary>
		/// to check if menu is open.
		/// </summary>
		private bool isMenuOpen = false;
		/// <summary>
		/// The window rect.
		/// </summary>
		private Rect windowRect = new Rect(0, 0, 500, 500);
		/// <summary>
		/// The scaled rect (for menu).
		/// </summary>
		private ScaledRect scaledRect = ScaledRect.FromOrigin(ScaledRectAlignment.MiddleCenter, ScaledValue.FromPixelValue(300), ScaledValue.FromPixelValue(350));
		/// <summary>
		/// The scaled rect2 (for graphics options).
		/// </summary>
		private ScaledRect scaledRect2 = ScaledRect.FromOrigin(ScaledRectAlignment.BottomCenter, ScaledValue.FromPixelValue(700), ScaledValue.FromPixelValue(50));
		/// <summary>
		/// The window rect for graphics options.
		/// </summary>
		private Rect windowRectGraphics = new Rect(0, 0, 700, 100);
		/// <summary>
		/// The show graphics drop down.
		/// </summary>
		private bool showGraphicsDropDown = false;

		/// <summary>
		/// The slider canvas (volume).
		/// </summary>
		public GameObject sliderCanvas;

		/// <summary>
		/// The music sources.
		/// </summary>
		private AudioSource[] musicSource;

		/// <summary>
		/// The music volume slider.
		/// </summary>
		public Slider musicVolumeSlider;

		/// <summary>
		/// Raises the enable event.
		/// </summary>
		void OnEnable(){
			musicVolumeSlider.onValueChanged.AddListener(delegate{OnMusicVolumeChange();});
			musicVolumeSlider.value = AudioListener.volume;
			showSettings (isMenuOpen);
		}

        /// <summary>
        /// Listener to change volume when changing
        /// the levels of the music slider.
        /// </summary>
		public void OnMusicVolumeChange(){
			
			AudioListener.volume =musicVolumeSlider.value;



		}

        /// <summary>
        /// enables/disables the music slider
        /// </summary>
        /// <param name="show">boolean variable</param>
		void showSettings(bool show){

			sliderCanvas.SetActive (show);

		
		}

		void Start() {
			if (questLogWindow == null) questLogWindow = FindObjectOfType<QuestLogWindow>();
			//DialogueManager.ShowAlert("Πάτησε " + menuKey + " για το Μενού");
		}
		
		void Update() {
			//check and enable/disable menu
			if (Input.GetKeyDown(menuKey) && !DialogueManager.IsConversationActive && !IsQuestLogOpen()) {
				showExitOptions (false);
				showGraphicsDropDown = false;
				SetMenuStatus(!isMenuOpen);

			}
			// If you want to lock the cursor during gameplay, add ShowCursorOnConversation to the Player,
			// and uncomment the code below:
			if (!DialogueManager.IsConversationActive && !isMenuOpen && !IsQuestLogOpen () && !showingExitOptions) {
				Screen.lockCursor = true;
			}

			if (isMenuOpen || IsQuestLogOpen()) {
				for (int i = 0; i < toDisableWhenMenuIsOpen.Length; i++) {
					toDisableWhenMenuIsOpen [i].SetActive (false);
				}
			} else {
				for (int i = 0; i < toDisableWhenMenuIsOpen.Length; i++) {
					if (toDisableWhenMenuIsOpen [i].activeSelf==false) 
						toDisableWhenMenuIsOpen [i].SetActive (true);
				}
			}
		}

		/// <summary>
		/// Raises the GU event.
		/// </summary>
		void OnGUI() {
			if (isMenuOpen) {
				
				if (guiSkin != null) {
					GUI.skin = guiSkin;
				}
				windowRect = GUI.Window(0, windowRect, WindowFunction, "Μενού");

                //show graphic options
				if(showGraphicsDropDown == true){
				windowRectGraphics = GUI.Window(1, windowRectGraphics, WindowFunctionGraphics,"");}
			}
		}
		


		/// <summary>
		/// setup graphics options
		/// </summary>
		/// <param name="windowID">Window ID</param>
		private void WindowFunctionGraphics(int windowID) {


			//Create the Graphics settings buttons, these won't show automatically, they will be called when
			//the user clicks on the "Ποιότητα Γραφικών" Button, and then dissapear when they click
			//on it again....
		
			if(GUI.Button(new Rect(10, 10,  windowRectGraphics.width/5, 48), "Ελάχιστη")){

				QualitySettings.SetQualityLevel (0, true);

			}
			
			if(GUI.Button(new Rect(150, 10, windowRectGraphics.width/6, 48), "Χαμηλή")){
	
		
				QualitySettings.SetQualityLevel (1, true);
			}
			
			if(GUI.Button(new Rect(290, 10, windowRectGraphics.width/5, 48), "Κανονική")){


				QualitySettings.SetQualityLevel (2, true);
			}
			if(GUI.Button(new Rect(440, 10, windowRectGraphics.width/7, 48), "Υψηλή")){
				
			
				QualitySettings.SetQualityLevel (3, true);
			}
			if(GUI.Button(new Rect(555, 10, windowRectGraphics.width/5, 48), "Καλύτερη")){
				
				QualitySettings.SetQualityLevel (4, true);
			}

			if(Input.GetKeyDown(menuKey)){
				showGraphicsDropDown = false;
			}
		

		}

		/// <summary>
		/// setup the menu
		/// </summary>
		/// <param name="windowID">Window ID</param>
		private void WindowFunction(int windowID) {
			
			if (GUI.Button(new Rect(10, 40, windowRect.width - 20, 48), "Λίστα Αποστολών")) {
				SetMenuStatus(false);
				OpenQuestLog();
			}
			if (GUI.Button(new Rect(10, 90, windowRect.width - 20, 48), "Αποθήκευση Παιχνιδιού")) {
				SetMenuStatus(false);
				SaveGame();
			}
			if (GUI.Button(new Rect(10, 140, windowRect.width - 20, 48), "Φόρτωση Παιχνιδιού")) {
				SetMenuStatus(false);
				LoadGame();
			}
			if (GUI.Button(new Rect(10, 190, windowRect.width - 20, 48), "Ποιότητα Γραφικών")) {
				

				if(showGraphicsDropDown == false){

				showGraphicsDropDown = true;}

				else{
				showGraphicsDropDown = false;}

			}

			if (GUI.Button(new Rect(10, 240, windowRect.width - 20, 48), "Έξοδος Παιχνιδιού")) {


				SetMenuStatus(false);
				showExitOptions (true);
				//doExitGame ();
			}
			if (GUI.Button(new Rect(10, 290, windowRect.width - 20, 48), "Κλείσιμο Μενού")) {
				showGraphicsDropDown = false;
				SetMenuStatus(false);
			}



		
		







		}
		/// <summary>
		/// The exit panel.
		/// </summary>
		public GameObject exitPanel;

		/// <summary>
		/// The showing exit options.
		/// </summary>
		private bool showingExitOptions = false;

		/// <summary>
		/// Shows the exit options.
		/// </summary>
		/// <param name="show">If set to <c>true</c> show.</param>
		public void showExitOptions(bool show){
			showingExitOptions = show;
			exitPanel.SetActive (show);
			Time.timeScale = show ? 0 : 1;
		
		}

		/// <summary>
		/// Exits the game.
		/// </summary>
		public void doExitGame()
		{
			Application.Quit();
		}

		/// <summary>
		/// Sets the menu status.
		/// </summary>
		/// <param name="open">If set to <c>true</c> open.</param>
		private void SetMenuStatus(bool open) {
			
			showExitOptions (false);
			isMenuOpen = open;
			showSettings (open);
			if (open) {windowRect = scaledRect.GetPixelRect();windowRectGraphics = scaledRect2.GetPixelRect();}
			Time.timeScale = open ? 0 : 1;
		}

		/// <summary>
		/// Determines whether this instance quest log is open.
		/// </summary>
		/// <returns><c>true</c> if this instance quest log is open; otherwise, <c>false</c>.</returns>
		private bool IsQuestLogOpen() {
			return (questLogWindow != null) && questLogWindow.IsOpen;
		}

		/// <summary>
		/// Opens the quest log.
		/// </summary>
		private void OpenQuestLog() {
			if ((questLogWindow != null) && !IsQuestLogOpen()) {
				questLogWindow.Open();
			}
		}


		/// <summary>
		/// The save game URL.
		/// </summary>
		private string saveGameUrl = "http://localhost/efollowme/saveGame.php";

		/// <summary>
		/// Postusersave the specified username, save and score.
		/// </summary>
		/// <param name="uname">Usernname.</param>
		/// <param name="save">Save.</param>
		/// <param name="score">Score.</param>
		IEnumerator postusersave(string uname,string save,int score){

			var form = new WWWForm(); //here you create a new form connection
			form.AddField("username", uname);
			form.AddField("save", save);
			form.AddField ("score", score);
			var w = new WWW(saveGameUrl, form); //here we create a var called 'w' and we sync with our URL and the form
			yield return w; //we wait for the form to check the PHP file, so our game dont just hang
			if (w.error != null) {
				Debug.Log (w.error); //if there is an error, tell us
				DialogueManager.ShowAlert ("Σφάλμα! Έλεγξε την σύνδεση σου!");
			} else {
				if (!string.IsNullOrEmpty (w.text)) { 
					string[] values = w.text.Split (new string[] { "</br>" }, System.StringSplitOptions.None); 

					var attributes = values [0].Split (',');
					for (int i = 1; i < values.Length - 1; i++) {


						attributes = values [i].Split (',');

					}
					Debug.Log (attributes [0]);
					if (attributes [0].Equals ("ERROR")) {
						Debug.Log ("ERROR");
						DialogueManager.ShowAlert ("Σφάλμα! Η αποθύκευση δεν έγινε");
					} else if (attributes [0].Equals ("NOTHING")) {
						Debug.Log ("NOTHING CHANGED");
						DialogueManager.ShowAlert ("Δεν υπάρχουν αλλαγές για να αποθηκευτούν");
					} else if (attributes [0].Equals ("OK")) {
						Login.save_logged = save;
						Debug.Log ("GAME SAVED" + Login.save_logged);
						DialogueManager.ShowAlert ("Το παιχνίδι αποθηκέυτηκε");
					}
				} else {
					DialogueManager.ShowAlert ("Σφάλμα! Η αποθύκευση δεν έγινε");
				}
			}


		}

		/// <summary>
		/// Exits all menus.
		/// </summary>
		public void exitAll(){
			showExitOptions (false);
			SetMenuStatus (false);
		}

		/// <summary>
		/// Saves and exits the game.
		/// </summary>
		public void saveAndExitGame(){
			SaveGame ();
			doExitGame ();

		}

		/// <summary>
		/// Saves the game.
		/// </summary>
		private void SaveGame() {
			if (Login.username_logged == null && Login.password_logged == null) {
				DialogueManager.ShowAlert("Δεν μπορείς να αποθηκεύσεις το παιχνίδι");
				return;
			}
			string saveData = PersistentDataManager.GetSaveData();
			//PlayerPrefs.SetString("SavedGame", saveData);
			//File.WriteAllText (Application.persistentDataPath + "/savedgame.json", saveData);


			StartCoroutine (postusersave(Login.username_logged,saveData,DialogueLua.GetVariable("Score").AsInt));

			//Debug.Log("Save Game Data: " + saveData);
			//DialogueManager.ShowAlert("Game Saved to PlayerPrefs");


		}
	
		/// <summary>
		/// Loads the game.
		/// </summary>
		private void LoadGame() {
			
			//if (File.Exists((Application.persistentDataPath + "/savedgame.json"))) {
			if (Login.save_logged!= null && !Login.save_logged.Equals("empty")){
				//string saveData = (File.ReadAllText(Application.persistentDataPath + "/savedgame.json"));
				string saveData = Login.save_logged;
				//string saveData = PlayerPrefs.GetString("SavedGame");
				Debug.Log("Load Game Data: " + saveData);
				LevelManager levelManager = FindObjectOfType<LevelManager>();
				if (levelManager != null) {
					levelManager.LoadGame(saveData);
				} else {
					PersistentDataManager.ApplySaveData(saveData);
                    DialogueManager.SendUpdateTracker();
				}
				DialogueManager.ShowAlert("Το παιχνίδι φορτώθηκε");
				GameObject.FindWithTag ("Player").transform.localScale = 
					new Vector3 (
						DialogueLua.GetVariable ("PlayerX").AsFloat,
						DialogueLua.GetVariable ("PlayerY").AsFloat,
						DialogueLua.GetVariable ("PlayerZ").AsFloat);
			} else {
				DialogueManager.ShowAlert("Δεν υπάρχει αποθηκευμένο παιχνίδι");
			}
		}
		
	


	}


