using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string characterName;
    public Character character;

    public GameObject characterPlayer;
    public SkillManager skillManager;
    public LevelManager levelManager;
    public CharacterManager characterManager;
    public AbilityManager abilityManager;

    public GameObject panel_Intro;
    public Button btn_ok;

    void Start()
    {
        characterName = PlayerPrefs.GetString("CharacterName");
        Init(characterName);
    }

    public void Init(string characterName)
    {
        character = LoadCharacter(characterName);

        skillManager.Init(character);
        levelManager.Init(character);
        characterManager.Init(character);
        abilityManager.Init(character);

        btn_ok.onClick.AddListener(OK);
        Time.timeScale = 0;
        panel_Intro.SetActive(true);
    }

    public void OK()
    {
        Time.timeScale = 1;
        panel_Intro.SetActive(false);
    }

    public Character LoadCharacter(string characterName)
    {
        CharacterSO so = Resources.Load<CharacterSO>("Characters/" + characterName);
        Character character = new Character();
        character.Avatar = so.Avatar;
        character.Name = characterName;
        character.Health = so.Health;
        character.Damage = so.Damage;
        character.AttackSpeed = so.AttackSpeed;
        character.MoveSpeed = so.MoveSpeed;
        character.AttackRange = so.AttackRange;
        character.NumberTarget = so.NumberTarget;
        character.PercentExpBonusEarn = so.PercentExpBonusEarn;
        character.Ability1 = so.Ability1;
        character.Ability2 = so.Ability2;
        return character;
    }

}
