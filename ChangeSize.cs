using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

/// <summary>
/// This class changes the size of the
/// character in order to be smaller
/// in some places and bigger to some
/// other place. Also it saves the size.
/// e.g small at school, big at town
/// </summary>
public class ChangeSize : MonoBehaviour {

	/// <summary>
	/// The offset.
	/// </summary>
	private Vector3 offset;

	/// <summary>
	/// running scale.
	/// </summary>
	public Vector3 Scale;
	/// <summary>
	/// Player scale x.
	/// </summary>
	float x;
	/// <summary>
	/// Player scale y.
	/// </summary>
	float y;
	/// <summary>
	/// Player scale z.
	/// </summary>
	float z;
	/// <summary>
	/// must go small.
	/// </summary>
	bool small;
	/// <summary>
	/// must go big.
	/// </summary>
	bool big;



	void OnTriggerEnter () {
		decideAction ();

	}

	/// <summary>
	/// Decides if the player will 
	/// get smaller or get bigger.
	/// </summary>
	void decideAction(){
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
		changeSizeSlowly();	
}

	/// <summary>
	/// Changes the size slowly in order
	/// to be unoticable.
	/// </summary>
	void changeSizeSlowly(){

		if (small) {
			x = x - offset.x;

			y = y - offset.y;

			z = z - offset.z;


			if (Scale.x < x) {

				GameObject.FindWithTag ("Player").transform.localScale = new Vector3 (x, y, z);
				DialogueLua.SetVariable("PlayerX", GameObject.FindWithTag ("Player").transform.localScale.x);
				DialogueLua.SetVariable("PlayerY", GameObject.FindWithTag ("Player").transform.localScale.y);
				DialogueLua.SetVariable("PlayerZ", GameObject.FindWithTag ("Player").transform.localScale.z);
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
				DialogueLua.SetVariable("PlayerX", GameObject.FindWithTag ("Player").transform.localScale.x);
				DialogueLua.SetVariable("PlayerY", GameObject.FindWithTag ("Player").transform.localScale.y);
				DialogueLua.SetVariable("PlayerZ", GameObject.FindWithTag ("Player").transform.localScale.z);
			} else {
				big = false;
			}

		}








	}



	}