public class Experience
{
    public float experience;
    public int bonusPercent;

    public void Init(int bonus)
    {
        this.experience = 0;
        this.bonusPercent = bonus;
    }

    public void IncreaseExp(int expEarn)
    {
        experience += expEarn * bonusPercent / 100;
    }

    public void ResetExperience()
    {
        this.experience = 0;
    }
}
