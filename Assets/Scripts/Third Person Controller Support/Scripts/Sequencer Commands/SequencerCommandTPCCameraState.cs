using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Sequencer command TPCCameraState(state, [apply]).
    /// Sets the TPC camera state.
    /// 
    /// - state: The name of the camera state (e.g., "Default", "Zoom").
    /// - apply: If true, applies the state. If false, reverts the state. Default: true.
    /// </summary>
    public class SequencerCommandTPCCameraState : SequencerCommand
    {

        public void Start()
        {
            var state = GetParameter(0);
            var apply = GetParameterAsBool(1, true);
            var cameraController = FindObjectOfType<Opsive.ThirdPersonController.CameraController>();
            if (cameraController == null)
            {
                if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCCameraState(" + GetParameters() + "): Can't find the CameraController");
            }
            else
            {
                if (DialogueDebug.LogInfo) Debug.Log("Dialogue System: Sequencer: TPCCameraState(" + GetParameters() + ")");
                cameraController.ChangeState(state, apply);
            }
            Stop();
        }

    }

}
