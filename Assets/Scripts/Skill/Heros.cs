using System.Collections.Generic;

public abstract class Heros
{
    public string heroName;
    public List<Ability> abilities = new List<Ability>();

    public abstract void SetupAbilities();
}
