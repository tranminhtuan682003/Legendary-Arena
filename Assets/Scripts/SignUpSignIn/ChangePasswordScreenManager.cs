using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangePasswordScreenManager : MonoBehaviour
{
    private Button returnLoginButton;
    [Inject] UILoadScreenManager uILoadScreenManager;

    private void Awake()
    {
        InitiaLize();
    }

    private void InitiaLize()
    {
        FindComponent();
        AddButtonOnClick();
    }

    private void FindComponent()
    {
        returnLoginButton = transform.Find("Body/Panel/Footer/ReturnLoginButton")?.GetComponent<Button>();
    }

    private void AddButtonOnClick()
    {
        returnLoginButton.onClick.AddListener(() => HandleReturnLoginButtonClick());
    }

    private void HandleReturnLoginButtonClick()
    {
        uILoadScreenManager.ShowScreen("SignIn");
    }
}
