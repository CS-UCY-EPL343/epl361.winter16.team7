using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.ThirdPersonControllerSupport {

	/// <summary>
	/// Custom inspector for DialogueSystemThirdPersonControllerBridge.
	/// Adds a button to automatically find all ItemTypes in project.
	/// </summary>
	[CustomEditor(typeof(DialogueSystemThirdPersonControllerBridge))]
	public class DialogueSystemThirdPersonControllerBridgeEditor : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			var bridge = target as DialogueSystemThirdPersonControllerBridge;
			if ((bridge != null) && bridge.CompareTag("Player") && GUILayout.Button("Find Item Types")) {
				if (EditorUtility.DisplayDialog("Find Item Types", "Search entire project to populate the Item Types list? (This may take some time.)", "OK", "Cancel")) {
					Undo.RecordObject(bridge, "DialogueSystemThirdPersonControllerBridge");
					FindItemTypes(bridge);
				}
			}
		}

		private void FindItemTypes(DialogueSystemThirdPersonControllerBridge bridge) {
			var list = new List<Opsive.ThirdPersonController.ItemType>();
			string[] filePaths = Directory.GetFiles("Assets", "*.asset", SearchOption.AllDirectories);
			foreach (string filePath in filePaths) {
				string assetPath = filePath.Replace ("\\", "/");
				var itemType = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Opsive.ThirdPersonController.ItemType)) as Opsive.ThirdPersonController.ItemType;
				if (itemType != null) {
					list.Add(itemType);
				}
			}
			bridge.itemTypes = list.ToArray();
		}
	}
}
