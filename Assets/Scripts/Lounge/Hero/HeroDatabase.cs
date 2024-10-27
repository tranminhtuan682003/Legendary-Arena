using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HeroDatabase", menuName = "ScriptableObjects/HeroDatabase")]
public class HeroDatabase : ScriptableObject
{
    [System.Serializable]
    public class HeroData
    {
        public string heroName;
        public GameObject heroPrefab;
    }
    public List<HeroData> heros;
}
