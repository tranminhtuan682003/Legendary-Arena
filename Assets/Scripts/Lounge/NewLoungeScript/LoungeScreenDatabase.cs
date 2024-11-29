using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoungeScreenDatabase", menuName = "Scriptable Objects/LoungeScreenDatabase")]
public class LoungeScreenDatabase : ScriptableObject
{
    [System.Serializable]
    public class LoungeScreenData
    {
        public string name;
        public GameObject screen;
    }
    public List<LoungeScreenData> screens;
}
