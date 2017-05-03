using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Load next with loading screen.
/// </summary>
public class LoadNextWithLoadingScreen : MonoBehaviour {

    /// <summary>
    /// Loads the next level
    /// </summary>
	public void Loadlevel()
	{
		// Alternative method showing to wait a key press to continue and using a second load scene
		DPLoadScreen.Instance.LoadLevel("mcp_day", true, "LoadScreenPressToContinue");
	}
}
