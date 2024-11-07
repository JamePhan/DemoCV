using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ThrowBomb : Ability
{
    public GameObject bombPrefab;
    public GameObject VFXPrefab;

    public Vector3 position;
    public GameObject bomb;
    public GameObject vfx;
    public ParticleSystem effectVFX;

    public int abilityDamage;
    public float abilityRadiusDamage;

    public ThrowBomb(Image imgSkill, Button button, GameObject reload, Image image, TextMeshProUGUI text)
    {
        Init(imgSkill, button, reload, image, text);
        GetAbilityStats();
        this.bombPrefab = AbilitySO.objectSpawm;
        this.VFXPrefab = AbilitySO.effectVFX;
        LevelManager.playerPosDelegate += PlantPosition;
        InitBomb();
        InitVFX();
    }

    public override void GetAbilityStats()
    {
        abilityDamage = AbilityStatsSO.abilityDamage;
        abilityRadiusDamage = AbilityStatsSO.abilityRadiusDamage;
    }

    public override void Activate()
    {
        Action();
    }

    public override void Action()
    {
        bomb.SetActive(true);
        bomb.transform.position = position;
    }

    public override void Execute()
    {
        Exploded();
    }
    
    public override void EffectExecute()
    {
        vfx.transform.position = bomb.transform.position;
        effectVFX.Play();
    }
    
    public void Exploded()
    {
        Collider[] hitColliders = Physics.OverlapSphere(bomb.transform.position, abilityRadiusDamage, LayerMask.GetMask("Monster"));
        if (hitColliders != null)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.TryGetComponent(out IDamageable damage))
                {
                    damage.GetDamage(abilityDamage);
                }
            }

            bomb.SetActive(false);
        }

    }
    
    public void InitBomb()
    {
        bomb = Object.Instantiate(this.bombPrefab, position, Quaternion.identity);
        bomb.SetActive(false);
    }

    public void InitVFX()
    {
        vfx = Object.Instantiate(this.VFXPrefab, bomb.transform.position, Quaternion.identity);
        effectVFX = vfx.GetComponent<ParticleSystem>();
    }

    public void PlantPosition(Vector3 playerPosition)
    {
        position = playerPosition - new Vector3(0f, 1f, 0f);
    }
}
