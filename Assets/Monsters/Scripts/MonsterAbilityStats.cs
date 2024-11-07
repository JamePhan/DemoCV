using UnityEngine;

public class MonsterAbilityStats 
{
    public float detectionRange { get; private set; }
    public float abilitySpeedBuff {  get; private set; }
    public int abilityDamage { get; private set; }
    public float abilityRadius { get; private set; }
    public GameObject vfxExecute { get; private set; }

    public MonsterAbilityStats(float detectRange, float speedBuff, int damage, float radius, GameObject vfxExecute)
    {
        this.detectionRange = detectRange;
        this.abilitySpeedBuff = speedBuff;
        this.abilityDamage = damage;
        this.abilityRadius = radius;
        this.vfxExecute = vfxExecute;
    }
}
