using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    private float speed = 1f;
    void Start()
    {
        StartCoroutine(WaitForReturnPool());
    }

    void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private IEnumerator WaitForReturnPool()
    {
        yield return new WaitForSeconds(7f);
        gameObject.SetActive(false);
    }

}
