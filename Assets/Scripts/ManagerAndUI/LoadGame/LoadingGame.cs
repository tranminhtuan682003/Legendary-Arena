using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadingGame : MonoBehaviour
{
    public GameObject NameCompany; // Panel đầu tiên
    public GameObject NameGame; // Panel thứ hai
    public GameObject loadingPanel; // Panel loading
    public GameObject loginPanel; // Panel đăng nhập
    public Slider loadingSlider; // Slider tải
    public TextMeshProUGUI loadingText; // Văn bản tải

    private void Start()
    {
        StartCoroutine(ShowPanels());
    }

    private IEnumerator ShowPanels()
    {
        NameCompany.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Chờ 1.5 giây
        NameCompany.SetActive(false);
        NameGame.SetActive(true);
        yield return new WaitForSeconds(1.5f); // Chờ 1.5 giây
        NameGame.SetActive(false);
        loadingPanel.SetActive(true);
        yield return StartCoroutine(LoadAsync()); // Gọi hàm tải
    }

    private IEnumerator LoadAsync()
    {
        // Tải tất cả tài nguyên cho cảnh
        // Bạn có thể sử dụng Resources.Load hoặc Addressables tùy thuộc vào cách tổ chức tài nguyên của bạn
        // Ở đây, chúng ta sẽ giả lập việc tải tài nguyên
        for (float progress = 0; progress <= 1; progress += 0.05f)
        {
            loadingSlider.value = progress; // Cập nhật giá trị Slider

            // Cập nhật văn bản tải với phần trăm
            loadingText.text = "Đang tải tài nguyên... " + (progress * 100).ToString("F0") + "%";
            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây để giả lập quá trình tải
        }

        // Tại đây bạn có thể thêm mã để tải các tài nguyên cụ thể
        // Ví dụ: GameObject prefab = Resources.Load<GameObject>("Path/To/Your/Prefab");
        // Khi hoàn tất tải, bạn có thể quyết định giữ panel loading hoặc chuyển sang cảnh sau
        // Ví dụ: ẩn panel loading và hiện panel đăng nhập
        loadingPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    // Hàm để chuyển sang cảnh mới khi cần thiết
    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log("Đang tải cảnh: " + sceneName);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null; // Chờ cho đến khi cảnh tải xong
        }

        Debug.Log("Chuyển sang cảnh " + sceneName + " thành công.");
    }
}
