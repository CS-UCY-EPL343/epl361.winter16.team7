using UnityEngine;
using System.Collections;

/// <summary>
/// My unity singleton.
/// </summary>
public class MyUnitySingleton : MonoBehaviour
{

	/// <summary>
	/// The instance.
	/// </summary>
    private static MyUnitySingleton instance = null;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
    public static MyUnitySingleton Instance
    {
        get { return instance; }
    }
	/// <summary>
	/// Awake this instance.
	/// </summary>
    void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}