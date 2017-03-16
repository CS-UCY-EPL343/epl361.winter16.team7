using UnityEngine;

namespace PixelCrushers.DialogueSystem.MenuSystem
{

    /// <summary>
    /// Handles the title menu.
    /// </summary>
    public class TitleMenu : MonoBehaviour
    {

        [Tooltip("Index of title scene in build settings.")]
        public int titleSceneIndex = 0;

        [Tooltip("Index of credits scene in build settings.")]
        public int creditsSceneIndex = 2;

        public SelectablePanel titleMenuPanel;
        public UnityEngine.UI.Button startButton;
        public UnityEngine.UI.Button continueButton;
        public UnityEngine.UI.Button restartButton;

        private SaveHelper m_saveHelper;

        private void Awake()
        {
            m_saveHelper = GetComponent<SaveHelper>();
        }

        private void Start()
        {
            UpdateAvailableButtons();
        }

        public void OnSceneLoaded(int index)
        {
            if (index == titleSceneIndex)
            {
                titleMenuPanel.Open();
                if (InputDeviceManager.deviceUsesCursor) Tools.SetCursorActive(true);
            }
            else
            {
                titleMenuPanel.Close();
            }
        }

        public void UpdateAvailableButtons()
        {
            var hasSavedGame = (m_saveHelper != null) ? m_saveHelper.HasLastSavedGame() : false;
            startButton.gameObject.SetActive(!hasSavedGame);
            continueButton.gameObject.SetActive(hasSavedGame);
            restartButton.gameObject.SetActive(hasSavedGame);
            var selectableToFocus = hasSavedGame ? continueButton.gameObject : startButton.gameObject;
            titleMenuPanel.firstSelected = selectableToFocus;
        }

        public void ShowCreditsScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(creditsSceneIndex);
        }

    }

}