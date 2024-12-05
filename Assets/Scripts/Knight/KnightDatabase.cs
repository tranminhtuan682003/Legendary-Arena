using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "KnightData", menuName = "KnightDatabase/KnightData", order = 1)]
public class KnightDatabase : ScriptableObject
{
    [System.Serializable]
    public class KnightData
    {
        public string name;
        public GameObject prefab;
    }
    public List<KnightData> data;
}
