using UnityEngine;
using System.Collections;

public class displayscore : MonoBehaviour {
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    private GUIStyle guiStyle2 = new GUIStyle(); //create a new variable
    // Use this for initialization
    void OnGUI()
    {
        guiStyle.fontSize = 45;
        // GUI.Label(new Rect(10, 10, 200, 30), "Player Score");
        GUILayout.Label("Player Score: ---", guiStyle);

        guiStyle2.fixedHeight = 100;

        GUILayout.Label("Press 'ESC' to pause", guiStyle2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
