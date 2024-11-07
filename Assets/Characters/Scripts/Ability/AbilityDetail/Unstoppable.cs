using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unstoppable : Ability
{
    public Vector3 position;
    public GameObject VFXPrefab;
    public GameObject vfx;
    public ParticleSystem effectVFX;

    public bool isUnstoppable;

    public Unstoppable(Image imgSkill, Button button, GameObject reload, Image image, TextMeshProUGUI text)
    {
        Init(imgSkill, button, reload, image, text);
        GetAbilityStats();
        this.VFXPrefab = AbilitySO.effectVFX;
        LevelManager.playerPosDelegate += GetPlayerPosition;
        InitVFX();
    }

    public void GetPlayerPosition(Vector3 playerPosition)
    {
        position = playerPosition - new Vector3(0f, 1f, 0f);
    }

    public override void Activate()
    {
        Action();
        vfx.SetActive(true);
    }

    public override void Action()
    {
        CharacterManager.immuneDamageDelegate += IsUnstoppable;
    }

    public override void EffectDuration()
    {
        vfx.transform.position = position;
        //effectVFX.Play();
    }

    public void InitVFX()
    {
        vfx = Object.Instantiate(this.VFXPrefab, position, Quaternion.identity);
        effectVFX = vfx.GetComponent<ParticleSystem>();
        vfx.SetActive(false);
    }

    public override void DisableEffectDuration()
    {
        CharacterManager.immuneDamageDelegate -= IsUnstoppable;
        isUnstoppable = false;
        vfx.SetActive(false);
    }

    public void IsUnstoppable()
    {
        isUnstoppable = true;
    }
}
