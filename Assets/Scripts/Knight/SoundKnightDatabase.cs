using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundKnightData", menuName = "KnightDatabase/SoundKnightData", order = 1)]
public class SoundKnightDatabase : ScriptableObject
{
    [System.Serializable]
    public class SoundKnightData
    {
        public string name;
        public AudioClip audio;
    }
    public List<SoundKnightData> datas;

}
