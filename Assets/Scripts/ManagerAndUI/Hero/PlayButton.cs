using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { HandleClick(); });
    }

    private void HandleClick()
    {
        if (UIManager.Instance.nameHero == null)
        {
            Debug.Log("chua chon tuong");
            return;
        }
        SceneManager.LoadScene("1vs1");
        UIManager.Instance.InitHero();
    }
}
