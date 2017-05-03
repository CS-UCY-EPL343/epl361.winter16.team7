using UnityEngine;
using System.Collections;

/// <summary>
/// Dontdestroy.
/// </summary>
public class dontdestroy : MonoBehaviour {
	/// <summary>
	/// Awake this instance.
	/// </summary>
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
