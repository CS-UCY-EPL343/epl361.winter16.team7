using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

/// <summary>
/// Final results manager.
/// </summary>
public class resultsmanager : MonoBehaviour {

	/// <summary>
	/// The quiz questions.
	/// </summary>
	string[] quizQuestions= {
        "Παίζεις το αγαπημένο σου παιχνίδι και σε κάποια στιγμή αποφασίζεις να αγοράσεις κάτι με πραγματικά λεφτά, ποιος είναι ο πιο καλός τρόπος να το κάνεις για να προστατεύσεις τα στοιχεία σου;",
        "Είσαι θυμωμένος με ένα φίλο σου και βλέπεις ότι πολλοί κάνουν πόστ για το πώς αισθάνονται και θέλεις να το κάνεις και εσύ. Τι κάνεις;",
        "Οι διαφημίσεις που σου εμφανίζονται είναι κάτι που (επέλεξε την σωστή απάντηση):",
        "Όταν είσαι προσεκτικός με το τι κάνεις πόστ, το προσωπικό σου ψηφιακό αποτύπωμα θα είναι σε καλή κατάσταση;",
        "Έχεις ένα λογαριασμό σε ένα κοινωνικό δίκτυο που δεν το χρησιμοποιάς, τι κάνεις;",
        "Βγαίνεις με την παρέα σου και βγάζετέ μια φωτογραφία, μετά από λίγο βλέπεις ότι αυτή η φωτογραφία ανέβηκε στο Facebook. Τι κάνεις;"};
	/// <summary>
	/// The quiz answers.
	/// </summary>
    string[] quizAnswers = {
        "Η καλύτερη επιλογή όταν θέλεις να αγοράσεις κάτι με πραγματικά λεφτά σε κάποιο παιχνίδι είναι η χρήση κάρτας μιας χρήσης (όπως paysafecard), για τον λόγο ότι πληρώνεις ανώνυμα και δεν αφήνεις στοιχεία πίσω σου!",
        "Το πιο σωστό είναι να τηλεφωνήσεις σε ένα φίλο σου και να του πεις πως νιώθεις, διότι αν κάνεις ποστ το τι αισθάνεσαι μπορεί να το δει οποιοσδήποτε και να προσπαθήσει να το εκμεταλλευτεί!",
        "Οι διαφημίσεις, αν και τις αγνοούμε τις περισσότερες φορές, είναι κάτι που μας ενδιαφέρουν διότι μπαίνοντας σε διάφορες ιστοσελίδες κτλ., συλλέγονται πληροφορίες με το ιστορικό μας μέσω των cookies και μας εμφανίζονται διαφημίσεις σχετικές με αυτά!",
        "Δεν έχει σημασία πόσο προσέχεις εσύ, θα πρέπει να προσέχεις και το που σε αναφέρουν οι άλλοι (π.χ. οι φίλοι σου) διότι μέσω των άλλων μπορεί να εκτεθούν δικές σου προσωπικές πληροφορίες(όπως που βρίσκεσαι και με ποιους)!",
        "Αφού δεν χρησιμοποιάς πλέον αυτόν τον λογαριασμό το καλύτερο που έχεις να κάνεις είναι να το διαγράψεις, έτσι ώστε να μην υπάρχουν περισσότερες πληροφορίες για εσένα για να τις βλέπει ο οποιοσδήποτε!",
        "Στην περίπτωση αυτή, είναι δικαίωμα σου να χρησιμοποιήσεις την φωτογραφία αυτή όπως εσύ πιστεύεις, και το καλύτερο είναι να την κρατήσεις για τον εαυτό σου, ή αν θέλεις να την ανεβάσεις, να το κάνεις άλλη μέρα για να μην εκθέσεις την τοποθεσία σου!"
    };
	/// <summary>
	/// Quests that failed.
	/// </summary>
    string[] apostolesFailed = {
        "Έβγαλες φωτογραφία με την Μαρία, αλλά επέλεξες να την ανεβάσεις. Έκανες λάθος, γιατί ανεβάζοντάς την έκθεσες τον χώρο που βρίσκεσαι και μπορεί να το δει ο οποιοσδήποτε!",
        "Εμπιστευτικές κάποιον άγνωστο που σου έστειλε ένα μήνυμα για να σου τραβήξει την προσοχή σου. Δεν ήταν σωστό να πας να τον βρεις διότι δεν ξέρεις ποιος είναι και τι θέλει πραγματικά από εσένα!",
        "Η καλύτερη επιλογή σου εδώ ήταν να πληρώσεις μέσω κάρτας μιας χρήσης αλλά το σύστημα δεν υποστήριζε αυτή την μέθοδο πληρωμής, οπόταν θα έπρεπε να ακυρώσεις την πληρωμή. Αυτό διότι πληρώνοντας με πιστωτική/χρεωστική κάρτα, αφήνεις πίσω σου προσωπικά στοιχεία, όπως επίσης μπορεί με κάποιο τρόπο να σου πάρουν περισσότερα χρήματα από την κάρτα!",
        "Το παιχνίδι που έπαιξες σε προκαλούσε για να σου τραβήξει την προσοχή, αλλά στην ουσία έπαιρνε πληροφορίες για εσένα!",
        "Έπεσες στην παγίδα! Έδωσες τον κωδικό σου για να πάρεις φαγητό και ποτό! Αυτό ήταν λάθος, δεν πρέπει να δίνουμε τον κωδικό μας ποτέ σε κανένα!"
    };
	/// <summary>
	/// Quests that succeded.
	/// </summary>
    string[] apostolesSuccess = {
        "Έβγαλες φωτογραφία με την Μαρία και δεν την ανέβασες! Ήταν η σωστή επιλογή διότι δεν έκθεσες τον χώρο που βρίσκεσαι!",
        "Δεν εμπιστευτικές αυτόν που σου έστειλε μήνυμα! Μπράβο, δεν πρέπει να εμπιστευόμαστε αγνώστους που μας στέλνουν μηνύματα και μας λένε διάφορα!",
        "Αν δεν υπάρχει η δυνατότητα να πληρώσεις μέσω κάρτας μιας χρήσης τότε καλύτερα να ακυρώσεις την αγορά, όπως και έκανες! Συγχαρητήρια!",
        "Επέλεξες να μην παίξεις το παιχνίδι με τις πληροφορίες (μάντεψε ποιος είμαι), διότι αν το έπαιζες θα έδινες σημαντικές πληροφορίες για εσένα! Σωστή επιλογή!",
        "Δεν έπεσες στην παγίδα! Δεν πρέπει να δίνουμε τον κωδικό μας όπως ακριβός έκανες! Μπράβο!"
    };

