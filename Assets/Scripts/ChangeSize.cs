using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour {

	private Vector3 offset;


	public Vector3 Scale;
	float x;
	float y;
	float z;
	bool small,big;
	// Use this for initialization
	void OnTriggerEnter () {
		x=GameObject.FindWithTag ("Player").transform.localScale.x;
		y=GameObject.FindWithTag ("Player").transform.localScale.y;
		z=GameObject.FindWithTag ("Player").transform.localScale.z;

		if (small == false && big == false) {
			if (Scale.x > x) {
		
				big = true;
			} else if (Scale.x < x) {
		
				small = true;
			} else {
				big = false;
				small = false;
			}
		}

	}

	void Start(){
		offset.x = 0.002f;
		offset.y = 0.002f;
		offset.z = 0.002f;

	}

	void Update(){

		if (small) {
			x = x - offset.x;
			y = y - offset.y;
			z = z - offset.z;
		
			if (Scale.x < x) {
				
				GameObject.FindWithTag ("Player").transform.localScale = new Vector3 (x, y, z);
			} else {
				small = false;
			}
		
		}

		if (big) {
			x = x + offset.x;
			y = y + offset.y;
			z = z + offset.z;
	
			if (Scale.x > x) {

				GameObject.FindWithTag ("Player").transform.localScale = new Vector3 (x, y, z);
			} else {
				big = false;
			}

		}








	}



}
