using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingInGamePlayKnight : MonoBehaviour
{
    [Inject] SoundKnightManager soundKnightManager;
    [Inject] UIKnightManager uIKnightManager;
    private Animator animator;
    private Button setting;
    private Button increaseVolume;
    private Button volumeDown;
    private Button exitGame;
    private Slider volume;
    private bool isRunning;

    private void Awake()
    {
        Initialize();
        SetOnclickButton();

    }

    void Start()
    {
        animator.SetTrigger("Idle");
    }

    private void Initialize()
    {
        animator = GetComponent<Animator>();
        setting = transform.Find("Setting").GetComponent<Button>();
        increaseVolume = transform.Find("IncreaseVolume").GetComponent<Button>();
        volumeDown = transform.Find("VolumeDown").GetComponent<Button>();
        exitGame = transform.Find("ExitGame").GetComponent<Button>();
        volume = transform.Find("IncreaseVolume/Volume").GetComponent<Slider>();
    }
    private void SetOnclickButton()
    {
        setting.onClick.AddListener(HandleSettingClick);
        increaseVolume.onClick.AddListener(HandleIncreaseVolumeClick);
        volumeDown.onClick.AddListener(HandleVolumeDownClick);
        exitGame.onClick.AddListener(HandlExitGameClick);
        volume.value = 1;
    }

    private void HandleSettingClick()
    {
        if (isRunning)
        {
            animator.SetTrigger("Idle"); // Chuyển về trạng thái Idle
        }
        else
        {
            animator.SetTrigger("Run"); // Chuyển sang trạng thái Run
        }

        isRunning = !isRunning; // Đảo ngược trạng thái
    }

    private void HandleIncreaseVolumeClick()
    {
        float newVolume = Mathf.Clamp(volume.value + 0.1f, 0f, 1f); // Tăng 10%
        volume.value = newVolume;
        soundKnightManager.IncreaseVolume();

    }
    private void HandleVolumeDownClick()
    {
        float newVolume = Mathf.Clamp(volume.value - 0.1f, 0f, 1f); // Tăng 10%
        volume.value = newVolume;
        soundKnightManager.DecreaseVolume();
    }

    private void HandlExitGameClick()
    {
        uIKnightManager.ChangeStatePlayScreen(false);
        uIKnightManager.ChangeStateStartScreen(true);
    }

}
