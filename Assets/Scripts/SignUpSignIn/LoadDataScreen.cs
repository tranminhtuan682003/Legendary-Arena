using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Để sử dụng Scene loading
using Zenject;
using System.Collections.Generic;
using TMPro;

public class LoadDataScreen : MonoBehaviour
{
    [Inject] private UILoadScreenManager uILoadScreenManager;
    [Inject] private GameGlobalController gameGlobalController;
    private GameObject nameCompany;
    private GameObject nameGame;
    private GameObject loadScreen;
    private Slider loader;  // Thanh slider để hiển thị tiến độ
    private TextMeshProUGUI textLoadInfor;
    private TextMeshProUGUI process;

    private Dictionary<string, GameObject> screens;

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        StartCoroutine(NextToStart());
    }

    private void InitiaLize()
    {
        screens = new Dictionary<string, GameObject>();
        FindComponent();
        AddScreensToDictionary();
    }

    private void FindComponent()
    {
        nameCompany = transform.Find("Company")?.gameObject;
        nameGame = transform.Find("NameGame")?.gameObject;
        loadScreen = transform.Find("Load")?.gameObject;
        loader = transform.Find("Load/Footer/Field/Slider")?.GetComponent<Slider>();  // Lấy tham chiếu tới Slider
        textLoadInfor = transform.Find("Load/Footer/Field/Loading asset")?.GetComponent<TextMeshProUGUI>();
        process = transform.Find("Load/Footer/Field/Process")?.GetComponent<TextMeshProUGUI>();
    }

    private void AddScreensToDictionary()
    {
        screens.Add("Company", nameCompany);
        screens.Add("NameGame", nameGame);
        screens.Add("Load", loadScreen);
    }

    private IEnumerator NextToStart()
    {
        ShowScreen("Company");
        yield return new WaitForSeconds(1f);
        ShowScreen("NameGame");
        yield return new WaitForSeconds(1f);
        ShowScreen("Load");
        yield return StartCoroutine(LoadGameAssets());
    }

    private IEnumerator LoadGameAssets()
    {
        // Giả sử chúng ta sẽ tải một scene trong game (hoặc tải tài nguyên nào đó)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lounge");  // Tải một scene
        asyncLoad.allowSceneActivation = false;  // Tạm thời không chuyển sang scene mới cho đến khi tải xong
        gameGlobalController.SetSceneLoungeAsyncOperation(asyncLoad);

        while (!asyncLoad.isDone)
        {
            // Cập nhật thanh loader theo tiến độ tải
            loader.value = asyncLoad.progress;

            // Hiển thị phần trăm tải lên text
            float progress = asyncLoad.progress * 100; // Chuyển đổi thành phần trăm
            process.text = $"Loading: {progress:0}%";  // Cập nhật phần trăm

            // Cập nhật thông tin tải (tuỳ theo bạn muốn hiển thị gì thêm)
            if (asyncLoad.progress < 0.9f)
            {
                textLoadInfor.text = "Loading assets...";  // Ví dụ: Đang tải tài nguyên
            }
            else
            {
                textLoadInfor.text = "Almost there...";  // Ví dụ: Gần xong
            }

            // Khi tải gần xong, cho phép chuyển sang scene mới
            if (asyncLoad.progress >= 0.9f)
            {
                loader.value = 1f;  // Đảm bảo thanh loader hoàn thành
                yield return new WaitForSeconds(2f);
                uILoadScreenManager.ShowScreen("SignIn");
            }

            yield return null;
        }
    }

    public void ShowScreen(string screenName)
    {
        // Ẩn tất cả các màn hình
        foreach (var screen in screens.Values)
        {
            screen.SetActive(false);
        }

        // Hiển thị màn hình cần thiết
        if (screens.ContainsKey(screenName))
        {
            screens[screenName].SetActive(true);
        }
    }
}
