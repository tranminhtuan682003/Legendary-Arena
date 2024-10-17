using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (!UIManager.Instance.isAnyButtonProcessing)
        {
            string buttonName = button.name;

            // Sử dụng switch-case thay cho if-else
            switch (buttonName)
            {
                case "Home":
                    UIManager.Instance.ShowNavigation("Navigation");
                    break;
                case "Game":
                    UIManager.Instance.ShowNavigation("Navigation");
                    break;
                default:
                    UIManager.Instance.HideNavigation("Navigation");
                    break;
            }
            UIManager.Instance.ShowScreen(buttonName);
        }
    }
}
