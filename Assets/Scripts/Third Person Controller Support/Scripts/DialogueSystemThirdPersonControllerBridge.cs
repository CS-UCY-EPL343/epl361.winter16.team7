using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PixelCrushers.DialogueSystem.ThirdPersonControllerSupport
{

    /// <summary>
    /// This Dialogue System / Third Person Controller bridge component integrates
    /// a TPC character with the Dialogue System.
    /// </summary>
    [AddComponentMenu("Dialogue System/Third Party/Third Person Controller/Dialogue System Third Person Controller Bridge")]
#if ENABLE_MULTIPLAYER
    public class DialogueSystemThirdPersonControllerBridge : UnityEngine.Networking.NetworkBehaviour
#else
    public class DialogueSystemThirdPersonControllerBridge : MonoBehaviour
#endif
    {

#if ENABLE_MULTIPLAYER
        /// <summary>
        /// In multiplayer, points to this client's local player bridge.
        /// </summary>
        public static DialogueSystemThirdPersonControllerBridge localPlayerBridge;
#endif

        /// <summary>
        /// (Optional) Normally, this component uses the game object's name, or "Player" for 
        /// the player, to match up with the name of the actor in your dialogue database. If your 
        /// actor is named differently in your dialogue database, set this property to the actor's name.
        /// </summary>
        [Tooltip("Use this name in the Actor table.")]
        public string overrideActorName;

        /// <summary>
        /// Set `true` to deactivate TPC controls during conversations.
        /// </summary>
        [Tooltip("Disable TPC controls during conversations.")]
        public bool deactivateDuringConversations = true;

        /// <summary>
        /// Switch to unequipped weapon type during conversations.
        /// </summary>
        [Tooltip("Switch to unequipped weapon type during conversations.")]
        public bool holsterDuringConversations = false;

        [Tooltip("Any additional GameObjects that you want to deactivate during conversations.")]
        public GameObject[] additionalObjectsToDeactivate = new GameObject[0];

        /// <summary>
        /// Set `true` to record stats (health, etc.) in save data.
        /// </summary>
        [Tooltip("Record stats in save data.")]
        public bool recordStats = true;

        /// <summary>
        /// Set `true` to record inventory in save data.
        /// </summary>
        [Tooltip("Record inventory in save data.")]
        public bool recordInventory = true;

        /// <summary>
        /// Set `true` to record position in save data.
        /// </summary>
        [Tooltip("Record position in save data.")]
        public bool recordPosition = true;

        /// <summary>
        /// Set `true to record the character's current level. If so, then on load the 
        /// position is only applied if the current level matches the recorded level.
        /// </summary>
        [Tooltip("Record current level in save data.")]
        public bool recordCurrentLevel = true;

        [Tooltip("Log debug info.")]
        public bool debug = false;

        /// <summary>
        /// The unequipped item type. The TPC Inventory script doesn't expose its item
        /// type, so we need to redeclare it here.
        /// </summary>
        [Tooltip("This should match the Inventory component's Unequipped Item Type.")]
        public Opsive.ThirdPersonController.ItemType unequippedItemType;

        /// <summary>
        /// The item types that are available to Lua functions. You only need to
        /// specify an item types list on one character, usually the player.
        /// </summary>
        [Tooltip("(Player only) Add any item types here that you will reference in Lua.")]
        public Opsive.ThirdPersonController.ItemType[] itemTypes = new Opsive.ThirdPersonController.ItemType[0];

        // Record these behaviours and GameObjects to use if disabling TPC control:
        protected Opsive.ThirdPersonController.RigidbodyCharacterController m_rbcc = null;
        protected Opsive.ThirdPersonController.Inventory m_inventory = null;
        protected Opsive.ThirdPersonController.ControllerHandler m_controllerHandler = null;
        protected Opsive.ThirdPersonController.CameraController m_cameraController = null;
        protected Opsive.ThirdPersonController.UI.AbilityIndicatorMonitor m_abilityIndicatorMonitor = null;

        protected bool m_wasInventoryEnabled;
        protected bool m_wasControllerHandlerEnabled;

        // One instance of this script should contain a list of all item types and
        // set this static global list so the Lua functions and persistent data
        // methods can access item types:
        protected static Opsive.ThirdPersonController.ItemType[] s_globalItemTypes = null;

        public bool showDebug { get { return debug || DialogueDebug.LogInfo; } }

        public bool isPlayer { get { return gameObject.CompareTag("Player"); } }

        #region Start

        public virtual void Start()
        {
            FindComponents();
            RegisterLuaFunctions();
            if (s_globalItemTypes == null && itemTypes != null && itemTypes.Length > 0)
            {
                s_globalItemTypes = itemTypes;
            }
        }

#if ENABLE_MULTIPLAYER
        public override void OnStartLocalPlayer()
        {
            localPlayerBridge = this;
        }
#endif

        protected virtual void FindComponents()
        {
            // Set actor name:
            if (string.IsNullOrEmpty(overrideActorName))
            {
                if (isPlayer && GetComponent<OverrideActorName>() == null)
                {
                    overrideActorName = "Player";
                }
                else
                {
                    overrideActorName = OverrideActorName.GetInternalName(transform);
                }
            }

            // Get TPC components:
            if (m_rbcc == null)
            {
                m_rbcc = GetComponent<Opsive.ThirdPersonController.RigidbodyCharacterController>();
                m_inventory = GetComponent<Opsive.ThirdPersonController.Inventory>();
                m_controllerHandler = GetComponent<Opsive.ThirdPersonController.ControllerHandler>();
                m_cameraController = isPlayer ? FindObjectOfType<Opsive.ThirdPersonController.CameraController>() : null;
                m_abilityIndicatorMonitor = isPlayer ? FindObjectOfType<Opsive.ThirdPersonController.UI.AbilityIndicatorMonitor>() : null;
                if (m_rbcc == null) Debug.LogWarning("Dialogue System: Third Person Controller Bridge can't find a TPC RigidbodyCharacterController on " + name, this);
            }
        }

        public virtual void OnEnable()
        {
            PersistentDataManager.RegisterPersistentData(gameObject);
        }

        public virtual void OnDisable()
        {
            PersistentDataManager.UnregisterPersistentData(gameObject);
        }

        #endregion

        #region Conversations

        /// <summary>
        /// When a conversation starts, if deactivateDuringConversations is true,
        /// disables TPC control.
        /// </summary>
        /// <param name="actor">Actor.</param>
        public virtual void OnConversationStart(Transform actor)
        {
            if (holsterDuringConversations) Holster();
            if (deactivateDuringConversations) DisableTpcControl();
        }

        /// <summary>
        /// When a conversation ends, if deactivateDuringConversations is true,
        /// re-enables TPC control.
        /// </summary>
        /// <param name="actor">Actor.</param>
        public virtual void OnConversationEnd(Transform actor)
        {
            if (deactivateDuringConversations) EnableTpcControl();
            if (holsterDuringConversations) Unholster();
        }

        /// <summary>
        /// Enables TPC control.
        /// </summary>
        public virtual void EnableTpcControl()
        {
            SetTpcControl(true);
        }

        /// <summary>
        /// Disables TPC control.
        /// </summary>
        public virtual void DisableTpcControl()
        {
            SetTpcControl(false);
        }

        /// <summary>
        /// Sets TPC control on or off.
        /// </summary>
        /// <param name="value">If set to <c>true</c>, let TPC control the character.</param>
        public virtual void SetTpcControl(bool value)
        {
            SetTpcControl(value, value, value);
        }

        /// <summary>
        /// Sets TPC control on or off.
        /// </summary>
        /// <param name="value">If set to <c>true</c>, let TPC control the character.</param>
        /// <param name="enableCamera">If set to <c>true</c>, enable the camera</param>
        /// <param name="enableInput">If set to <c>true</c>, enable player input</param>
        public virtual void SetTpcControl(bool enableCharacter, bool enableCamera, bool enableInput)
        {
            if (showDebug) Debug.Log("Dialogue System: " + name + ".SetTpcControl(" + enableCharacter + ")", this);
            FindComponents();
            if (!enableCharacter)
            {
                m_wasInventoryEnabled = (m_inventory != null) && m_inventory.enabled;
                m_wasControllerHandlerEnabled = (m_controllerHandler != null) && m_controllerHandler.enabled;
                m_rbcc.StopMovement();
                m_rbcc.GetComponent<Animator>().SetFloat("Forward Input", 0);
                m_rbcc.GetComponent<Animator>().SetFloat("Horizontal Input", 0);
                m_rbcc.GetComponent<Animator>().SetFloat("Yaw", 0);
            }
            if (isPlayer)
            {
                Opsive.ThirdPersonController.EventHandler.ExecuteEvent<bool>(m_rbcc.gameObject, "OnAllowGameplayInput", enableInput);
                Opsive.ThirdPersonController.EventHandler.ExecuteEvent<GameObject>(m_rbcc.gameObject, "OnCameraAttachCharacter", enableCamera ? gameObject : null);
            }
            if (m_inventory != null) m_inventory.enabled = enableCharacter ? m_wasInventoryEnabled : false;
            if (m_controllerHandler != null) m_controllerHandler.enabled = enableCharacter ? m_wasControllerHandlerEnabled : false;
            if (m_cameraController != null) m_cameraController.enabled = enableCamera;
            if (m_abilityIndicatorMonitor != null) m_abilityIndicatorMonitor.gameObject.SetActive(enableCharacter);
            if (additionalObjectsToDeactivate != null)
            {
                foreach (var obj in additionalObjectsToDeactivate)
                {
                    if (obj != null) obj.SetActive(enableCharacter);
                }
            }
        }

        private Opsive.ThirdPersonController.Item m_holsteredWeapon = null;

        public void Holster()
        {
            if (m_inventory == null) return;
            m_holsteredWeapon = m_inventory.GetCurrentItem(typeof(Opsive.ThirdPersonController.PrimaryItemType));
            m_inventory.EquipItem(m_inventory.UnequippedItemType as Opsive.ThirdPersonController.PrimaryItemType);
        }

        public void Unholster()
        {
            if (m_inventory == null || m_holsteredWeapon == null) return;
            m_inventory.EquipItem(m_holsteredWeapon.ItemType as Opsive.ThirdPersonController.PrimaryItemType);
        }

        #endregion

        #region Save System

        /// <summary>
        /// Saves the TPC's current position & inventory to Lua.
        /// </summary>
        public void OnRecordPersistentData()
        {
            if (recordPosition)
            {
                SavePosition();
            }
            if (recordInventory)
            {
                SaveInventory();
            }
            if (recordStats)
            {
                SaveStats();
            }
        }

        /// <summary>
        /// Retrieves position & inventory from Lua and applies it to the TPC.
        /// </summary>
        public void OnApplyPersistentData()
        {
            if (recordPosition)
            {
                LoadPosition();
            }
            if (recordInventory)
            {
                LoadInventory();
            }
            if (recordStats)
            {
                LoadStats();
            }
        }

        private string GetSanitizedLevelName()
        {
#if UNITY_4_6 || UNITY_4_7 || UNITY || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            return PersistentPositionData.SanitizeLevelName(Application.loadedLevelName);
#else
            return PersistentPositionData.SanitizeLevelName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
#endif
        }

        public void SavePosition()
        {
            string positionString = GetPositionString();
            if (showDebug) Debug.Log("Dialogue System: " + overrideActorName + " saving position.", this);
            if (recordCurrentLevel)
            {
                DialogueLua.SetActorField(overrideActorName, "Position_" + GetSanitizedLevelName(), positionString);
            }
            else
            {
                DialogueLua.SetActorField(overrideActorName, "Position", positionString);
            }
        }

        public void LoadPosition()
        {
            string s = string.Empty;
            if (recordCurrentLevel)
            {
                s = DialogueLua.GetActorField(overrideActorName, "Position_" + GetSanitizedLevelName()).AsString;
            }
            else
            {
                s = DialogueLua.GetActorField(overrideActorName, "Position").AsString;
            }
            if (!string.IsNullOrEmpty(s))
            {
                if (showDebug) Debug.Log("Dialogue System: " + overrideActorName + " loading position.", this);
                ApplyPositionString(s);
            }
        }

        private string GetPositionString()
        {
            string optionalLevelName = recordCurrentLevel ? DialogueLua.DoubleQuotesToSingle("," + GetSanitizedLevelName()) : string.Empty;
            var rbccPos = transform.position;
            var rbccRot = transform.rotation;
            var camController = FindObjectOfType<Opsive.ThirdPersonController.CameraController>();
            if (camController == null && DialogueDebug.LogWarnings)
            {
                Debug.LogWarning("Dialogue System: Can't find TPC CameraController.", this);
            }
            var camPos = (camController != null) ? camController.transform.position : rbccPos;
            var camRot = (camController != null) ? camController.transform.rotation : rbccRot;
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}{14}",
                new System.Object[] {
                rbccPos.x, rbccPos.y, rbccPos.z,
                rbccRot.x, rbccRot.y, rbccRot.z, rbccRot.w,
                camPos.x, camPos.y, camPos.z,
                camRot.x, camRot.y, camRot.z, camRot.w,
                optionalLevelName });
        }

        private void ApplyPositionString(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Equals("nil")) return;
            string[] tokens = s.Split(',');
            if ((14 <= tokens.Length) && (tokens.Length <= 15))
            {
                if (recordCurrentLevel)
                {
                    if ((tokens.Length == 15) && !string.Equals(tokens[14], GetSanitizedLevelName()))
                    {
                        return; // If this is not the recorded level, don't apply position.
                    }
                }
                float[] values = new float[14];
                for (int i = 0; i < 14; i++)
                {
                    values[i] = 0;
                    float.TryParse(tokens[i], out values[i]);
                }
                var rbcc = GetComponent<Opsive.ThirdPersonController.RigidbodyCharacterController>();
                if (rbcc != null)
                {
                    rbcc.SetPosition(new Vector3(values[0], values[1], values[2]));
                    rbcc.SetRotation(new Quaternion(values[3], values[4], values[5], values[6]));
                }
                if (isPlayer)
                {
                    var camController = FindObjectOfType<Opsive.ThirdPersonController.CameraController>();
                    if (camController != null)
                    {
                        camController.transform.position = new Vector3(values[7], values[8], values[9]);
                        camController.ImmediatePosition(new Quaternion(values[10], values[11], values[12], values[13]));
                    }
                }
            }
        }

        public void SaveInventory()
        {
            if (showDebug) Debug.Log("Dialogue System: " + overrideActorName + " saving inventory.", this);
            if (m_inventory == null) return;
            var primary = m_inventory.GetCurrentItem(typeof(Opsive.ThirdPersonController.PrimaryItemType));
            var secondary = m_inventory.GetCurrentItem(typeof(Opsive.ThirdPersonController.SecondaryItemType));
            if (s_globalItemTypes == null)
            {
                if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: No character has provided the Item Types list. Inspect the bridge component on a character (usually the player) and click 'Find Item Types'.", this);
                return;
            }
            var sb = new StringBuilder();
            foreach (var itemType in s_globalItemTypes)
            {
                try
                {
                    if (!m_inventory.HasItem(itemType)) continue;
                    var loadedCount = m_inventory.GetItemCount(itemType, true);
                    var unloadedCount = m_inventory.GetItemCount(itemType, false);
                    int equipValue = 0;
                    if ((primary != null) && (primary.ItemType.ID == itemType.ID))
                    {
                        equipValue = 1;
                    }
                    else if ((secondary != null) && (secondary.ItemType.ID == itemType.ID))
                    {
                        equipValue = 2;
                    }
                    sb.AppendFormat("{0};{1};{2};{3};", itemType.ID, loadedCount, unloadedCount, equipValue);
                    if (showDebug) Debug.Log("   Saving " + itemType.name + ", loaded=" + loadedCount +
                               ", unloaded=" + unloadedCount + ", equip=" + equipValue);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    // Skip item if the inventory component doesn't know about it.
                }
            }
            DialogueLua.SetActorField(overrideActorName, "InventoryList", sb.ToString());
        }

        public void LoadInventory()
        {
            if (showDebug) Debug.Log("Dialogue System: " + overrideActorName + " loading inventory.", this);
            if (m_inventory == null) return;
            var inventoryString = DialogueLua.GetActorField(overrideActorName, "InventoryList").AsString;
            if (string.IsNullOrEmpty(inventoryString)) return;
            m_inventory.RemoveAllItems(false);
            var values = new Queue<string>(inventoryString.Split(new char[] { ';' }));
            var unequippedItemID = (unequippedItemType == null) ? -1 : unequippedItemType.ID;
            while (values.Count >= 4)
            {
                try
                {
                    var itemID = Tools.StringToInt(values.Dequeue());
                    var loadedCount = Tools.StringToInt(values.Dequeue());
                    var unloadedCount = Tools.StringToInt(values.Dequeue());
                    var equipValue = Tools.StringToInt(values.Dequeue());
                    var equip = (equipValue != 0);
                    var immediateActivation = (equipValue != 1);
                    var itemType = GetItemType(itemID);
                    var isUnequippedItem = itemType == null || itemType.ID == unequippedItemID;
                    if (isUnequippedItem) continue;
                    m_inventory.PickupItem(itemID, unloadedCount, equip, immediateActivation);
                    m_inventory.SetItemCount(itemType, loadedCount, unloadedCount);
                    if (showDebug) Debug.Log("   Adding " + itemType.name +
                                             ", loaded=" + loadedCount + ", unloaded=" + unloadedCount);
                }
                catch (System.NullReferenceException e)
                {
                    Debug.LogError("Can't load item: " + e.Message);
                }
            }
        }

        public void SaveStats()
        {
            var health = GetComponent<Opsive.ThirdPersonController.CharacterHealth>();
            if (health == null) return;
            DialogueLua.SetActorField(overrideActorName, "CurrentHealth", health.CurrentHealth);
            DialogueLua.SetActorField(overrideActorName, "CurrentShield", health.CurrentShield);
        }

        public void LoadStats()
        {
            var health = GetComponent<Opsive.ThirdPersonController.CharacterHealth>();
            if (health == null) return;
            var savedHealth = DialogueLua.GetActorField(overrideActorName, "CurrentHealth").AsFloat;
            var savedShield = DialogueLua.GetActorField(overrideActorName, "CurrentShield").AsFloat;
            health.SetHealthAmount(savedHealth);
            health.SetShieldAmount(savedShield);
        }

        #endregion

        #region Lua

        private static bool hasRegisteredLuaFunctions = false;

        /// <summary>
        /// Registers the TPC Lua functions with the Dialogue System.
        /// </summary>
        public static void RegisterLuaFunctions()
        {
            if (hasRegisteredLuaFunctions) return;
            hasRegisteredLuaFunctions = true;

            // Health:
            Lua.RegisterFunction("tpcGetHealth", null, SymbolExtensions.GetMethodInfo(() => tpcGetHealth(string.Empty)));
            Lua.RegisterFunction("tpcGetShield", null, SymbolExtensions.GetMethodInfo(() => tpcGetShield(string.Empty)));
            Lua.RegisterFunction("tpcSetInvincible", null, SymbolExtensions.GetMethodInfo(() => tpcSetInvincible(string.Empty, true)));
            Lua.RegisterFunction("tpcHeal", null, SymbolExtensions.GetMethodInfo(() => tpcHeal(string.Empty, (double)0)));
            Lua.RegisterFunction("tpcSetMaxHealth", null, SymbolExtensions.GetMethodInfo(() => tpcSetMaxHealth(string.Empty, (double)0)));
            Lua.RegisterFunction("tpcSetMaxShield", null, SymbolExtensions.GetMethodInfo(() => tpcSetMaxShield(string.Empty, (double)0)));
            Lua.RegisterFunction("tpcSetShieldRegenerativeAmount", null, SymbolExtensions.GetMethodInfo(() => tpcSetShieldRegenerativeAmount(string.Empty, (double)0)));

            // Inventory:
            Lua.RegisterFunction("tpcHasCurrentItem", null, SymbolExtensions.GetMethodInfo(() => tpcHasCurrentItem(string.Empty, string.Empty, true)));
            Lua.RegisterFunction("tpcGetItemCount", null, SymbolExtensions.GetMethodInfo(() => tpcGetItemCount(string.Empty, string.Empty, true)));
            Lua.RegisterFunction("tpcPickupItem", null, SymbolExtensions.GetMethodInfo(() => tpcPickupItem(string.Empty, string.Empty, (double)0, true, true)));
            Lua.RegisterFunction("tpcRemoveItem", null, SymbolExtensions.GetMethodInfo(() => tpcRemoveItem(string.Empty, string.Empty, true)));
            Lua.RegisterFunction("tpcRemoveAllItems", null, SymbolExtensions.GetMethodInfo(() => tpcRemoveAllItems(string.Empty)));
        }

        //--- Health:

        public static double tpcGetHealth(string characterName)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            return (health != null) ? health.CurrentHealth : 0;
        }

        public static double tpcGetShield(string characterName)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            return (health != null) ? health.CurrentShield : 0;
        }

        public static void tpcSetInvincible(string characterName, bool value)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            if (health != null) health.Invincible = value;
        }

        public static void tpcHeal(string characterName, double amount)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            if (health != null) health.Heal((float)amount);
        }

        public static void tpcSetMaxHealth(string characterName, double amount)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            if (health != null) health.MaxHealth = (float)amount;
        }

        public static void tpcSetMaxShield(string characterName, double amount)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            if (health != null) health.MaxShield = (float)amount;
        }

        public static void tpcSetShieldRegenerativeAmount(string characterName, double amount)
        {
            var health = GetCharacterComponent<Opsive.ThirdPersonController.Health>(characterName);
            if (health != null) health.ShieldRegenerativeAmount = (float)amount;
        }

        //--- Inventory:

        public static bool tpcHasCurrentItem(string characterName, string itemName, bool primaryItem)
        {
            var inventory = GetCharacterComponent<Opsive.ThirdPersonController.Inventory>(characterName);
            var currentItem = (inventory != null) ? inventory.GetCurrentItem(primaryItem ? typeof(Opsive.ThirdPersonController.PrimaryItemType) : typeof(Opsive.ThirdPersonController.SecondaryItemType)) : null;
            var checkItem = GetItemType(itemName);
            return (currentItem != null && checkItem != null) ? currentItem.ItemType.Equals(checkItem) : false;
        }

        public static double tpcGetItemCount(string characterName, string itemName, bool loadedCount)
        {
            var inventory = GetCharacterComponent<Opsive.ThirdPersonController.Inventory>(characterName);
            return (inventory != null) ? inventory.GetItemCount(GetItemType(itemName), loadedCount) : 0;
        }

        public static void tpcPickupItem(string characterName, string itemName, double amount, bool equip, bool immediateActivation)
        {
            var inventory = GetCharacterComponent<Opsive.ThirdPersonController.Inventory>(characterName);
            var item = GetItemType(itemName);
            if (inventory != null && item != null)
            {
                inventory.PickupItem(item.ID, (int)amount, equip, immediateActivation);
            }
        }

        public static void tpcRemoveItem(string characterName, string itemName, bool immediateRemoval)
        {
            var inventory = GetCharacterComponent<Opsive.ThirdPersonController.Inventory>(characterName);
            var item = GetItemType(itemName);
            if (inventory != null && item != null)
            {
                inventory.RemoveItem(item, true, immediateRemoval);
            }
        }

        public static void tpcRemoveAllItems(string characterName)
        {
            var inventory = GetCharacterComponent<Opsive.ThirdPersonController.Inventory>(characterName);
            if (inventory != null)
            {
                inventory.RemoveAllItems();
            }
        }

        private static Opsive.ThirdPersonController.ItemType GetItemType(string itemName)
        {
            if (s_globalItemTypes != null)
            {
                for (int i = 0; i < s_globalItemTypes.Length; i++)
                {
                    if (string.Equals(itemName, s_globalItemTypes[i].name)) return s_globalItemTypes[i];
                }
            }
            if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Can't find item '" + itemName + "'. Did you assign the item type to the Dialogue System Third Person Controller Bridge component?");
            return null;
        }

        private static Opsive.ThirdPersonController.ItemType GetItemType(int itemID)
        {
            if (s_globalItemTypes != null)
            {
                for (int i = 0; i < s_globalItemTypes.Length; i++)
                {
                    if (itemID == s_globalItemTypes[i].ID) return s_globalItemTypes[i];
                }
            }
            if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Can't find item ID '" + itemID + "'. Did you assign the item type to the Dialogue System Third Person Controller Bridge component?");
            return null;
        }

        //--- Utility functions:

        /// <summary>
        /// Finds a character by its GameObject name.
        /// </summary>
        /// <returns>The character GameObject.</returns>
        /// <param name="characterName">Name of the GameObject.</param>
        public static GameObject FindCharacter(string characterName)
        {
            return string.IsNullOrEmpty(characterName) ? GameObject.FindGameObjectWithTag("Player") : Tools.GameObjectHardFind(characterName);
        }

        /// <summary>
        /// Gets a component on a GameObject with the specified name.
        /// </summary>
        /// <returns>The component, or `null` if not found.</returns>
        /// <param name="characterName">Name of the GameObject.</param>
        public static T GetCharacterComponent<T>(string characterName) where T : Component
        {
            var go = FindCharacter(characterName);
            var component = (go != null) ? go.GetComponentInChildren<T>() : null;
            if ((component == null) && DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Can't find " + typeof(T).Name + " on " + characterName);
            return component;
        }

        #endregion

    }
}
