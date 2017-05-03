using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// Register.
/// </summary>
public class Register : MonoBehaviour {
	/// <summary>
	/// The field username.
	/// </summary>
	public GameObject fieldUsername;
	/// <summary>
	/// The field password.
	/// </summary>
	public GameObject fieldPassword;
	/// <summary>
	/// The field re enter password.
	/// </summary>
	public GameObject fieldReEnterPassword;

	/// <summary>
	/// The input username.
	/// </summary>
	private string inputUsername;
	/// <summary>
	/// The input password.
	/// </summary>
	private string inputPassword;
	/// <summary>
	/// The input re enter password.
	/// </summary>
	private string inputReEnterPassword;
	// Use this for initialization
	void Start () {
		{
			fieldUsername.GetComponent<InputField> ().Select ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (fieldUsername.GetComponent<InputField> ().isFocused) {
				fieldPassword.GetComponent<InputField> ().Select ();
			} else if (fieldPassword.GetComponent<InputField> ().isFocused) {
				fieldReEnterPassword.GetComponent<InputField> ().Select ();
			} else if (fieldReEnterPassword.GetComponent<InputField> ().isFocused) {
				fieldUsername.GetComponent<InputField> ().Select ();
			}
		}


		if (Input.GetKeyDown(KeyCode.Return))
		{
			RegisterClick();
		}

	}


	/// <summary>
	/// The error username length message.
	/// </summary>
	public GameObject errorUsernameLengthMessage;
	/// <summary>
	/// The error password length message.
	/// </summary>
	public GameObject errorPasswordLengthMessage;
	/// <summary>
	/// The error re enter password match message.
	/// </summary>
	public GameObject errorReEnterPasswordMatchMessage;
	/// <summary>
	/// The error account message.
	/// </summary>
	public GameObject errorAccountMessage;
	/// <summary>
	/// The error no connection message.
	/// </summary>
	public GameObject errorNoConnectionMessage;
	/// <summary>
	/// The success account message.
	/// </summary>
	public GameObject successAccountMessage;

	/// <summary>
	/// The error alphanumeric username message.
	/// </summary>
	public GameObject errorAlphanumericUsernameMessage;
	/// <summary>
	/// The error alphanumeric password message.
	/// </summary>
	public GameObject errorAlphanumericPasswordMessage;


	/// <summary>
	/// Register and checks.
	/// </summary>
	public void RegisterClick(){

		errorAlphanumericUsernameMessage.SetActive (false);
		errorAlphanumericPasswordMessage.SetActive (false);
		errorReEnterPasswordMatchMessage.SetActive (false);
		errorPasswordLengthMessage.SetActive (false);
		errorUsernameLengthMessage.SetActive (false);
		errorAccountMessage.SetActive (false);
		errorNoConnectionMessage.SetActive(false);
		successAccountMessage.SetActive (false);

		inputUsername = fieldUsername.GetComponent<InputField>().text;
		inputPassword = fieldPassword.GetComponent<InputField>().text;
		inputReEnterPassword = fieldReEnterPassword.GetComponent<InputField>().text;

		bool checklength = false;
		Regex r = new Regex("^[a-zA-Z0-9]*$");
		//check if alphanumeric
		bool checkalphanumeric=true;

		if (!r.IsMatch(inputUsername)) {
			errorAlphanumericUsernameMessage.SetActive (true);

			checkalphanumeric = false;
		}

		if (!r.IsMatch(inputPassword)) {
			errorAlphanumericPasswordMessage.SetActive (true);
			checkalphanumeric = false;
		}

		if (!checkalphanumeric) {
			return;
		}


		//chech if in range
		if ((inputUsername.Length < 4 || inputUsername.Length > 16)) {
			errorUsernameLengthMessage.SetActive (true);
			checklength = true;
		}




		if ((inputPassword.Length < 6 || inputPassword.Length > 16)) {
			errorPasswordLengthMessage.SetActive (true);
			checklength = true;
		}





		if (!inputPassword.Equals (inputReEnterPassword)) {
			errorReEnterPasswordMatchMessage.SetActive (true);
			checklength = true;
		}

		if (checklength)
			return;
		


		StartCoroutine (postuserinfo(inputUsername,inputPassword));

	}

	/// <summary>
	/// The register URL.
	/// </summary>
	private String RegisterUrl= "http://localhost/efollowme/register.php";

	/// <summary>
	/// Postuserinfo the specified username and password.
	/// </summary>
	/// <param name="uname">Username.</param>
	/// <param name="pword">Password.</param>
	IEnumerator postuserinfo(string uname,string pword){
	
		var form = new WWWForm(); //here you create a new form connection
		form.AddField("username", uname);
		form.AddField("password", pword);
		var w = new WWW(RegisterUrl, form); //here we create a var called 'w' and we sync with our URL and the form
		yield return w; //we wait for the form to check the PHP file, so our game dont just hang
		if (w.error != null) {
			Debug.Log (w.error); //if there is an error, tell us
			errorNoConnectionMessage.SetActive (true);
		} else {
			if (!string.IsNullOrEmpty (w.text)) { 
				string[] values = w.text.Split (new string[] { "</br>" }, System.StringSplitOptions.None); 

				var attributes = values [0].Split (',');
				for (int i = 1; i < values.Length - 1; i++) {


					attributes = values [i].Split (',');

				}
				Debug.Log (attributes [0]);
				if (attributes [0].Equals ("ERROR")) {
					errorAccountMessage.SetActive (true);
				} else {
					fieldUsername.GetComponent<InputField> ().text = "";
					fieldPassword.GetComponent<InputField>().text= "";
					fieldReEnterPassword.GetComponent<InputField>().text= "";
					successAccountMessage.SetActive (true);
				}
			}
		}


	}

}
