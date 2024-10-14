using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { HandleClick(); });
    }

    private void HandleClick()
    {
        UIManager.Instance.ShowScreen(gameObject.name);
        if (button.name == "Home")
        {
            UIManager.Instance.ShowNavigation("Navigation");
        }
        else
        {
            UIManager.Instance.HideNavigation("Navigation");
        }
    }
}