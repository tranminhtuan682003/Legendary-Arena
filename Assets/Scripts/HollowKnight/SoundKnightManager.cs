using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundKnightManager : MonoBehaviour
{
    public static SoundKnightManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {

    }

    private void Update()
    {

    }
}
