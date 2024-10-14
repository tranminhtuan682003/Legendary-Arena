using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroBackgroundDatabase", menuName = "ScriptableObjects/HeroBackgroundDatabase")]
public class HeroBackgroundDatabase : ScriptableObject
{
    [System.Serializable]
    public class HeroBackground
    {
        public string nameHero;
        public GameObject Image;
    }
    public List<HeroBackground> heroBackgrounds;
}
