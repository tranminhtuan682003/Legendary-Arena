using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenCandyDatabase", menuName = "ScreenCandy/ScreenCandyDatabase")]
public class CandyCrushData : ScriptableObject
{
    [System.Serializable]
    public class ScreenCandyData
    {
        public string screenName;
        public GameObject screenPrefab;
    }

    public List<ScreenCandyData> datas;
}

