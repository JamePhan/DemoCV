public interface IAttack 
{
    int Damage { get; set; }
    int CooldownAttack { get; set; }

    void Attack();
}
