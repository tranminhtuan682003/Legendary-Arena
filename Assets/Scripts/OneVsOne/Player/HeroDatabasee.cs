using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HeroDatabasee", menuName = "ScriptableObjects/HeroDatabasee")]
public class HeroDatabasee : ScriptableObject
{
    [System.Serializable]
    public class HeroDataa
    {
        public string name;
        public GameObject gameObject;
    }
    public List<HeroDataa> data;
}
