using UnityEngine;
using System.Reflection;
using Opsive.ThirdPersonController;

namespace PixelCrushers.DialogueSystem.ThirdPersonControllerSupport
{

    /// <summary>
    /// Sends a message to a GameObject when TPC interacts with this component.
    /// The typical use is to send "OnUse" to a GameObject with Dialogue System 
    /// triggers that are set to "OnUse".
    /// </summary>
    [AddComponentMenu("Dialogue System/Third Party/Third Person Controller/Dialogue System Interactable Target")]
#if ENABLE_MULTIPLAYER
    public class DialogueSystemInteractableTarget : UnityEngine.Networking.NetworkBehaviour, IInteractableTarget
#else
    public class DialogueSystemInteractableTarget : MonoBehaviour, IInteractableTarget
#endif
    {

        /// <summary>
        /// The player's transform. If unassigned, this component will find the
        /// first GameObject tagged "Player".
        /// </summary>
        public Transform player;

        /// <summary>
        /// The recipient of the message.
        /// </summary>
        public Transform target;

        /// <summary>
        /// The message to send to the target.
        /// </summary>
        public string message = "OnUse";

        /// <summary>
        /// Set true to log debug info.
        /// </summary>
        public bool debug = false;

        // The ID of the InteractableTarget. Used for ability filtering by the character. -1 indicates no ID.
        [SerializeField]
        private int m_ID = -1;

        public virtual void Awake()
        {
            if (target == null) target = transform;
        }

        /// <summary>
        /// Returns the ID of the interactable target.
        /// </summary>
        /// <returns>The ID of the interactable target.</returns>
        public int GetInteractableID()
        {
            return m_ID;
        }

        /// <summary>
        /// Is the target ready to be interacted with?
        /// </summary>
        /// <returns>True if the target is ready to be interacted with.</returns>
        public virtual bool IsInteractionReady()
        {
            return true;
        }

        /// <summary>
        /// Interact with the target by sending the specified message (e.g., "OnUse").
        /// </summary>
        public virtual void Interact()
        {
#if ENABLE_MULTIPLAYER
            var interactable = GetComponent<Interactable>();
            FieldInfo fieldInfo = typeof(Interactable).GetField("m_Interactor", BindingFlags.NonPublic | BindingFlags.Instance);
            var interactor = fieldInfo.GetValue(interactable) as Transform;
            if (interactor == null || (interactor != DialogueSystemThirdPersonControllerBridge.localPlayerBridge.transform))
            {
                return; // Only interact with local player.
            }
#endif
            if (debug) Debug.Log("Dialogue System: Sending '" + message + "' to " + target);
            if (player == null || !player.gameObject.activeInHierarchy)
            {
#if ENABLE_MULTIPLAYER
                player = DialogueSystemThirdPersonControllerBridge.localPlayerBridge.transform;
#endif
                if (player == null)
                {
                    var playerObject = GameObject.FindGameObjectWithTag("Player");
                    if (playerObject != null) player = playerObject.transform;
                }
            }
            target.SendMessage(message, player, SendMessageOptions.RequireReceiver);
        }

    }
}
