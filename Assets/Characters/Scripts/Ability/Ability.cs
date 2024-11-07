using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ability : IAbility
{
    public AbilitySO AbilitySO { get; set; }
    public AbilityStatsSO AbilityStatsSO { get; set; }
    public string Name { get; set; }
    public float DurationTime { get; set; }
    public float CooldownTime { get; set; }
    public string Keyboard { get ; set ; }

    public float cooldown { get; set; }
    public float duration;

    public bool canCast;

    public GameObject reload;
    public Image img_reload;
    public TextMeshProUGUI text_reload;
    public Image imgSkill;

    public void Init(Image imgSkill, Button button, GameObject reload, Image image, TextMeshProUGUI text)
    {
        this.imgSkill = imgSkill;
        LoadAbilityData();
        button.onClick.AddListener(Ready);
        this.reload = reload;
        this.img_reload = image;
        this.text_reload = text;
        
        canCast = true;
    }

    public void LoadAbilityData()
    {
        this.AbilitySO = Resources.Load<AbilitySO>("Abilities/" + this.GetType().Name);
        this.Name = AbilitySO.nameAbility;
        this.imgSkill.sprite = AbilitySO.skillSprite;
        this.CooldownTime = AbilitySO.cooldownTime;
        this.cooldown = this.CooldownTime;
        this.DurationTime = AbilitySO.durationTime;
        this.duration = this.DurationTime;
        this.Keyboard = AbilitySO.keyBoard;
        this.AbilityStatsSO = Resources.Load<AbilityStatsSO>("AbilityStats/" + this.GetType().Name);
    }

    public void Ready()
    {
        if (canCast == true)
        {
            Activate();
            duration = DurationTime;
            AbilityManager.abilityActiveDelegete += Cast;
            canCast = false;
        }
    }

    public void Cast()
    {
        if (duration > 0)
        {
            EffectDuration();
            duration -= Time.deltaTime;
        }
        else
        {
            Execute();
            EffectExecute();
            DisableEffectDuration();
            AbilityManager.abilityActiveDelegete -= Cast;
            AbilityManager.abilityCooldownDelegete += Cooldown;
            duration = DurationTime;
        }
    }

    public void Cooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            TurnOnCooldown_UI();
            UpdateCooldown_UI(cooldown);
        }
        else
        {
            TurnOffCooldown_UI();
            AbilityManager.abilityCooldownDelegete -= Cooldown;
            cooldown = CooldownTime;
            canCast = true;
        }
    }

    public void TurnOnCooldown_UI() => reload.SetActive(true);
    public void TurnOffCooldown_UI() => reload.SetActive(false);

    public void UpdateCooldown_UI(float cooldown)
    {
        float fillAmount = (1 / CooldownTime) * cooldown;
        img_reload.fillAmount = fillAmount;
        text_reload.text = Mathf.FloorToInt(cooldown) + "";
    }

    public virtual void GetAbilityStats() { }

    public virtual void Activate() { }

    public virtual void EffectDuration() { }

    public virtual void Action() { }

    public virtual void Execute() { }
    
    public virtual void EffectExecute() { }

    public virtual void DisableEffectDuration() { }
}
