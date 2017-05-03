using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
[System.Serializable]
/// <summary>
/// Setting manager for start menu.
/// </summary>
public class SettingManager: MonoBehaviour {
	/// <summary>
	/// The fullscreen toggle.
	/// </summary>
	public Toggle fullscreenToggle;
	/// <summary>
	/// The resolution dropdown.
	/// </summary>
	public Dropdown resolutionDropdown;
	/// <summary>
	/// The texture quality dropdown.
	/// </summary>
	public Dropdown textureQualityDropdown;
	//public Dropdown antialiasingDropdown;
	//public Dropdown vSyncDopwdown;
	/// <summary>
	/// The music volume slider.
	/// </summary>
    public Slider musicVolumeSlider;
	/// <summary>
	/// The apply button.
	/// </summary>
	public Button applyButton;

	/// <summary>
	/// The music source.
	/// </summary>
    private AudioSource musicSource;
	/// <summary>
	/// The resolutions.
	/// </summary>
    public Resolution[] resolutions;
	/// <summary>
	/// The game settings.
	/// </summary>
	public GameSettings gameSettings;

	void OnEnable(){

		gameSettings= new GameSettings();

		fullscreenToggle.onValueChanged.AddListener(delegate{OnFullscreenToggle();});
		resolutionDropdown.onValueChanged.AddListener(delegate{OnResolutionChange();});
		textureQualityDropdown.onValueChanged.AddListener(delegate{OnTextureQualityChange();});
	//	antialiasingDropdown.onValueChanged.AddListener(delegate{OnAntialiasingChange();});
	//	vSyncDopwdown.onValueChanged.AddListener(delegate{OnVSyncChange();});
		musicVolumeSlider.onValueChanged.AddListener(delegate{OnMusicVolumeChange();});
//		applyButton.onClick.AddListener (delegate {OnApplyButtonClick ();});


		musicSource = Object.FindObjectOfType<AudioSource>();
		musicVolumeSlider.value = musicSource.volume;

		textureQualityDropdown.value = QualitySettings.GetQualityLevel();
		fullscreenToggle.isOn = Screen.fullScreen;
		resolutions = Screen.resolutions; 
		foreach (Resolution resolution in resolutions) {
			resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
		
		}

	}

	/// <summary>
	/// Raises the fullscreen toggle event.
	/// </summary>
	public void OnFullscreenToggle(){

		gameSettings.fullscreen= Screen.fullScreen = fullscreenToggle.isOn;

	}
	/// <summary>
	/// Raises the resolution change event.
	/// </summary>
	public void OnResolutionChange(){
		Screen.SetResolution (resolutions [resolutionDropdown.value].width, resolutions [resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;

    }
	/// <summary>
	/// Raises the texture quality change event.
	/// </summary>
	public void OnTextureQualityChange(){

		QualitySettings.SetQualityLevel (textureQualityDropdown.value, true);
		gameSettings.textureQuality = textureQualityDropdown.value;

	}

	/*public void OnAntialiasingChange(){

		QualitySettings.antiAliasing = gameSettings.antialiasing=2*antialiasingDropdown.value;

	}

	public void OnVSyncChange(){
		QualitySettings.vSyncCount = gameSettings.vSync = vSyncDopwdown.value;

	}
*/
	/// <summary>
	/// Raises the music volume change event.
	/// </summary>
	public void OnMusicVolumeChange(){
        musicSource = Object.FindObjectOfType<AudioSource>();
        musicSource.volume =gameSettings.musicVolume= musicVolumeSlider.value;
	}



















}
