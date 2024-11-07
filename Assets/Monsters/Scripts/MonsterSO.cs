using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monster")]
public class MonsterSO : ScriptableObject
{
    public Color skin;

    public int health;

    public int damage;

    public int cooldownAttack;

    public float moveSpeed;

    public int experience;

    public bool haveAbility;

    public MonsterAbilities ability;

    public float cooldownTime;

    public float durationTime;

    public float detectionRange;

    public float abilitySpeedBuff;

    public int abilityDamage;

    public float abilityRadius;

    public GameObject vfxExecute;
}