	/// <summary>
	/// GameObjects to  disable.
	/// </summary>
	public GameObject[] disable;

	/// <summary>
	/// The final canvas.
	/// </summary>
    public GameObject FinalCanvas;
	/// <summary>
	/// The score.
	/// </summary>
	public Text Score;
	/// <summary>
	/// The dropdown (quests).
	/// </summary>
    public Dropdown dropdown;
	/// <summary>
	/// The results text.
	/// </summary>
    public Text Results;

	/// <summary>
	/// The quiz gameobject to enable quiz questions.
	/// </summary>
    public GameObject quiz;
	/// <summary>
	/// The quiz dropdown (questions).
	/// </summary>
    public Dropdown quizDropdown;
	/// <summary>
	/// The question text.
	/// </summary>
    public Text question;
    
	/// <summary>
	/// The show final results.
	/// </summary>
    bool showFinalResults=false;

	/// <summary>
	/// Checks the game is done.
	/// </summary>
	/// <returns><c>true</c>, if game is done, <c>false</c> otherwise.</returns>
	bool checkGameDone(){
		return DialogueLua.GetQuestField ("q8", "State").AsString.Equals ("success");
	}

	// Use this for initialization
	void OnEnable () {
		dropdown.onValueChanged.AddListener(delegate {
			OnMyValueChange(dropdown);
		});
		quizDropdown.onValueChanged.AddListener(delegate {
			OnMyValueChangeQuiz(quizDropdown);
		});

	}

