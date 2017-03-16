using UnityEngine;
using System.Collections;

public class dontdestroy : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
