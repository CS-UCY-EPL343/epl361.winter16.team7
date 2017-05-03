using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scoreboards link.
/// </summary>
public class scoreboards : MonoBehaviour {

	/// <summary>
	/// Opens the site link.
	/// </summary>
	public void openSiteLink(){

		System.Diagnostics.Process.Start("http://localhost/efollowme/scoreboards");

	}
}
