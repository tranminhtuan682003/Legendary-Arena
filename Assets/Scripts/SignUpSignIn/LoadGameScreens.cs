using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadGameScreens", menuName = "Scriptable Objects/LoadGameScreens")]
public class LoadGameScreens : ScriptableObject
{
    [System.Serializable]
    public class ScreenLoads
    {
        public string name;
        public GameObject screen;
    }
    public List<ScreenLoads> screens;
}
