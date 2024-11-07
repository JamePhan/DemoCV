using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour, IDamageable, IMovement, IAttack
{
    public int Health { get; set; }
    public int Damage { get; set; }
    public int CooldownAttack { get; set; }

    public float currentHealth;
    public float currentCooldownAtk;
    public float moveSpeed;
    public float moveSpeedCurrent;
    public int experience;
    public bool isCooldownAttack;
    public bool isBeingBuff;

    public bool haveAbility;
    public MonsterAbilities ability;
    //public MonsterAbilityStats stats;
    public AbilityMonster abilityMonster;
    public AbilityFactory abilityFactory;
    public GameObject monsInit;
    public Vector3 playerPos;
    public LineRenderer lineRenderer;

    public void Init(Monster monster, GameObject monsInit, LineRenderer lineRenderer)
    {
        this.monsInit = monsInit;
        this.Health = monster.health;
        this.currentHealth = this.Health;
        this.Damage = monster.damage;
        this.CooldownAttack = monster.cooldownAttack;
        this.currentCooldownAtk = this.CooldownAttack;
        this.moveSpeed = monster.moveSpeed;
        this.moveSpeedCurrent = this.moveSpeed;
        this.experience = monster.experience;
        this.isCooldownAttack = false;
        if(!monster.haveAbility) return;
        
        abilityFactory = new AbilityFactory(monsInit, monster.stats, lineRenderer, InitVfxExecute(monster));
        abilityMonster = new AbilityMonster(abilityFactory, monster);
        abilityMonster.Ready();
    }

    public GameObject InitVfxExecute(Monster monster)
    {
        if(monster.stats.vfxExecute == null) return null;
        GameObject vfxExecute = Instantiate(monster.stats.vfxExecute, monsInit.transform);
        vfxExecute.transform.localPosition = new Vector3(0, 0, 0);
        vfxExecute.SetActive(false);
        return vfxExecute;
    }

    public void Die()
    {
        if(abilityFactory != null) abilityFactory.isMonsterDie = true;
        LevelManager.killMonsDelegate += () => gameObject;
        LevelManager.earnExpDelegete += () => experience;
    }

    public void Move(Vector3 playerPosition)
    {
        MoveToPlayer(playerPosition);
        Push();
        if (!isCooldownAttack) Attack();
        else IsCooldownAttack();
        ResetMoveSpeedCurrent();
    }

    public void MoveToPlayer(Vector3 playerPosition)
    {
        Vector3 direction = -(transform.position - playerPosition).normalized;
        direction.y = 0;
        transform.Translate(direction * moveSpeedCurrent);
    }

    public void GetDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    public void IncreaseMoveSpeed(float amount)
    {
        //moveSpeedCurrent = 0.08f;
        moveSpeedCurrent += amount;
    }

    public void ResetMoveSpeedCurrent()
    {
        moveSpeedCurrent = moveSpeed;
    }

    private void OnDisable()
    {
        LevelManager.playerPosDelegate -= Move;
    }

    public void Attack()
    {
        Collider[] playerCollider = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), Quaternion.identity, LayerMask.GetMask("Character"));
        if (playerCollider.Length == 0) return;
        if (playerCollider[0].gameObject.TryGetComponent(out CharacterManager player))
        {
            player.GetDamage(Damage);
        }
        isCooldownAttack = true;
    }

    public void Push()
    {
        Collider[] playerCollider = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f), Quaternion.identity, LayerMask.GetMask("Monster"));
        if (playerCollider.Length <= 1) return;

        float distance = Vector3.Distance(transform.position, playerCollider[0].transform.position);
        if (distance < 1.1f)
        {
            Vector3 direction = (transform.position - playerCollider[0].transform.position).normalized;
            direction.y = 0;
            transform.Translate(direction * moveSpeedCurrent);

        }
    }

    public void IsCooldownAttack()
    {
        currentCooldownAtk -= Time.deltaTime;
        if (currentCooldownAtk <= 0)
        {
            currentCooldownAtk = CooldownAttack;
            isCooldownAttack = false;
        }
    }
}
