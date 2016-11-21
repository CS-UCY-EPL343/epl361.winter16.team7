using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class playvideo : MonoBehaviour {
	public MovieTexture movie;
	private AudioSource audio1; 

	void Start () {
		movie.loop = true; 
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio1 = GetComponent<AudioSource> ();
		audio1.clip = movie.audioClip;
		movie.Play ();
		audio1.Play ();
	
	}
	
	 
	void Update () {
		
	
	}
}
