using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "PipeData", menuName = "Pipedatabase/PipeData", order = 1)]
public class FlappyBirdDatabase : ScriptableObject
{
    [System.Serializable]
    public class FlappyData
    {
        public string name;
        public GameObject prefab;
    }
    public List<FlappyData> data;
}
