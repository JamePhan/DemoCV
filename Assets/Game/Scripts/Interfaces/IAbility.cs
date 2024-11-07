using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IAbility
{
    AbilitySO AbilitySO { get; set; }
    AbilityStatsSO AbilityStatsSO { get; set; }
    string Name { get; set; }
    float CooldownTime { get; set; }
    float DurationTime { get; set; }
    string Keyboard { get; set; }

    float cooldown { get; set; }

    void Init(Image imgSkill, Button button, GameObject reload, Image image, TextMeshProUGUI text);
    void LoadAbilityData();
    void Ready();
    void Cast();
    void Cooldown();

    void Activate();
    // Action() and Effect() - both in Activate()
    void Action();

    /// <summary>
    /// Execute calls after duration time end
    /// </summary>
    void Execute();
   
    //VFX
    void EffectDuration();

    void DisableEffectDuration();

    void EffectExecute();

}
