using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StartPosition", menuName = "ScriptableObjects/StartPosition")]
public class StartPosition : ScriptableObject
{
    public List<Vector3> startPosition;
}
