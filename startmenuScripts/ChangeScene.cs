using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Change scene.
/// </summary>
public class ChangeScene : MonoBehaviour
{
	/// <summary>
	/// Changes to scene.
	/// </summary>
	/// <param name="SceneToChangeTo">Scene to change to.</param>
    public void ChangeToScene(int SceneToChangeTo)
    {
        //Application.LoadLevel(SceneToChangeTo);
        SceneManager.LoadScene(SceneToChangeTo);
    }

}