using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Zenject;

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

    private void Awake()
    {
        InitiaLize();
    }

    private void Start()
    {
        // Đây là nơi bạn có thể thực hiện các thiết lập thêm nếu cần
    }

    private void InitiaLize()
    {
        FindComponent();
        AddButtonOnclick();
    }

    private void FindComponent()
    {
        // Tìm các TMP_InputField và Button
        userName = transform.Find("Body/Panel/Body/InputField/UserName/InputField")?.GetComponent<TMP_InputField>();
        email = transform.Find("Body/Panel/Body/InputField/Input/Email/InputField")?.GetComponent<TMP_InputField>();
        password = transform.Find("Body/Panel/Body/InputField/Input/Password/InputField")?.GetComponent<TMP_InputField>();
        rePassword = transform.Find("Body/Panel/Body/InputField/Input/RePassword/InputField")?.GetComponent<TMP_InputField>();

        signUpButton = transform.Find("Body/Panel/Body/MothodLogin/SignupButton")?.GetComponent<Button>();
        signInButton = transform.Find("Body/Panel/Footer/SignInButton")?.GetComponent<Button>();
    }

    private void AddButtonOnclick()
    {
        // Đăng ký sự kiện click cho các nút
        signInButton.onClick.AddListener(() => HandleSignInButtonClick());
        signUpButton.onClick.AddListener(() => HandleSignUpButtonClick());
    }

    private void HandleSignInButtonClick()
    {
        // Chuyển màn hình sang Sign In
        uILoadScreenManager.ShowScreen("SignIn");
    }

    private void HandleSignUpButtonClick()
    {
        // Lấy giá trị từ các input field
        string username = userName.text;
        string emailAddress = email.text;
        string userPassword = password.text;
        string confirmPassword = rePassword.text;

        // Kiểm tra tính hợp lệ của các trường nhập liệu
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

        // Tạo yêu cầu đăng ký người dùng
        var registerRequest = new RegisterPlayFabUserRequest()
        {
            Username = username,
            Email = emailAddress,
            Password = userPassword
        };

        // Gọi PlayFab API để đăng ký người dùng
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnSignUpSuccess, OnSignUpFailure);
    }

    // Kiểm tra định dạng email hợp lệ
    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    // Đăng ký thành công
    private void OnSignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Đăng ký thành công!");

        // Sau khi đăng ký thành công, chuyển sang màn hình đăng nhập
        uILoadScreenManager.ShowScreen("SignIn");
    }

    // Đăng ký thất bại
    private void OnSignUpFailure(PlayFabError error)
    {
        Debug.LogError($"Đăng ký thất bại: {error.GenerateErrorReport()}");
        ShowError($"Đăng ký thất bại: {error.ErrorMessage}");
    }

    // Hiển thị thông báo lỗi
    private void ShowError(string message)
    {
        Debug.LogError(message);
        // Bạn có thể thay thế hàm này bằng cách hiển thị một thông báo trên UI nếu cần
    }
}
