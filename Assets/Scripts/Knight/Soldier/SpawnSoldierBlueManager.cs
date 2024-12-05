using UnityEngine;
using Zenject;
using System.Collections;

public class SpawnSoldierBlueManager : MonoBehaviour
{
    [Inject] UIKnightManager uIKnightManager;

    private void OnEnable()
    {
        StartCoroutine(SpawnSoldiers());
    }

    private IEnumerator SpawnSoldiers()
    {
        while (true)
        {
            for (int i = 0; i < 1; i++)
            {
                uIKnightManager.GetSoldierBlue(transform);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(20);
        }
    }
}
