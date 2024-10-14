using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SupplementaryTable", menuName = "ScriptableObjects/SupplementaryTable")]

public class SupplementaryDatabase : ScriptableObject
{
    [System.Serializable]
    public class SupplymentaryData
    {
        public string nameSup;
        public GameObject supPrefab;
    }
    public List<SupplymentaryData> supplymentarys;
}