	/// <summary>
	/// Raises the my value change event.
	/// </summary>
	/// <param name="d">the quests dropdown</param>
	public void OnMyValueChange(Dropdown d)
	{
		if (d.value == 0) {
			
			quiz.SetActive (true);
			int n = RandomNodeLuaFunctions.listNodes.Count;
			for (int i = 0; i < n; i++) {
				quizDropdown.options.Add (new Dropdown.OptionData ("Ερώτηση " + (i + 1)));
			}
			iniDropdown (quizDropdown);
			return;
		} else {
			quiz.SetActive (false);
		}

		if (d.value == 1) {

			if (DialogueLua.GetVariable ("photofailed").AsBool)
				Results.text = apostolesFailed [0];
			else
				Results.text = apostolesSuccess [0];

		} else if (d.value == 2) {

			if (DialogueLua.GetVariable ("xarosfailed").AsBool)
				Results.text = apostolesFailed [1];
			else
				Results.text = apostolesSuccess [1];

		} else if (d.value == 3) {

			if (DialogueLua.GetVariable ("pfailed").AsBool)
				Results.text = apostolesFailed [2];
			else
				Results.text = apostolesSuccess [2];

		} else if (d.value == 4) {

			if (DialogueLua.GetVariable ("gameDone").AsBool) {
				if (DialogueLua.GetVariable ("gamefailed").AsBool)
					Results.text = apostolesFailed [3];
				else
					Results.text = apostolesSuccess [3];
			} else {
				Results.text = "Δεν έκανες αυτή την μυστική αποστολή!";
			}
		} else if (d.value == 5) {

			if (DialogueLua.GetVariable ("vendDone").AsBool) {
				if (DialogueLua.GetVariable ("vendfailed").AsBool)
					Results.text = apostolesFailed [4];
				else
					Results.text = apostolesSuccess [4];
			} else {
				Results.text = "Δεν έκανες αυτή την μυστική αποστολή!";
			}
		} else
			return;




	}

	/// <summary>
	/// Raises the my value change quiz event.
	/// </summary>
	/// <param name="d">quiz questions dropdown</param>
	public void OnMyValueChangeQuiz(Dropdown d)
	{

		if (d.value+1 > RandomNodeLuaFunctions.listNodes.Count)
			return;
	//	if (d.value == 0) {
			question.text = quizQuestions [RandomNodeLuaFunctions.listNodes[d.value]-1];
			Results.text = quizAnswers [RandomNodeLuaFunctions.listNodes[d.value]-1];
		
		//}
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy()
	{
		dropdown.onValueChanged.RemoveAllListeners();
		quizDropdown.onValueChanged.RemoveAllListeners();
	}

    // Update is called once per frame
    void Update() {
        if (checkGameDone() && 
			!showFinalResults) { 
        showFinalResults = true;

			for (int i = 0; i < disable.Length; i++) {
				disable[i].SetActive (false);
			}
			SaveGame ();
			Screen.lockCursor = false;
        Time.timeScale = 0;
       	
		showUI ();
		}
    }


    /// <summary>
    /// Exits the game
    /// </summary>
	public void doExitGame()
	{
		Application.Quit();
	}

	/// <summary>
	/// Shows the UI.
	/// </summary>
    void showUI() {
		FinalCanvas.SetActive(true);
		Score.text = DialogueLua.GetVariable ("Score").AsString;
		iniDropdown (dropdown);
        



    }

	/// <summary>
	/// Initialize dropdown.
	/// </summary>
	/// <param name="d">D.</param>
	void iniDropdown(Dropdown d){
		// Add a blank dropdown option you will then remove at the end of the options list
		d.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "Επέλεξε" });
		// Select it
		d.GetComponent<Dropdown>().value = dropdown.GetComponent<Dropdown>().options.Count - 1;
		// Remove it
		d.GetComponent<Dropdown>().options.RemoveAt(dropdown.GetComponent<Dropdown>().options.Count - 1);
		// Done
	}


	/// <summary>
	/// Saves the game.
	/// </summary>
	private void SaveGame() {
		if (Login.username_logged == null && Login.password_logged == null) {
			
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
			//DialogueManager.ShowAlert ("Σφάλμα! Έλεγξε την σύνδεση σου!");
		} else {
			if (!string.IsNullOrEmpty (w.text)) { 
				string[] values = w.text.Split (new string[] { "</br>" }, System.StringSplitOptions.None); 

				var attributes = values [0].Split (',');
				for (int i = 1; i < values.Length - 1; i++) {


					attributes = values [i].Split (',');

				}
				//Debug.Log (attributes [0]);
				if (attributes [0].Equals ("ERROR")) {
				//	Debug.Log ("ERROR");
				//	DialogueManager.ShowAlert ("Σφάλμα! Η αποθύκευση δεν έγινε");
				} else if (attributes [0].Equals ("NOTHING")) {
				//	Debug.Log ("NOTHING CHANGED");
				//	DialogueManager.ShowAlert ("Δεν υπάρχουν αλλαγές για να αποθηκευτούν");
				} else if (attributes [0].Equals ("OK")) {
					Login.save_logged = save;
				//	Debug.Log ("GAME SAVED" + Login.save_logged);
				//	DialogueManager.ShowAlert ("Το παιχνίδι αποθηκέυτηκε");
				}
			} else {
				//DialogueManager.ShowAlert ("Σφάλμα! Η αποθύκευση δεν έγινε");
			}
		}


	}



}
