using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public delegate void PlayerPositionDelegate(Vector3 playerPosition);
    public static event PlayerPositionDelegate playerPosDelegate;

    public delegate GameObject KillMonsterDelegate();
    public static event KillMonsterDelegate killMonsDelegate;

    public delegate GameObject DestroyGameObjectDelegate();
    public static event DestroyGameObjectDelegate destroyObjectDelegate;

    public delegate int EarnExpDelegate();
    public static event EarnExpDelegate earnExpDelegete;
    public delegate void LevelUpDelegate();
    public static event LevelUpDelegate levelUpDelegete;

    public Level level;
    public int currentLevel;
    public Experience exp;
    public float currentExp;
    public Slider expSlider;
    public List<Level> levelsList = new List<Level>();
    

    public Character character;

    public Dictionary<string, Monster> monsterDict;

    public GameObject characterPlayer;
    public Spawner spawner;
    public GameObject CubePrefab;
    public GameObject SpherePrefab;

    public void Init(Character character)
    {
        this.character = character;
        LoadListMonster();
        InitSpawner();
        InitLevelsList();
        NewGame(this.character.PercentExpBonusEarn);
        InvokeRepeating("SpawnRandomMonster", 30f, 15f);
        InvokeRepeating("SpawnRandomMonster", 45f, 15f);
    }

    public void LoadListMonster()
    {
        MonsterSO[] arrMonsters = Resources.LoadAll<MonsterSO>("Monsters");
        monsterDict = new Dictionary<string, Monster>();
        foreach (MonsterSO so in arrMonsters)
        {
            monsterDict.Add(so.name, new Monster(so));
        }
    }

    public void InitLevelsList()
    {
        int expPerLevel = 50;
        for (int i = 1; i < 10; i++) levelsList.Add(new Level(i, expPerLevel += expPerLevel));
    }

    public void NewGame(int bonus)
    {
        currentLevel = 0;
        level = levelsList[currentLevel];
        exp = new Experience();
        exp.Init(bonus);
        SetUp();
        InitSliderExp();
        RunCurrentLevel();
    }

    public void SetUp()
    {
        this.currentLevel = level.idLevel;
        this.currentExp = exp.experience;
    }

    private void FixedUpdate()
    {
        if (earnExpDelegete != null) EarnExp();
        playerPosDelegate?.Invoke(characterPlayer.transform.position);
        if (killMonsDelegate != null) KillMonsters();
        if (destroyObjectDelegate != null) DestroyGameObject();
    }

    public void EarnExp()
    {
        var invocationList = earnExpDelegete.GetInvocationList();
        foreach (var del in invocationList)
        {
            int expEarn = ((EarnExpDelegate)del).Invoke();
            exp.IncreaseExp(expEarn);
            UpdateSliderExp();
            if (exp.experience >= level.expNeed) LevelUp();
            SetUp();
            earnExpDelegete -= (EarnExpDelegate)del;
        }
    }

    public void UpdateSliderExp()
    {
        expSlider.value = exp.experience;
    }

    public void LevelUp()
    {
        exp.ResetExperience();
        levelUpDelegete?.Invoke();
        GetNextLevel();
        InitSliderExp();
        RunCurrentLevel();
    }

    public void InitSliderExp()
    {
        expSlider.maxValue = level.expNeed;
        expSlider.value = 0;
    }

    public void GetNextLevel()
    {
        currentLevel++;
        level = levelsList[currentLevel - 1];
    }

    public void RunCurrentLevel()
    {
        switch (currentLevel)
        {
            case 1:
                StartCoroutine(RunLevel_1());
                break;

            case 2:
                StartCoroutine(RunLevel_2());
                break;

            case 3:
                StartCoroutine(RunLevel_3());
                break;

            case 4:
                StartCoroutine(RunLevel_4());
                break;

            case 5:
                StartCoroutine(RunLevel_5());
                break;
        }
    }

    IEnumerator RunLevel_1()
    {
        InitSpawner();
        spawner.Init(CubePrefab, true, 100);
        SpawnMonsters(monsterDict["Yellow"], 5);
        yield return new WaitForSeconds(10f);
        SpawnMonsters(monsterDict["Yellow"], 5);
        yield return new WaitForSeconds(20f);
        SpawnMonsters(monsterDict["Yellow"], 10);
    }

    IEnumerator RunLevel_2()
    {
        SpawnMonsters(monsterDict["Orange"], 3);
        yield return new WaitForSeconds(10f);
        SpawnMonsters(monsterDict["Black"], 1);
        yield return new WaitForSeconds(10f);
        SpawnMonsters(monsterDict["Orange"], 3);
        yield return new WaitForSeconds(10f);
        SpawnMonsters(monsterDict["Green"], 1);
        SpawnMonsters(monsterDict["Red"], 5);
        yield return new WaitForSeconds(10f);
        SpawnMonsters(monsterDict["Green"], 3);
        SpawnMonsters(monsterDict["Blue"], 5);

    }

    IEnumerator RunLevel_3()
    {
        SpawnMonsters(monsterDict["Blue"], 1);
        SpawnMonsters(monsterDict["Green"], 1);
        SpawnMonsters(monsterDict["Red"], 3);
        yield return new WaitForSeconds(30f);
        SpawnMonsters(monsterDict["Black"], 10);
        SpawnMonsters(monsterDict["Red"], 10);
        yield return new WaitForSeconds(10f);
        SpawnMonsters(monsterDict["Red"], 10);


    }

    IEnumerator RunLevel_4()
    {
        SpawnMonsters(monsterDict["Purple"], 5);
        yield return new WaitForSeconds(50f);
        SpawnMonsters(monsterDict["Blue"], 1);
        SpawnMonsters(monsterDict["Purple"], 20);
        yield return new WaitForSeconds(100f);
        SpawnMonsters(monsterDict["Purple"], 20);
        yield return new WaitForSeconds(100f);
        SpawnMonsters(monsterDict["Purple"], 20);
    }

    IEnumerator RunLevel_5()
    {
        SpawnMonsters(monsterDict["Green"], 2);
        yield return new WaitForSeconds(50f);
        SpawnMonsters(monsterDict["Black"], 20);
        yield return new WaitForSeconds(100f);
        SpawnMonsters(monsterDict["Black"], 20);
        yield return new WaitForSeconds(100f);
        SpawnMonsters(monsterDict["Blue"], 10);
    }

    public void SpawnRandomMonster()
    {
        int randomValue = Random.Range(1, 7);
        switch (randomValue)
        {
            case 1:
                SpawnMonsters(monsterDict["Yellow"], 15);
                break;

            case 2:
                SpawnMonsters(monsterDict["Orange"], 15);
                break;

            case 3:
                SpawnMonsters(monsterDict["Red"], 15);
                break;

            case 4:
                SpawnMonsters(monsterDict["Green"], 3);
                break;

            case 5:
                SpawnMonsters(monsterDict["Blue"], 5);
                break;

            case 6:
                SpawnMonsters(monsterDict["Black"], 7);
                break;

            case 7:
                SpawnMonsters(monsterDict["Purple"], 15);
                break;
        }
    }

    public void InitSpawner()
    {
        spawner = transform.AddComponent<Spawner>();
    }

    public void SpawnMonsters(Monster monster, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnMonster(monster);
        }
    }

    public void SpawnMonster(Monster monster)
    {
        Vector3 monsterPosition = GetSpawnPosition(characterPlayer.transform.position, 50f);
        GameObject monsInit = spawner.Spawn();
        monsInit.transform.position = monsterPosition;

        MonsterBehaviour monsterBeha = monsInit.AddComponent<MonsterBehaviour>();

        monsterBeha.Init(monster, monsInit, InitBallisticAbility(monsInit));
        monsInit.GetComponent<MeshRenderer>().material.SetColor("_Color", monster.skin);

        playerPosDelegate += monsterBeha.Move;
    }

    public LineRenderer InitBallisticAbility(GameObject monsInit)
    {
        LineRenderer lineRenderer = monsInit.transform.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = monsInit.transform.AddComponent<LineRenderer>();
        }
        //LineRenderer lineRenderer = monsInit.transform.GetComponent<LineRenderer>() ?? monsInit.transform.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;
        return lineRenderer;
    }

    public Vector3 GetSpawnPosition(Vector3 target, float radius)
    {
        Vector2 randomPoint2D = Random.insideUnitCircle * radius;
        Vector3 randomPoint3D = new Vector3(randomPoint2D.x, 0, randomPoint2D.y);
        return target + randomPoint3D;
    }

    public void KillMonsters()
    {
        var invocationList = killMonsDelegate.GetInvocationList();
        foreach (var del in invocationList)
        {
            GameObject monster = ((KillMonsterDelegate)del).Invoke();
            spawner.Kill(monster);
            killMonsDelegate -= (KillMonsterDelegate)del;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(destroyObjectDelegate?.Invoke());
    }
}
