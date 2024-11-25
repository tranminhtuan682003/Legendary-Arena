using UnityEngine;
using System.Collections;

public class HomeBlueController : MonoBehaviour
{
    private Coroutine healCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            var knight = other.GetComponent<ITeamMember>();
            if (knight != null)
            {
                healCoroutine = StartCoroutine(HealKnight(knight));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            if (healCoroutine != null)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
            }
        }
    }

    private IEnumerator HealKnight(ITeamMember knight)
    {
        while (true)
        {
            knight.TakeDamage(-60);
            yield return new WaitForSeconds(1f);
        }
    }
}
