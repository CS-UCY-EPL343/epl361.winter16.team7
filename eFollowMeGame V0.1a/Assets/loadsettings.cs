using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class loadsettings : MonoBehaviour {
    public GameSettings gameSettings = new GameSettings();
    public AudioSource musicSource;
    // Use this for initialization
    void Start () {
        LoadSettings();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        musicSource.volume = gameSettings.musicVolume;
        QualitySettings.antiAliasing = gameSettings.antialiasing / 2;
        QualitySettings.vSyncCount = gameSettings.vSync;
        QualitySettings.masterTextureLimit = gameSettings.textureQuality;
        Screen.fullScreen = gameSettings.fullscreen;
        Resolution[] resolutions = Screen.resolutions;
        Screen.SetResolution(resolutions[gameSettings.resolutionIndex].width, resolutions[gameSettings.resolutionIndex].height, Screen.fullScreen);
    }
}
