using UnityEngine;
using System.Collections;
/// <summary>
/// Opens a url.
/// </summary>
public class openurl : MonoBehaviour {

	/// <summary>
	/// Opens the url link.
	/// </summary>
    public void openUrllink() {

        Application.OpenURL("https://github.com/CS-UCY-EPL361/epl361.winter16.team7/issues");
    }
}
