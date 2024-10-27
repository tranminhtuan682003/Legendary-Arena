using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HanldeClick);
    }

    private void HanldeClick()
    {
        UIManager.Instance.nameGame = button.name;
    }
}
