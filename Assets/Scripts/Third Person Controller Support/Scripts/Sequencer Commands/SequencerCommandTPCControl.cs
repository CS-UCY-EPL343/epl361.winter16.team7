using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Sequencer command TPCControl(control, camera, input)
    /// Sets TPC control on the player character.
    /// 
    /// - control: true or false. Allow TPC to control the player. Defaults to true.
    /// - camera: Enable the camera to follow the character. Defaults to the value of the first parameter.
    /// - input: Allow the player to provide input control to TPC. Defaults to the value of the first parameter.
    /// </summary>
    public class SequencerCommandTPCControl : SequencerCommand
    {

        public void Start()
        {
            var enableControl = GetParameterAsBool(0, true);
            var enableCamera = GetParameterAsBool(1, enableControl);
            var enableInput = GetParameterAsBool(2, enableControl);
            var playerGameObject = GameObject.FindWithTag("Player");
            var subject = (playerGameObject != null) ? playerGameObject.transform : null;                    
            if (subject == null)
            {
                if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCControl(" + GetParameters() + "): Can't find the player");
            }
            else
            {
                var rbcc = subject.GetComponent<Opsive.ThirdPersonController.RigidbodyCharacterController>();
                if (rbcc == null)
                {
                    if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCControl(" + GetParameters() + "): The player doesn't have a TPC RigidbodyCharacterController", subject);
                }
                else
                {
                    var bridge = subject.GetComponent<PixelCrushers.DialogueSystem.ThirdPersonControllerSupport.DialogueSystemThirdPersonControllerBridge>();
                    if (bridge == null)
                    {
                        if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCControl(" + GetParameters() + "): The player doesn't have a DialogueSystemThirdPersonControllerBridge");
                    }
                    else
                    {
                        if (DialogueDebug.LogInfo) Debug.Log("Dialogue System: Sequencer: TPCControl(" + subject + ", TPC control=" + enableControl + ", attach camera=" + enableCamera + ", player input=" + enableInput + ")");
                        bridge.SetTpcControl(enableControl, enableInput, enableCamera);
                    }
                }
            }
            Stop();
        }

    }

}
