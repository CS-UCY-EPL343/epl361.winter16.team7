using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerdoor : MonoBehaviour {
	public MeshCollider mc;
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
