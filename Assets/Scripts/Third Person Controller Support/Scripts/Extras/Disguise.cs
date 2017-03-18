using UnityEngine;

namespace PixelCrushers.DialogueSystem.ThirdPersonControllerSupport
{

    /// <summary>
    /// This is a customer-requested ability to swap characters.
    /// </summary>
    public class Disguise : Opsive.ThirdPersonController.Abilities.Ability
    {

        [Tooltip("The character to change into. Should be inactive in the scene.")]
        public Opsive.ThirdPersonController.RigidbodyCharacterController disguiseCharacter;

        public override bool CanStartAbility()
        {
            return true;
        }

        public override bool CanStopAbility()
        {
            return true;
        }

        protected override void AbilityStarted()
        {
            base.AbilityStarted();
        }

        protected override void AbilityStopped()
        {
            base.AbilityStopped();
            if (disguiseCharacter == null)
            {
                Debug.LogWarning("Disguise: Disguise Character is unassigned.");
                return;
            }
            var oldBridge = m_Controller.GetComponent<DialogueSystemThirdPersonControllerBridge>();
            if (oldBridge == null)
            {
                Debug.LogWarning("Disguise: Your original character must have a Dialogue System Third Person Controller Bridge component.");
                return;
            }
            var newBridge = disguiseCharacter.GetComponent<DialogueSystemThirdPersonControllerBridge>();
            if (newBridge == null)
            {
                Debug.LogWarning("Disguise: The new character must have a Dialogue System Third Person Controller Bridge component.");
                return;
            }
            Debug.Log("Changing character to: " + disguiseCharacter, disguiseCharacter);

            // Turn off old character:
            oldBridge.OnRecordPersistentData();
            var oldActorName = !string.IsNullOrEmpty(oldBridge.overrideActorName) ? oldBridge.overrideActorName : OverrideActorName.GetActorName(oldBridge.transform);
            CharacterInfo.UnregisterActorTransform(oldActorName, oldBridge.transform);
            oldBridge.gameObject.SetActive(false);

            // Turn on new character:
            disguiseCharacter.gameObject.SetActive(true);
            FindObjectOfType<Opsive.ThirdPersonController.CameraController>().Character = disguiseCharacter.gameObject;
            var newActorName = !string.IsNullOrEmpty(newBridge.overrideActorName) ? newBridge.overrideActorName : OverrideActorName.GetActorName(newBridge.transform);
            CharacterInfo.RegisterActorTransform(newActorName, disguiseCharacter.transform);
            newBridge.OnApplyPersistentData();

            // Jumpstart shield regeneration:
            var health = newBridge.GetComponent<Opsive.ThirdPersonController.Health>();
            if (health != null) health.Damage(0, Vector3.zero, Vector3.zero);
        }

    }
}