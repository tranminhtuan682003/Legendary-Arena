public enum TypeSkill
{
    Attack,
    Skill1,
    Skill2,
    Skill3,
    Heal,
    Recall,
    Supplymentary,
    None

}

public static class TypeSkillExtensions
{
    public static bool CanInterrupt(this TypeSkill skill)
    {
        return skill == TypeSkill.Recall || skill == TypeSkill.Heal || skill == TypeSkill.Supplymentary;
    }
}

public enum TypeMove
{
    Up,
    Down,
    Right,
    Left,
    None
}

public enum EnemyBlue
{
    enemyBlue,
    None
}
