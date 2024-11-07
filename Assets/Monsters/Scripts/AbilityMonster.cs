using UnityEngine;

public class AbilityMonster
{
    public float cooldownTime;
    public float durationTime;
    public MonsterAbilities ability;
    public MonsterAbilityStats stats;

    public bool canCast;
    public AbilityFactory abilityFactory;

    public float cooldown;
    public float duration;
    public float delayTime;

    public AbilityMonster(AbilityFactory factory, Monster monster)
    {
        canCast = true;
        abilityFactory = factory;
        this.cooldownTime = monster.cooldownTime;
        this.durationTime = monster.durationTime;
        this.cooldown = this.cooldownTime;
        this.duration = this.durationTime;
        this.ability = monster.ability;
        AbilityManager.abilityReadyDelegete += Ready;
    }
   
    public bool CanCast()
    {
        canCast = abilityFactory.MonsterAbilityCastConditions(ability.ToString());
        return canCast;
    }

    public void Ready()
    {
        if (CanCast())
        {
            delayTime = 1f;
            this.duration = durationTime;
            AbilityManager.abilityReadyDelegete -= Ready;
            canCast = false;
            AbilityManager.abilityActiveDelegete += Cast;
        }
    }

    public void Cast()
    {
        if (this.duration > 0)
        {
            EffectDuration();
            this.duration -= Time.deltaTime;

        }
        else if (this.duration > -1)
        {
            EffectDuration();
            abilityFactory.DelayExecute();
            this.duration = -2f;
        }

        if (this.duration != -2f) return;
        if (delayTime > 0)
        {
            EffectDuration();
            delayTime -= Time.deltaTime;
        }
        else
        {
            abilityFactory.ExecuteMonsterAbility(ability.ToString());
            Execute();
            EffectExecute();
            DisableEffectDuration();
            AbilityManager.abilityActiveDelegete -= Cast;
            AbilityManager.abilityCooldownDelegete += Cooldown;
            this.duration = durationTime;
        }
    }

    public void Cooldown()
    {
        if (this.cooldown > 0)
        {
            this.cooldown -= Time.deltaTime;
        }
        else
        {
            AbilityManager.abilityCooldownDelegete -= Cooldown;
            this.cooldown = cooldownTime;
            AbilityManager.abilityReadyDelegete += Ready;
            canCast = true;
        }
    }

    public void GetAbilityStats() { }

    public void Activate() { }

    public void EffectDuration() 
    {
        abilityFactory.CastMonsterAbility(ability.ToString());
    }

    public void Action() { }

    public void Execute() { }

    public void EffectExecute() { }

    public void DisableEffectDuration() { }
}
