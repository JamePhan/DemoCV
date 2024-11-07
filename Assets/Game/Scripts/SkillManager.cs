using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Stats
{
    Health, 
    Damage, AttackRange,
    MoveSpeed,
    AttackSpeed,
}

public class SkillStats
{
    public Stats stats {  get; private set; }
    public int amount { get; private set; }
    public float amountF {  get; private set; }

    public SkillStats(Stats stats, int amount)
    {
        this.stats = stats;
        this.amount = amount;
    }

    public SkillStats(Stats stats, float amountF)
    {
        this.stats = stats;
        this.amountF = amountF;
    }
}

public class SkillManager : MonoBehaviour
{
    public Character character;

    public GameObject panel;
    public Button skill_1;
    public Button skill_2;
    public Button skill_3;
    public TextMeshProUGUI textTitle_1;
    public TextMeshProUGUI textTitle_2;
    public TextMeshProUGUI textTitle_3;
    public TextMeshProUGUI textSkill_1;
    public TextMeshProUGUI textSkill_2;
    public TextMeshProUGUI textSkill_3;

    private int atkDamageIncrease = 20;
    private int hpIncrease = 200;
    private float atkSpeedIncrease = 0.15f;
    private float moveSpeedIncrease = 0.2f;
    private float radiusAttackIncrease = 0.5f;
    //private int bonusIncrease = 25;

    public void Init(Character character)
    {
        this.character = character;
        LevelManager.levelUpDelegete += () => LevelUp();
    }

    public void LevelUp()
    {
        GameStop();
        InitChooseSkill_Panel();
    }

    public void GameStop()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void GameResume()
    {
        Time.timeScale = 1;
        skill_1.onClick.RemoveAllListeners();
        skill_2.onClick.RemoveAllListeners();
        skill_3.onClick.RemoveAllListeners();
        panel.SetActive(false);
    }

    public void InitChooseSkill_Panel()
    {
        GetRandomSkill(skill_1, textTitle_1, textSkill_1);
        GetRandomSkill(skill_2, textTitle_2, textSkill_2);
        GetRandomSkill(skill_3, textTitle_3, textSkill_3);
    }

    public void GetRandomSkill(Button btn, TextMeshProUGUI title, TextMeshProUGUI skill)
    {
        int randomValue = Random.Range(1, 6);
        switch (randomValue)
        {
            case 1:
                btn.onClick.AddListener(IncreaseHealth);
                title.text = "Increase a huge health";
                skill.text = "Health + 200";
                break;

            case 2:
                btn.onClick.AddListener(IncreaseAttackDamage);
                title.text = "Increase a huge damage";
                skill.text = "Damage + 20";
                break;

            case 3:
                btn.onClick.AddListener(IncreaseAttackSpeed);
                title.text = "Increase attack speed";
                skill.text = "Attack Speed + 10%";
                break;

            case 4:
                title.text = "Increase move speed";
                skill.text = "Move Speed + 2";
                btn.onClick.AddListener(IncreaseMoveSpeed);
                break;

            case 5:
                title.text = "Increase attack range";
                skill.text = "Attack range + 5";
                btn.onClick.AddListener(IncreaseRadiusAttack);
                break;

                //case 6:
                //    title.text = "Increase exp earn";
                //    skill.text = "Exp earn + 25%";
                //    btn.onClick.AddListener(IncreaseBonusExpEarn);
                //    break;
        }
    }

    public void IncreaseHealth()
    {
        CharacterManager.increaseStatsDelegate += () => new SkillStats(Stats.Health, hpIncrease);
        GameResume();
    }

    public void IncreaseAttackDamage()
    {
        CharacterManager.increaseStatsDelegate += () => new SkillStats(Stats.Damage, atkDamageIncrease);
        GameResume();
    }

    public void IncreaseAttackSpeed()
    {
        CharacterManager.increaseStatsDelegate += () => new SkillStats(Stats.AttackSpeed, atkSpeedIncrease);
        GameResume();
    }

    public void IncreaseMoveSpeed()
    {
        CharacterManager.increaseStatsDelegate += () => new SkillStats(Stats.MoveSpeed, moveSpeedIncrease);
        GameResume();
    }

    public void IncreaseRadiusAttack()
    {
        CharacterManager.increaseStatsDelegate += () => new SkillStats(Stats.AttackRange, radiusAttackIncrease);
        GameResume();
    }

}
