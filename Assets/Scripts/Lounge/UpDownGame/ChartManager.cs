using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    private TextMeshProUGUI seasion;
    private TextMeshProUGUI date;
    private TextMeshProUGUI betMoney;
    private TextMeshProUGUI winMoney;
    private TextMeshProUGUI detail;

    void Start()
    {
        seasion = FindComponentInChild<TextMeshProUGUI>("");
        date = FindComponentInChild<TextMeshProUGUI>("");
        betMoney = FindComponentInChild<TextMeshProUGUI>("");
        winMoney = FindComponentInChild<TextMeshProUGUI>("");
        detail = FindComponentInChild<TextMeshProUGUI>("");
    }

    void Update()
    {

    }

    private T FindComponentInChild<T>(string path) where T : Component
    {
        var component = transform.Find(path)?.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"{typeof(T).Name} component at path '{path}' not found.");
        }
        return component;
    }
}
