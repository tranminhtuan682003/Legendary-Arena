using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData(int balance)
    {
        PlayerPrefs.SetInt("PlayerBalance", balance);
    }

    public int LoadPlayerData()
    {
        return PlayerPrefs.GetInt("PlayerBalance", 100000); // Default to 100000 if no data found
    }
}
