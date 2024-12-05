using System.Collections;
using UnityEngine;
using Zenject;

public class SpawnSoldierManager : MonoBehaviour
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
            for (int i = 0; i < 2; i++)
            {
                uIKnightManager.GetSoldierRed(transform);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(15);
        }
    }
}
