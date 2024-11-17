using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKnightManager : MonoBehaviour
{
    public static GameKnightManager instance;
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
