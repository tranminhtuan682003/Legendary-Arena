using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameManager : MonoBehaviour
{
    public static SelectGameManager Instance;
    private Image background;
    private GameObject gameMode;  // Chỉ sử dụng một GameObject cho mọi chế độ
    private string previousGameMode;  // Biến để lưu giá trị trước đó của nameGame
    private Dictionary<string, Sprite> backgrounds;

    //SceneTransition
    private GameObject sceneTransition;
    private GameObject ready;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        sceneTransition = GameObject.Find("SceneTransition");
        ready = GameObject.Find("Ready");

        SceneTransitionState(false);
        ReadyScreenState(false);

    }

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
            { "1vs1", Resources.Load<Sprite>("UI/BackgroundGameMode/bg2") },
            { "FlappyBird", Resources.Load<Sprite>("UI/BackgroundGameMode/bg3") },
            { "HollowKnight", Resources.Load<Sprite>("UI/BackgroundGameMode/bg4") },
            { "CandyCrush", Resources.Load<Sprite>("UI/BackgroundGameMode/bg5") },
            { "Sicbo", Resources.Load<Sprite>("UI/BackgroundGameMode/bg6") },
            { "XXX", Resources.Load<Sprite>("UI/BackgroundGameMode/xxx") },
            { "None", Resources.Load<Sprite>("UI/BackgroundGameMode/rank") },
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
            case "Sicbo":
                gameMode.SetActive(true);
                break;
            case "FlappyBird":
                gameMode.SetActive(true);
                break;
            case "HollowKnight":
                gameMode.SetActive(true);
                break;
            case "CandyCrush":
                gameMode.SetActive(true);
                break;
            case "XXX":
                gameMode.SetActive(true);
                break;
            case "None":
                gameMode.SetActive(true);
                break;
            default:
                gameMode.SetActive(false);
                break;
        }
    }

    public void SceneTransitionState(bool state)
    {
        if (sceneTransition != null)
        {
            sceneTransition.SetActive(state);
        }
    }

    public void ReadyScreenState(bool state)
    {
        if (ready != null)
        {
            ready.SetActive(state);
        }
    }
}
