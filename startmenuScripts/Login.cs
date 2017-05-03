using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;


/// <summary>
/// Login Manager.
/// </summary>
public class Login : MonoBehaviour
{

	/// <summary>
	/// The field username.
	/// </summary>
    public GameObject fieldUsername;
	/// <summary>
	/// The field password.
	/// </summary>
    public GameObject fieldPassword;

	/// <summary>
	/// The input username.
	/// </summary>
	private string inputUsername;
	/// <summary>
	/// The input password.
	/// </summary>
	private string inputPassword;
    
	/// <summary>
	/// The game settings reference.
	/// </summary>
    public GameSettings gameSettings;

 
 
    // Use this for initialization
    void Start()
    {
		fieldUsername.GetComponent<InputField> ().Select ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
			if (fieldUsername.GetComponent<InputField> ().isFocused) {
				fieldPassword.GetComponent<InputField> ().Select ();
			} else if (fieldPassword.GetComponent<InputField> ().isFocused) {
				fieldUsername.GetComponent<InputField> ().Select ();
			}
        }

        
        if (Input.GetKeyDown(KeyCode.Return))
        {
			login ();
        }
       


    }

	/// <summary>
	/// The username logged.
	/// </summary>
	public static string username_logged=null;
	/// <summary>
	/// The password logged.
	/// </summary>
	public static string password_logged=null;
	/// <summary>
	/// The save logged.
	/// </summary>
	public static string save_logged=null;

	/// <summary>
	/// The login URL.
	/// </summary>
	private string LoginUrl="http://localhost/efollowme/login.php";

	/// <summary>
	/// Getuserinfo the specified username and password.
	/// </summary>
	/// <param name="uname">Username.</param>
	/// <param name="pword">Password.</param>
	IEnumerator getuserinfo(string uname,string pword)
	{
		
		var form = new WWWForm(); //here you create a new form connection
		form.AddField("username", uname);
		form.AddField("password", pword);
		var w = new WWW(LoginUrl, form); //here we create a var called 'w' and we sync with our URL and the form
		yield return w; //we wait for the form to check the PHP file, so our game dont just hang
		if (w.error != null)
		{
			Debug.Log(w.error); //if there is an error, tell us
			errorNoConnectionMessage.SetActive(true);
		}
		else
		{
			if (!string.IsNullOrEmpty (w.text)) { 
				string[] values = w.text.Split (new string[] { "</br>" }, System.StringSplitOptions.None); 

				var attributes = values [0].Split ('@');
				for (int i = 1; i < values.Length - 1; i++) {
					

					attributes = values [i].Split ('@');
				}

				/*
				Debug.Log ("username:" + attributes [0]);
				Debug.Log ("password:" + attributes [1]);
				Debug.Log ("save:" + attributes [2]);
				*/

				string encrypted_passwd = registerLuaSHA256.sha256_hash (pword);

				if (uname.Equals (attributes [0]) && encrypted_passwd.Equals (attributes [1])) {
					username_logged = uname;
					password_logged = encrypted_passwd;
					save_logged = attributes [2];
					//Debug.Log (save_logged);
					SceneManager.LoadScene (1);
				} else {
					Debug.Log ("wrong account info");
					errorAccountMessage.SetActive (true);
				}
				
			} else {
				Debug.Log ("wrong account info");
				errorAccountMessage.SetActive (true);
			}

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
	/// The error account message.
	/// </summary>
	public GameObject errorAccountMessage;
	/// <summary>
	/// The error no connection message.
	/// </summary>
	public GameObject errorNoConnectionMessage;
	/// <summary>
	/// The error alphanumeric username message.
	/// </summary>
	public GameObject errorAlphanumericUsernameMessage;
	/// <summary>
	/// The error alphanumeric password message.
	/// </summary>
	public GameObject errorAlphanumericPasswordMessage;
	/// <summary>
	/// Login and checks.
	/// </summary>
	public void login(){

		errorAlphanumericUsernameMessage.SetActive (false);
		errorAlphanumericPasswordMessage.SetActive (false);
		errorPasswordLengthMessage.SetActive (false);
		errorUsernameLengthMessage.SetActive (false);
		errorAccountMessage.SetActive (false);
		errorNoConnectionMessage.SetActive(false);

		inputUsername = fieldUsername.GetComponent<InputField>().text;
		inputPassword = fieldPassword.GetComponent<InputField>().text;

		bool checklength = false;

		Regex r = new Regex("^[a-zA-Z0-9]*$");

		//check if is alphanumeric
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

		//check if in range
		if ((inputPassword.Length < 6 || inputPassword.Length > 16)) {
			errorPasswordLengthMessage.SetActive (true);
			checklength = true;
		}



		if ((inputUsername.Length < 4 || inputUsername.Length > 16)) {
			errorUsernameLengthMessage.SetActive (true);
			checklength = true;
		}


		if (checklength)
			return;




			StartCoroutine (getuserinfo(inputUsername,inputPassword));
			

		



	}
}
