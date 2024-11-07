public interface IDamageable
{
    int Health { get; set; }

    void GetDamage(int amount);
    void Die();
}
