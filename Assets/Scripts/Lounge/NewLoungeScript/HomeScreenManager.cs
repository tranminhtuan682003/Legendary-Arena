using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HomeScreenManager : MonoBehaviour
{
    [Inject] private UILoungeManager uILoungeManager;

    private Button profileButton;

    private void Awake()
    {
        profileButton = FindButtonByName(transform, "ProfileButton");
        profileButton.onClick.AddListener(OnProfileButtonClick);
    }

    private Button FindButtonByName(Transform parentTransform, string buttonName)
    {
        Button[] buttons = parentTransform.GetComponentsInChildren<Button>(true);
        foreach (var button in buttons)
        {
            if (button.name == buttonName)
            {
                return button;
            }
        }
        return null;
    }

    private void OnProfileButtonClick()
    {
        uILoungeManager.StateNavigation(false);
        uILoungeManager.ShowScreen("ProfileScreen");
    }
}
