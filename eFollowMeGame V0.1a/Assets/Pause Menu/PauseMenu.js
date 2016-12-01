
var mainMenuSceneName : String;
var pauseMenuFont : Font;
private var pauseEnabled = false;			

function Start(){
	pauseEnabled = false;
	Time.timeScale = 1;
	Cursor.visible = false;
}

function Update(){

	//check if pause button (escape key) is pressed
	if(Input.GetKeyDown("escape")){
	
		//check if game is already paused		
		if(pauseEnabled == true){
			//unpause the game
			pauseEnabled = false;
			Time.timeScale = 1;
			Cursor.visible = false;			
		}
		
		//else if game isn't paused, then pause it
		else if(pauseEnabled == false){
			pauseEnabled = true;
			Time.timeScale = 0;
			Cursor.visible = true;
		}
	}
}

private var showGraphicsDropDown = false;

function OnGUI(){

    GUI.skin.box.font = pauseMenuFont;
    GUI.skin.box.fontSize = 30;
    GUI.skin.button.font = pauseMenuFont;
    GUI.skin.button.fontSize = 30;

	if(pauseEnabled == true){
		
		//Make a background box
		GUI.Box(Rect(Screen.width /2 - 250,Screen.height /2 - 100,500,300), "Pause Menu");
		
		//Make Main Menu button
		if(GUI.Button(Rect(Screen.width /2 - 250,Screen.height /2 ,500,100), "Back to Main Menu")){
			Application.LoadLevel(mainMenuSceneName);
		}
		
		
		//Make quit game button
		
		if  (GUI.Button(Rect(Screen.width /2 - 250,Screen.height /2 +100,500,100), "Quit Game")){
			Application.Quit();
		}
	}
}