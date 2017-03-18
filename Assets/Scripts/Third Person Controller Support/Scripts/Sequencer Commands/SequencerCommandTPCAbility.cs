using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Sequencer command TPCAbility(abilityName, [subject], [ignorePriority])
    /// Tries to start an ability on a TPC subject.
    /// 
    /// - abilityName: The name of the ability's script. For example, the "Damage Visualization"
    ///   ability's script name is "DamageVisualization".
    /// - subject: The TPC character to start the ability on. Default: speaker.
    /// - ignorePriority (true/false): If true, ignore ability priority. Default: true.
    /// </summary>
    public class SequencerCommandTPCAbility : SequencerCommand
    {

        public void Start()
        {
            var abilityName = GetParameter(0);
            var subject = GetSubject(1, Sequencer.Speaker);
            var ignorePriority = GetParameterAsBool(2, true);
            if (subject == null)
            {
                if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCAbility(" + GetParameters() + "): Can't find the subject");
            }
            else
            {
                var rbcc = subject.GetComponent<Opsive.ThirdPersonController.RigidbodyCharacterController>();
                if (rbcc == null)
                {
                    if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCAbility(" + GetParameters() + "): The subject doesn't have a TPC RigidbodyCharacterController");
                }
                else
                {
                    Opsive.ThirdPersonController.Abilities.Ability ability = null;
                    foreach (var a in rbcc.Abilities)
                    {
                        if (a != null && string.Equals(a.GetType().Name, abilityName))
                        {
                            ability = a;
                            break;
                        }
                    }
                    if (ability == null)
                    {
                        if (DialogueDebug.LogWarnings) Debug.LogWarning("Dialogue System: Sequencer: TPCAbility(" + GetParameters() + "): Can't find an ability script named " + abilityName);
                    }
                    else
                    {
                        if (DialogueDebug.LogInfo) Debug.Log("Dialogue System: Sequencer: TPCAbility(" + abilityName + ", " + subject + ", ignorePriority=" + ignorePriority + ")");
                        rbcc.TryStartAbility(ability, ignorePriority);
                    }
                }
            }
            Stop();
        }

    }

}
