using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SupplyToTheTurret", menuName = "ScriptableObjects/SupplyToTheTurret")]
public class SupplyToTheTurret : ScriptableObject
{
    [System.Serializable]
    public class SupplyDatabase
    {
        public string name;
        public GameObject prefab;
    }
    public List<SupplyDatabase> items;
}
