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


public class Login : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    public int a;
    public GameSettings gameSettings;
    public void button()
    {
        if (a == 1)
        {

           // Application.LoadLevel(3);
            SceneManager.LoadScene(3);

        }
    }
 
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }

        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if ((Password != "" && Username != "") && (Password.Length >= 6 && Password.Length <= 16))
            {
                //Application.LoadLevel(3);
                SceneManager.LoadScene(3);
            }



        }
        if ((Password != ""  && Username != "") && (Password.Length>=6 && Password.Length <=16))
        {
            a = 1;
        }

    }
}
