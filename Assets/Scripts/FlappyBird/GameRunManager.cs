using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunManager : MonoBehaviour
{
    private float timeSpawn = 2.5f;

    void Start()
    {
        StartCoroutine(SpawnPipe());
    }

    private IEnumerator SpawnPipe()
    {
        while (true)
        {
            UIFlappyManager.Instance.GetPipe();
            yield return new WaitForSeconds(timeSpawn);
        }
    }
}
