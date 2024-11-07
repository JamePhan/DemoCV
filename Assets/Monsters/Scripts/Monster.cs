using UnityEngine;

public class Monster
{
    public Color skin;
    public int health;
    public int damage;
    public int cooldownAttack;
    public float moveSpeed;
    public int experience;

    public bool haveAbility;
    public float cooldownTime;
    public float durationTime;
    public MonsterAbilities ability;
    public MonsterAbilityStats stats;
    
    public Monster(MonsterSO so)
    { 
        this.skin = so.skin;
        this.health = so.health;
        this.damage = so.damage;
        this.cooldownAttack = so.cooldownAttack;
        this.moveSpeed = so.moveSpeed;
        this.experience = so.experience;
        this.haveAbility = so.haveAbility;
        this.ability = so.ability;
        this.cooldownTime = so.cooldownTime;
        this.durationTime = so.durationTime;
        this.stats = new MonsterAbilityStats(so.detectionRange, so.abilitySpeedBuff, so.abilityDamage, so.abilityRadius, so.vfxExecute);
    }
}
