using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
using System.IO;

namespace PixelCrushers.DialogueSystem.Examples {
	
	/// <summary>
	/// This script provides a rudimentary main menu for the Feature Demo.
	/// It's not designed to be a production-quality component but rather
	/// something to glue together the Dialogue System's features for
	/// demonstration purposes.
	/// </summary>
	public class FeatureDemo : MonoBehaviour {

        public KeyCode menuKey = KeyCode.Escape;
		public GUISkin guiSkin;
		public QuestLogWindow questLogWindow;
		
		private bool isMenuOpen = false;
		private Rect windowRect = new Rect(0, 0, 500, 500);
		private ScaledRect scaledRect = ScaledRect.FromOrigin(ScaledRectAlignment.MiddleCenter, ScaledValue.FromPixelValue(300), ScaledValue.FromPixelValue(320));
		
		void Start() {
			if (questLogWindow == null) questLogWindow = FindObjectOfType<QuestLogWindow>();
			DialogueManager.ShowAlert("Πατήσε " + menuKey + " για το Μενού");
		}
		
		void Update() {
			if (Input.GetKeyDown(menuKey) && !DialogueManager.IsConversationActive && !IsQuestLogOpen()) {
				SetMenuStatus(!isMenuOpen);
			}
			// If you want to lock the cursor during gameplay, add ShowCursorOnConversation to the Player,
			// and uncomment the code below:
			//if (!DialogueManager.IsConversationActive && !isMenuOpen && !IsQuestLogOpen ()) {
			//	Screen.lockCursor = true;
			//}
		}
		
		void OnGUI() {
			if (isMenuOpen) {
				if (guiSkin != null) {
					GUI.skin = guiSkin;
				}
				windowRect = GUI.Window(0, windowRect, WindowFunction, "Μενού");
			}
		}
		
		private void WindowFunction(int windowID) {
			if (GUI.Button(new Rect(10, 60, windowRect.width - 20, 48), "Λίστα Αποστολών")) {
				SetMenuStatus(false);
				OpenQuestLog();
			}
			if (GUI.Button(new Rect(10, 110, windowRect.width - 20, 48), "Αποθήκευση Παιχνιδιού")) {
				SetMenuStatus(false);
				SaveGame();
			}
			if (GUI.Button(new Rect(10, 160, windowRect.width - 20, 48), "Φόρτωση Παιχνιδιού")) {
				SetMenuStatus(false);
				LoadGame();
			}
			if (GUI.Button(new Rect(10, 210, windowRect.width - 20, 48), "Clear Saved Game")) {
				SetMenuStatus(false);
				ClearSavedGame();
			}
			if (GUI.Button(new Rect(10, 260, windowRect.width - 20, 48), "Κλείσιμο Μενού")) {
				SetMenuStatus(false);
			}
		}
		
		private void SetMenuStatus(bool open) {
			isMenuOpen = open;
			if (open) windowRect = scaledRect.GetPixelRect();
			Time.timeScale = open ? 0 : 1;
		}
		
		private bool IsQuestLogOpen() {
			return (questLogWindow != null) && questLogWindow.IsOpen;
		}
		
		private void OpenQuestLog() {
			if ((questLogWindow != null) && !IsQuestLogOpen()) {
				questLogWindow.Open();
			}
		}
		
		private void SaveGame() {
			string saveData = PersistentDataManager.GetSaveData();
			//PlayerPrefs.SetString("SavedGame", saveData);
			File.WriteAllText (Application.persistentDataPath + "/savegame.json", saveData);


			Debug.Log("Save Game Data: " + saveData);
			DialogueManager.ShowAlert("Game Saved to PlayerPrefs");
		}
	
		private void LoadGame() {
			
			if (true) {
				
				string saveData = (File.ReadAllText(Application.persistentDataPath + "/savegame.json"));

				//string saveData = PlayerPrefs.GetString("SavedGame");
				Debug.Log("Load Game Data: " + saveData);
				LevelManager levelManager = FindObjectOfType<LevelManager>();
				if (levelManager != null) {
					levelManager.LoadGame(saveData);
				} else {
					PersistentDataManager.ApplySaveData(saveData);
                    DialogueManager.SendUpdateTracker();
				}
				DialogueManager.ShowAlert("Game Loaded from PlayerPrefs");
			} else {
				DialogueManager.ShowAlert("Save a game first");
			}
		}
		

		private void ClearSavedGame() {
			if (PlayerPrefs.HasKey("SavedGame")) {
				PlayerPrefs.DeleteKey("SavedGame");
				Debug.Log("Cleared saved game data");
			}
			DialogueManager.ShowAlert("Saved Game Cleared From PlayerPrefs");
		}

	}

}