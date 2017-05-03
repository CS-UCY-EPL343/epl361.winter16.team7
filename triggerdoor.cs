using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// hackfix to let player go any door if blocked
/// </summary>
public class triggerdoor : MonoBehaviour {
    /// <summary>
    /// Mesh collider
    /// </summary>
	public MeshCollider mc;
    /// <summary>
    /// GameObject destroy
    /// </summary>
	public GameObject go;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if(go.GetComponent<Collider>()!=null)
			Destroy (go.GetComponent<Collider>());
	}
	void OnTriggerExit(){
		if(go.GetComponent<Collider>()==null)
		go.AddComponent <MeshCollider>();
	}
}
