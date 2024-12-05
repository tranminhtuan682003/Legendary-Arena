using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroDataBase", menuName = "Scriptable Objects/HeroDataBase")]
public class HeroDataBase : ScriptableObject
{
    [System.Serializable]
    public class HeroData
    {
        public string name;
        public GameObject hero;
    }
    public List<HeroData> heros;
}
