using BigRookGames.Weapons;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public float atkSpeedCurrent;

    public GunfireController gfController;
    public Collider[] hitColliders;
    public bool isActive = true;

    public Character character;
    public int numberTarget;

    public void Init(Character character)
    {
        this.character = character;
        atkSpeedCurrent = character.AttackSpeed;
        gfController.ChangeAttackSpeed(character.AttackSpeed);
    }

    private void FixedUpdate()
    {
        if (atkSpeedCurrent >= character.AttackSpeed && isActive)
        {
            DetectAndShootEnemy();
        }
        else 
        { 
            atkSpeedCurrent += Time.deltaTime;
            SpamEffect(false);
        }
    }

    public void IncreseDamage(int amount)
    {
        character.Damage += amount;
    }

    public void IncreaseAtkSpeed(float amount)
    {
        character.AttackSpeed = character.AttackSpeed - amount;
        gfController.ChangeAttackSpeed(character.AttackSpeed);
    }

    public void IncreaseAtkRange(float amount)
    {
        character.AttackRange += amount;
    }

    private void DetectAndShootEnemy()
    {
        hitColliders = Physics.OverlapSphere(transform.position, character.AttackRange, LayerMask.GetMask("Monster"));
        if (hitColliders.Length == 0) return;

        GetNumberTargetShoot();
        for (int i = numberTarget; i < hitColliders.Length; i++)
        {
            RotateGun(hitColliders[i].transform);
            Fire(hitColliders[i].transform);
        }
    }

    public void GetNumberTargetShoot()
    {
        numberTarget = hitColliders.Length - character.NumberTarget;
    }

    public void RotateGun(Transform enemy)
    {
        transform.LookAt(enemy);
    }

    public void Fire(Transform enemy)
    {
        //if (atkSpeedCurrent >= character.AttackSpeed)
        //{
            SpamEffect(true);
            Damage(enemy);
            atkSpeedCurrent = 0;
        //}    
    }

    public void SpamEffect(bool status)
    {
        gfController.ChangeAutoFireStatus(status);
    }

    public void Damage(Transform enemy)
    {
        if (enemy.gameObject.TryGetComponent(out IDamageable damage))
        {
            damage.GetDamage(character.Damage);
        }
    }
}
