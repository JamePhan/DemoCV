using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    public Sprite Avatar;

    public int Health;

    public int Damage;

    public float AttackSpeed;

    public float MoveSpeed;

    public float AttackRange;

    public int NumberTarget;

    public int PercentExpBonusEarn;

    public CharacterAbilities Ability1;

    public CharacterAbilities Ability2;
}
