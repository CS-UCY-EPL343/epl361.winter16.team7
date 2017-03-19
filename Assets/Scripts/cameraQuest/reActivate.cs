using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reActivate : MonoBehaviour {

	public float sec = 1f;
	public RawImage obj;
	void Start()
	{
		if (obj.IsActive()== true)
			obj.enabled = false;

		StartCoroutine(LateCall());
	}

	IEnumerator LateCall()
	{

		yield return new WaitForSeconds(sec+1);

		obj.enabled = true;
		yield return new WaitForSeconds(sec/10);
		obj.enabled = false;
		//gameObject.SetActive(false);
		//Do Function here...
	}
}
