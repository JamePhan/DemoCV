using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityStats", menuName = "Ability Stats")]
public class AbilityStatsSO : ScriptableObject
{
    public AbilityType AbilityType;

    public int abilityShieldAmount;

    public int abilityDamage;

    public int abilityRadiusDamage;

    public float abilitySpeedAmount;

    public float abilityRadiusCC;
}
