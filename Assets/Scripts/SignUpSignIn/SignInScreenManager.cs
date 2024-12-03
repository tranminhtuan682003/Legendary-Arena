using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Zenject;
using TMPro;
using UnityEngine.SceneManagement;

public class SignInScreenManager : MonoBehaviour
{
    private TMP_InputField email;
    private TMP_InputField password;
    private Button signInButton;
    private Button signUpButton;
    [Inject] private UILoadScreenManager uILoadScreenManager;

    private void Awake()
    {
        InitiaLize();
    }

    void Start()
    {
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
    }

    private void AddButtonOnclick()
    {
        signInButton.onClick.AddListener(() => HandleSignInButtonClick());
        signUpButton.onClick.AddListener(() => HandleSignUpButtonClick());
    }

    private void HandleSignInButtonClick()
    {
        // Lấy thông tin từ các trường nhập liệu
        string emailValue = email.text;
        string passwordValue = password.text;

        if (string.IsNullOrEmpty(emailValue) || string.IsNullOrEmpty(passwordValue))
        {
            Debug.LogError("Email or Password cannot be empty!");
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

    // Hàm khi đăng nhập thành công
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");

        // Chuyển đến màn hình chính của game hoặc dashboard
        SceneManager.LoadScene("Lounge");
    }

    // Hàm khi đăng nhập thất bại
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login failed: " + error.ErrorMessage);

        // Hiển thị thông báo lỗi cho người dùng
        // Có thể thông báo lỗi đăng nhập trong UI
        // uILoadScreenManager.ShowError("Login failed: " + error.ErrorMessage);
    }

    private void HandleSignUpButtonClick()
    {
        uILoadScreenManager.ShowScreen("SignUp");
    }
}
