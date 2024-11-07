using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AbilityFactory;

public class AbilityFactory
{
    private Dictionary<string, Func<Ability>> abilityDictionary;

    private Dictionary<string, Func<bool>> monsterAbilityCastConditions;
    private Dictionary<string, Action> monsterAbilityDictionary;
    private Dictionary<string, Action> monsterAbilityExecuteDict;

    public delegate void BuffMoveSpeedDelegate(float abilitySpeedBuff);
    public static event BuffMoveSpeedDelegate buffMoveSpeedDelegate;

    public GameObject monsterGO;
    public MonsterAbilityStats stats;
    public LineRenderer lineRenderer;
    public bool isMonsterDie;
    public GameObject vfxExecute;

    public Vector3 from;
    public Vector3 to;
    public float radius;

    public AbilityFactory(GameObject monsterGO, MonsterAbilityStats stats, LineRenderer lineRenderer, GameObject vfxExecute)
    {
        monsterAbilityCastConditions = new Dictionary<string, Func<bool>>
        {
            { "Exploded", () => MonsterDie() },
            { "HurryUp", () => InRangeAbility(monsterGO, stats) },
            { "BlueLine", () => InRangeAbility(monsterGO, stats) },
        };

        monsterAbilityDictionary = new Dictionary<string, Action>
        {
            { "Exploded", () => DrawCircleBallistic() },
            { "HurryUp", () => HurryUp(monsterGO, stats) },
            { "BlueLine", () => DrawLineBallistic() },
        };
        monsterAbilityExecuteDict = new Dictionary<string, Action>
        {
            { "Exploded", () => Exploded(monsterGO, stats) },
            { "HurryUp", () =>  DisableEffectVFX() },
            { "BlueLine", () => BlueLine() },
        };

        this.monsterGO = monsterGO;
        this.stats = stats;
        this.lineRenderer = lineRenderer;
        lineRenderer.enabled = false;
        this.vfxExecute = vfxExecute;
        isMonsterDie = false;
        radius = stats.abilityRadius;
    }

    public AbilityFactory(Image imgSkill, Button button, GameObject reload, Image image, TextMeshProUGUI text)
    {
        abilityDictionary = new Dictionary<string, Func<Ability>>
        {
            { "ThrowBomb", () => new ThrowBomb(imgSkill, button, reload, image, text) },
            { "GetShield", () => new GetShield(imgSkill, button, reload, image, text) },
            { "Unstoppable", () => new Unstoppable(imgSkill, button, reload, image, text) },
            // .......
        };
    }

    public Ability CreateAbility(string abilityName)
    {
        if (abilityDictionary.ContainsKey(abilityName))
        {
            return abilityDictionary[abilityName]();
        }
        throw new Exception("Ability not found");
    }

    public void CastMonsterAbility(string abilityName)
    {
        if (monsterAbilityDictionary.ContainsKey(abilityName))
        {
            monsterAbilityDictionary[abilityName]();
        }
    }

    public bool MonsterAbilityCastConditions(string abilityName)
    {
        if (monsterAbilityCastConditions.ContainsKey(abilityName))
        {
            return monsterAbilityCastConditions[abilityName]();
        }
        throw new Exception("Condition not found");
    }
    
    public void ExecuteMonsterAbility(string abilityName)
    {
        if (monsterAbilityExecuteDict.ContainsKey(abilityName))
        {
            monsterAbilityExecuteDict[abilityName]();
        }
    }

    public bool MonsterDie()
    {
        if (isMonsterDie)
        {
            return true;
        }
        return false;
    }

    public void DrawCircleBallistic()
    {
        //Debug.Log("DRAW CIRCLE");
        //lineRenderer.enabled = true;
        //lineRenderer.startColor = Color.red;
        //lineRenderer.endColor = Color.red;
        //int subdivision = 50;
        //float angleStep = 2f * Mathf.PI / subdivision;
        //lineRenderer.positionCount = subdivision;
        //for (int i = 0; i < subdivision; i++)
        //{
        //    float xPosition = radius * Mathf.Cos(angleStep * i);
        //    float zPosition = radius * Mathf.Sin(angleStep * i);

        //    Vector3 pointInCircle = new Vector3(xPosition, 0f, zPosition);
        //    lineRenderer.SetPosition(i, pointInCircle);
        //}
    }

    public void DrawLineBallistic()
    {
        from = monsterGO.transform.position;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = true;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }


    public bool InRangeAbility(GameObject monsterGO, MonsterAbilityStats stats)
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(monsterGO.transform.position, stats.detectionRange, LayerMask.GetMask("Character"));
        if (hitColliders.Length == 0) return false;
        foreach (Collider enemy in hitColliders)
        {
            LevelManager.playerPosDelegate += GetPlayerPosition;
        }

        return true;
    }

    public void GetPlayerPosition(Vector3 playerPosition)
    {
        to = playerPosition;
    }

    public void Exploded(GameObject monsterGO, MonsterAbilityStats stats)
    {
        isMonsterDie = false;
        lineRenderer.enabled = false;
        Collider[] hitColliders = Physics.OverlapSphere(monsterGO.transform.position, stats.abilityRadius, LayerMask.GetMask("Character"));
        if (hitColliders.Length == 0) return;
        foreach (Collider enemy in hitColliders)
        {
            if (enemy.gameObject.TryGetComponent(out CharacterManager damage))
            {
                damage.GetDamage(stats.abilityDamage);
            }
        }
    }

    public void HurryUp(GameObject monsterGO, MonsterAbilityStats stats)
    {
        if(!vfxExecute.activeSelf) vfxExecute.SetActive(true);

        Collider[] hitColliders = Physics.OverlapSphere(monsterGO.transform.position, stats.abilityRadius, LayerMask.GetMask("Monster"));
        if (hitColliders.Length == 0) return;
        foreach (Collider target in hitColliders)
        {
            if (target.gameObject.TryGetComponent(out MonsterBehaviour behaviour))
            {
                buffMoveSpeedDelegate += behaviour.IncreaseMoveSpeed;
            }
        }
        buffMoveSpeedDelegate?.Invoke(stats.abilitySpeedBuff);
        buffMoveSpeedDelegate = null;
    }

    public void BlueLine()
    {
        lineRenderer.enabled = false;
        if(Physics.Raycast(monsterGO.transform.position, (to - monsterGO.transform.position).normalized, out RaycastHit hit, Vector3.Distance(monsterGO.transform.position, to)))
        {
            if (hit.transform.TryGetComponent(out CharacterManager damage))
            {
                damage.GetDamage(stats.abilityDamage);
            }
        }
    }

    public void DisableEffectVFX()
    {
        vfxExecute.SetActive(false);
    }

    public void DelayExecute()
    {
        LevelManager.playerPosDelegate -= GetPlayerPosition;
    }

}
