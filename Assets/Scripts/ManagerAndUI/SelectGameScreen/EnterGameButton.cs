using UnityEngine;
using UnityEngine.UI;

public class EnterGameButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        UIManager.Instance.HideNavigation("Navigation");
        switch (UIManager.Instance.nameGame)
        {
            case "1vs1":
                UIManager.Instance.isPickHeroScreen = true;
                UIManager.Instance.ShowScreen("PickHero");
                break;
            case "1vs3":
                UIManager.Instance.ShowScreen("PickHero");
                break;
            case "1vs5":
                UIManager.Instance.ShowScreen("PickHero");
                break;
            case "1vs10":
                UIManager.Instance.ShowScreen("PickHero");
                break;
            case "UpDown":
                UIManager.Instance.ShowScreen("UpDown");
                break;
            case "XXX":
                UIManager.Instance.ShowScreen("UpDown");
                break;
            default:
                break;
        }
    }
}
