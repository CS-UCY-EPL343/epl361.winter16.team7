using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(int SceneToChangeTo)
    {
        //Application.LoadLevel(SceneToChangeTo);
        SceneManager.LoadScene(SceneToChangeTo);
    }
    public void nnn(int i ){}
}