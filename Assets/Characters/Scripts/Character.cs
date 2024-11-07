using UnityEngine;

public class Character
{
    public Sprite Avatar { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackRange { get; set; }
    public int NumberTarget { get; set; }
    public int PercentExpBonusEarn { get; set; }
    public CharacterAbilities Ability1 { get; set; }
    public CharacterAbilities Ability2 { get; set; }

    public Character() { }

    public Character(Sprite avatar, string name, int health, int damage, float atkSpeed, float moveSpeed, float atkRange, int number, int bonusExp, 
        CharacterAbilities a1, CharacterAbilities a2)
    {
        this.Avatar = avatar;
        this.Name = name;
        this.Health = health;
        this.Damage = damage;
        this.AttackSpeed = atkSpeed;
        this.MoveSpeed = moveSpeed;
        this.AttackRange = atkRange;
        this.NumberTarget = number;
        this.PercentExpBonusEarn = bonusExp;
        this.Ability1 = a1;
        this.Ability2 = a2;
    }

    //public void IncreaseHealth(int amount)
    //{
    //    this.Health += amount;
    //}

    //public void IncreaseDamage(int amount)
    //{
    //    this.Damage += amount;
    //}

}
