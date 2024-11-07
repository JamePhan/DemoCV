using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetShield : Ability
{
    public int abilityShieldAmount;

    public Vector3 position;
    public GameObject VFXPrefab;
    public GameObject vfx;
    public ParticleSystem effectVFX;

    public GetShield(Image imgSkill, Button button, GameObject reload, Image image, TextMeshProUGUI text)
    {
        Init(imgSkill, button, reload, image, text);
        GetAbilityStats();
        this.VFXPrefab = AbilitySO.effectVFX;
        LevelManager.playerPosDelegate += GetPlayerPosition;
        InitVFX();
    }

    public override void GetAbilityStats()
    {
        abilityShieldAmount = AbilityStatsSO.abilityShieldAmount;
    }

    public override void Activate()
    {
        Action();
        vfx.SetActive(true);
    }

    public override void Action()
    {
        CharacterHealth.increaseShieldDelegate += IncreaseShield;
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

    public int IncreaseShield()
    {
        return abilityShieldAmount;
    }

    public void GetPlayerPosition(Vector3 playerPosition)
    {
        position = playerPosition - new Vector3(0f, 1f, 0f);
    }

    public override void DisableEffectDuration() => vfx.SetActive(false);
}
