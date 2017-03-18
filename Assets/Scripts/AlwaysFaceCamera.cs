using UnityEngine;

namespace PixelCrushers.DialogueSystem.Examples {

	/// <summary>
	/// Component that keeps its game object always facing the main camera.
	/// </summary>
	[AddComponentMenu("Dialogue System/Actor/Always Face Camera")]
	public class AlwaysFaceCamera : MonoBehaviour {
		
		public bool yAxisOnly = false;
		private Camera myCamera;
		private Transform myTransform = null;
		private Camera[] mycams;
		
		void Awake() {
			mycams = Camera.allCameras;
			for (int i = 0; i < mycams.Length; i++) {
				if (mycams[i].tag=="myCamera") {
					myCamera = mycams [i];
					break;
				}
			}
			myTransform = transform;
		}
	
		void Update() {

			if (myCamera == null) {
				mycams = Camera.allCameras;
				for (int i = 0; i < mycams.Length; i++) {
					if (mycams [i].tag == "myCamera") {
						myCamera = mycams [i];
						break;
					}
				}
			}

			if ((myTransform != null) && (myCamera != null)) {
				if (yAxisOnly) {
					myTransform.LookAt(new Vector3(myCamera.transform.position.x, myTransform.position.y, myCamera.transform.position.z));
				} else {
					myTransform.LookAt(myCamera.transform);
				}
			}
		}
		
	}

}
