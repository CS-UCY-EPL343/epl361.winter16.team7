using UnityEngine;
using System.Collections;

public class displayscore : MonoBehaviour {
    private GUIStyle guiStyle = new GUIStyle(); //create a new variable
    // Use this for initialization
    void OnGUI()
    {
        guiStyle.fontSize = 30;
        // GUI.Label(new Rect(10, 10, 200, 30), "Player Score");
        GUILayout.Label("Player Score: --", guiStyle);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
