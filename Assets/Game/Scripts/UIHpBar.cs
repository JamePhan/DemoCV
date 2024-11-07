using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class UIHpBar : MonoBehaviour
{
    //public static UIHpBar instance;

    public SpriteRenderer hpBar;
    public GameObject shield_Bar;
    public SpriteRenderer shieldBar;

    public TextMeshPro textHp;
    private float oneHpPerWidth;
    private float oneShieldPerWidth;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else Destroy(gameObject);
    //}

    public void InitHp(int hpMax)
    {
        UpdateText(hpMax);
        this.oneHpPerWidth = 5f / hpMax;
        CalculateHpSizeSprite(hpMax);
    }

    public void UpdateHpMax(int hpMax)
    {
        this.oneHpPerWidth = 5f / hpMax;
    }

    public void UpdateHpBar(int amount)
    {
        CalculateHpSizeSprite(amount);
        UpdateText(amount);
    }

    public void CalculateHpSizeSprite(int hp)
    {
        Vector2 newSize = new Vector2(oneHpPerWidth * hp, 1);
        hpBar.size = newSize;
    }
    
    public void InitShield(int shieldMax)
    {
        shield_Bar.SetActive(true);
        UpdateText(shieldMax);
        this.oneShieldPerWidth = 5f / shieldMax;
        CalculateShieldSizeSprite(shieldMax);
    }

    public void UpdateShieldBar(int amount)
    {
        CalculateShieldSizeSprite(amount);
        UpdateText(amount);
    }

    public void CalculateShieldSizeSprite(int amount)
    {
        Vector2 newSize = new Vector2(oneShieldPerWidth * amount, 1);
        shieldBar.size = newSize;
    }

    public void UpdateText(int amount)
    {
        textHp.text = amount + "";
    }

    public void DestroyShield()
    {
        shield_Bar.SetActive(false);
    }
}
