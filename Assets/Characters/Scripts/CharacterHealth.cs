using UnityEngine;

public class CharacterHealth : IDamageable
{
    public delegate int ShieldDelegate();
    public static event ShieldDelegate increaseShieldDelegate;

    public int Health { get; set; }
    public int currentHealth;
    public int shieldAmount;
    public bool haveShield;
    public bool isDie;
    public int passiveShield;
    
    public HpBarUI hpBar;

    public CharacterHealth(int health, HpBarUI hpBar)
    {
        this.Health = health;
        this.currentHealth = Health;
        this.shieldAmount = 0;
        this.passiveShield = 100;
        this.isDie = false;
        this.hpBar = hpBar;
        hpBar.InitHp(health);
    }

    public void AutoIncreaseShield()
    {
        if (increaseShieldDelegate == null) return;
        IncreaseShield();
    }

    public void IncreaseShield()
    {
        this.haveShield = true;
        var invocationList = increaseShieldDelegate.GetInvocationList();
        foreach (var del in invocationList)
        {
            int shieldAmountAbility = ((ShieldDelegate)del).Invoke();
            //player shield stats + ability shield stats
            shieldAmount = passiveShield + shieldAmountAbility;
            this.hpBar.InitShield(shieldAmount);

            increaseShieldDelegate -= (ShieldDelegate)del;
        }
    }

    public void DecreaseShieldAmount(int amount)
    {
        shieldAmount -= amount;
    }

    public void GetDamage(int amount)
    {
        int enemyDamage = amount;
        if (haveShield)
        {
            DecreaseShieldAmount(enemyDamage);
            hpBar.UpdateShieldBar(shieldAmount);
            if (shieldAmount > 0) return;
            hpBar.DestroyShield();
            haveShield = false;
            enemyDamage = Mathf.Abs(shieldAmount);
        }
        DecreaseHealth(enemyDamage);
        hpBar.UpdateHpBar(currentHealth);
    }

    public void IncreaseHp(int amount)
    {
        IncreaseHealth(amount);
        hpBar.UpdateHpMax(Health);
        hpBar.UpdateHpBar(currentHealth);
        if (haveShield) hpBar.UpdateShieldBar(shieldAmount);
    }

    public void IncreaseHealth(int amount)
    {
        this.Health += amount;
        this.currentHealth += amount;
    }
    
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        isDie = true;
    }
}
