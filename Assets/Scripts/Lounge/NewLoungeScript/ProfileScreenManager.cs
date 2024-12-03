using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProfileScreenManager : MonoBehaviour
{
    [Inject] private UILoungeManager uILoungeManager;

    private Button exitButton;

    private void Awake()
    {
        exitButton = FindButtonByName(transform, "ExitButton");
        exitButton.onClick.AddListener(OnExitButtonClick);
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

    private void OnExitButtonClick()
    {
        uILoungeManager.StateNavigation(true);
        uILoungeManager.ShowScreen("HomeScreen");
    }
}
