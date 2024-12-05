using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Zenject;
using System.Collections;

public class SignUpScreenManager : MonoBehaviour
{
    [Inject] private UILoadScreenManager uILoadScreenManager;

    // Các trường nhập liệu (sử dụng TMP_InputField)
    private TMP_InputField userName;
    private TMP_InputField email;
    private TMP_InputField password;
    private TMP_InputField rePassword;
    private Button signUpButton;
    private Button signInButton;
    private GameObject error;

    private void Awake()
    {
        InitiaLize();
    }

    private void InitiaLize()
    {
        FindComponent();
        AddButtonOnclick();
    }

    private void FindComponent()
    {
        userName = transform.Find("Body/Panel/Body/InputField/UserName/InputField")?.GetComponent<TMP_InputField>();
        email = transform.Find("Body/Panel/Body/InputField/Input/Email/InputField")?.GetComponent<TMP_InputField>();
        password = transform.Find("Body/Panel/Body/InputField/Input/Password/InputField")?.GetComponent<TMP_InputField>();
        rePassword = transform.Find("Body/Panel/Body/InputField/Input/RePassword/InputField")?.GetComponent<TMP_InputField>();

        signUpButton = transform.Find("Body/Panel/Body/MothodLogin/SignupButton")?.GetComponent<Button>();
        signInButton = transform.Find("Body/Panel/Footer/SignInButton")?.GetComponent<Button>();
        error = transform.Find("Error")?.gameObject;

        error.SetActive(false);

        if (userName == null || email == null || password == null || rePassword == null || signUpButton == null || signInButton == null)
        {
            Debug.LogError("Có một hoặc nhiều component bị thiếu trong SignUpScreenManager.");
        }
    }


    private void AddButtonOnclick()
    {
        signInButton.onClick.AddListener(() => HandleSignInButtonClick());
        signUpButton.onClick.AddListener(() => HandleSignUpButtonClick());
    }

    private void HandleSignInButtonClick()
    {
        uILoadScreenManager.ShowScreen("SignIn");
    }

    private void HandleSignUpButtonClick()
    {
        string username = userName.text;
        string emailAddress = email.text;
        string userPassword = password.text;
        string confirmPassword = rePassword.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(userPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowError("Vui lòng điền đầy đủ các thông tin!");
            return;
        }

        if (userPassword != confirmPassword)
        {
            ShowError("Mật khẩu không khớp!");
            return;
        }

        if (userPassword.Length < 6)
        {
            ShowError("Mật khẩu phải có ít nhất 6 ký tự!");
            return;
        }

        if (!IsValidEmail(emailAddress))
        {
            ShowError("Email không hợp lệ!");
            return;
        }

        var registerRequest = new RegisterPlayFabUserRequest()
        {
            Username = username,
            Email = emailAddress,
            Password = userPassword
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnSignUpSuccess, OnSignUpFailure);
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Đăng ký thành công!");
        uILoadScreenManager.ShowScreen("SignIn");
    }

    private void OnSignUpFailure(PlayFabError error)
    {
        Debug.Log($"Đăng ký thất bại: {error.GenerateErrorReport()}");
        ShowError($"Đăng ký thất bại: {error.ErrorMessage}");
    }

    private IEnumerator ShowError(string message)
    {
        Debug.Log(message);
        error.SetActive(true);
        yield return new WaitForSeconds(2f);
        error.SetActive(false);
    }
}
