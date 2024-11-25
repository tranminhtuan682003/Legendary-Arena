using UnityEngine;
using UnityEngine.SceneManagement;
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
        // SelectGameManager.Instance.SceneTransitionState(true);
        UIManager.Instance.HideNavigation("Navigation");
        switch (UIManager.Instance.nameGame)
        {
            case "1vs1":
                UIManager.Instance.isPickHeroScreen = true;
                UIManager.Instance.ShowScreen("PickHero");
                break;
            case "FlappyBird":
                SceneManager.LoadScene("FlappyBird");
                break;
            case "CandyCrush":
                SceneManager.LoadScene("CandyCrush");
                break;
            case "HollowKnight":
                SceneManager.LoadScene("MonsterSHot");
                break;
            case "Sicbo":
                SceneManager.LoadScene("SichBo");
                break;
            case "None":
                SceneManager.LoadScene("SichBo");
                break;
            default:
                break;
        }
    }
}
