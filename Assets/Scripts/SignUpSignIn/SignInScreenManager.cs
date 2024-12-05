using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Zenject;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class SignInScreenManager : MonoBehaviour
{
    private TMP_InputField email;
    private TMP_InputField password;
    private Button signInButton;
    private Button signUpButton;
    private GameObject error;
    private Button forgotPassword;
    private ScrollRect methodLogin;
    [Inject] private UILoadScreenManager uILoadScreenManager;
    [Inject] private GameGlobalController gameGlobalController;

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        methodLogin.verticalNormalizedPosition = 1f;
    }

    private void InitiaLize()
    {
        FindComponent();
        AddButtonOnclick();
    }

    private void FindComponent()
    {
        email = transform.Find("Body/Panel/Body/Input/EmailAndPassword/Email/InputField")?.GetComponent<TMP_InputField>();
        password = transform.Find("Body/Panel/Body/Input/EmailAndPassword/Password/InputField")?.GetComponent<TMP_InputField>();
        signInButton = transform.Find("Body/Panel/Body/MothodLogin/Scroll/Viewport/Content/SigninButton")?.GetComponent<Button>();
        signUpButton = transform.Find("Body/Panel/Body/MothodLogin/Scroll/Viewport/Content/SignupButton")?.GetComponent<Button>();
        forgotPassword = transform.Find("Body/Panel/Body/Input/ForgotPassword/Text (TMP)")?.GetComponent<Button>();
        methodLogin = transform.Find("Body/Panel/Body/MothodLogin/Scroll")?.GetComponent<ScrollRect>();
        error = transform.Find("Error")?.gameObject;
        error.SetActive(false);
    }

    private void AddButtonOnclick()
    {
        signInButton.onClick.AddListener(() => HandleSignInButtonClick());
        signUpButton.onClick.AddListener(() => HandleSignUpButtonClick());
        forgotPassword.onClick.AddListener(() => HandleForgotPasswordButtonClick());
    }

    private void HandleSignInButtonClick()
    {
        // Lấy thông tin từ các trường nhập liệu
        string emailValue = email.text;
        string passwordValue = password.text;

        if (string.IsNullOrEmpty(emailValue) || string.IsNullOrEmpty(passwordValue))
        {
            StartCoroutine(ShowError());
            Debug.Log("Email or Password cannot be empty!");
            return;
        }

        // Tạo đối tượng đăng nhập cho PlayFab
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailValue,
            Password = passwordValue
        };

        // Gửi yêu cầu đăng nhập đến PlayFab
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");
        gameGlobalController.GoOnSceneLounge();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Login failed: " + error.ErrorMessage);
    }

    private void HandleSignUpButtonClick()
    {
        uILoadScreenManager.ShowScreen("SignUp");
    }

    private void HandleForgotPasswordButtonClick()
    {
        uILoadScreenManager.ShowScreen("ForgotPassword");
    }

    private IEnumerator ShowError()
    {
        error.SetActive(true);
        yield return new WaitForSeconds(2f);
        error.SetActive(false);
    }
}
