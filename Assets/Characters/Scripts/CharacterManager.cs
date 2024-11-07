using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public delegate void ImmuneDamageDelegate();
    public static event ImmuneDamageDelegate immuneDamageDelegate;

    public delegate SkillStats IncreaseStatsDelegate();
    public static event IncreaseStatsDelegate increaseStatsDelegate;

    public KeyboardInput input;
    public CharacterHealth health;

    public Movement movement;
    public MeshRenderer avatar;
    public HpBarUI hpBar;
    public GunManager gunManager;

    public bool isImmuneDamage;

    public Character charac;
    public GameObject panel_lose;

    public void Init(Character character)
    {
        this.charac = character;
        input = new KeyboardInput();
        movement.Init(character.MoveSpeed);
        health = new CharacterHealth(character.Health, hpBar);
        avatar.material.mainTexture = character.Avatar.texture;
        gunManager.Init(character);
    }

    void FixedUpdate()
    {
        Push();
        health.AutoIncreaseShield();
        if (increaseStatsDelegate != null) IncreseStats();

        if (immuneDamageDelegate != null) 
        {
            IsImmuneDamage(true); 
        }
        else IsImmuneDamage(false);
        
    }

    public void IncreseStats()
    {
        var invocationList = increaseStatsDelegate.GetInvocationList();
        foreach (var del in invocationList)
        {
            SkillStats increaseStat = ((IncreaseStatsDelegate)del).Invoke();
            switch (increaseStat.stats)
            {
                case Stats.Health:
                    health.IncreaseHp(increaseStat.amount);
                    break;

                case Stats.Damage:
                    gunManager.IncreseDamage(increaseStat.amount);
                    break;

                case Stats.AttackRange:
                    gunManager.IncreaseAtkRange(increaseStat.amountF);
                    break;

                case Stats.MoveSpeed:
                    movement.IncreaseMoveSpeed(increaseStat.amountF);
                    break;

                case Stats.AttackSpeed:
                    gunManager.IncreaseAtkSpeed(increaseStat.amountF);
                    break;
            }

            increaseStatsDelegate -= (IncreaseStatsDelegate)del;
        }
    }

    public void GetDamage(int amount)
    {
        if (isImmuneDamage) return;
        if (health.isDie)
        {
            Die();
            return; 
        }
        health.GetDamage(amount);
    }

    public void Die()
    {
        panel_lose.SetActive(true);
        Time.timeScale = 0;
    }

    public void IsImmuneDamage(bool status)
    {
        isImmuneDamage = status;
    }

    public void Push()
    {
        
        Collider[] playerCollider = Physics.OverlapBox(transform.position, new Vector3(1.1f, 1.1f, 1.1f), Quaternion.identity, LayerMask.GetMask("Border"));
        if (playerCollider.Length == 0) return;

        Die();
    }

}
