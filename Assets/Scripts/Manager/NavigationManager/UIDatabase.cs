using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIDatabase", menuName = "ScriptableObjects/UIDatabase")]
public class UIDatabase : ScriptableObject
{
    [System.Serializable]
    public class UIScreenData
    {
        public string screenName;
        public GameObject screenPrefab;
    }

    public List<UIScreenData> screens;
}
