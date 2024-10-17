using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameManager : MonoBehaviour
{
    private Image background;
    private GameObject gameMode;  // Chỉ sử dụng một GameObject cho mọi chế độ
    private string previousGameMode;  // Biến để lưu giá trị trước đó của nameGame
    private Dictionary<string, Sprite> backgrounds;

    void Start()
    {
        InitBackground();
        InitComponent();
        SetUpComponent();
        previousGameMode = "";  // Khởi tạo giá trị ban đầu
    }

    void Update()
    {
        // Kiểm tra xem nameGame có thay đổi không trước khi thực hiện chuyển đổi chế độ
        if (previousGameMode != UIManager.Instance.nameGame)
        {
            SwichGameMode();
            ChangeBackground(UIManager.Instance.nameGame);
            previousGameMode = UIManager.Instance.nameGame;  // Cập nhật giá trị mới
        }
    }

    private void InitBackground()
    {
        backgrounds = new Dictionary<string, Sprite>
        {
            { "1vs1", Resources.Load<Sprite>("NewUI/BackgroundGameMode/bg2") },
            { "3vs3", Resources.Load<Sprite>("NewUI/BackgroundGameMode/bg3") },
            { "5vs5", Resources.Load<Sprite>("NewUI/BackgroundGameMode/bg4") },
            { "10vs10", Resources.Load<Sprite>("NewUI/BackgroundGameMode/bg5") },
            { "UpDown", Resources.Load<Sprite>("NewUI/BackgroundGameMode/bg6") },
            { "XXX", Resources.Load<Sprite>("NewUI/BackgroundGameMode/xxx") },
        };
    }
    private void ChangeBackground(string gameMode)
    {
        if (backgrounds.ContainsKey(gameMode))
        {
            background.sprite = backgrounds[gameMode];
        }
        else
        {
            Debug.LogError("Không tìm thấy background cho tướng: " + gameMode);
        }
    }

    private void InitComponent()
    {
        background = transform.Find("BackgroundGameMode").GetComponent<Image>();
        gameMode = transform.Find("GameMode").gameObject;  // Sử dụng chung một GameObject
    }

    private void SetUpComponent()
    {
        gameMode.SetActive(false);  // Mặc định tắt gameMode khi khởi động
    }

    private void SwichGameMode()
    {
        gameMode.SetActive(false);
        switch (UIManager.Instance.nameGame)
        {
            case "1vs1":
                gameMode.SetActive(true);
                break;
            case "UpDown":
                gameMode.SetActive(true);
                break;
            case "10vs10":
                gameMode.SetActive(true);
                break;
            case "3vs3":
                gameMode.SetActive(true);
                break;
            case "5vs5":
                gameMode.SetActive(true);
                break;
            case "XXX":
                gameMode.SetActive(true);
                break;
            default:
                gameMode.SetActive(false);
                break;
        }
    }
}
