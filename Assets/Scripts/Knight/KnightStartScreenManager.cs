using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class KnightStartScreenManager : MonoBehaviour
{
    private Button playGameButton;
    private Button exitGame;
    private GameObject readyScreen;
    private Button ready;
    private TextMeshProUGUI timer;
    private Image yourIcon;

    // Injected Managers
    private UIKnightManager uiKnightManager;

    // Countdown variables
    private Coroutine countdownCoroutine;
    private int countdownTime = 30; // 30 seconds countdown

    // Colors for the icon
    private Color defaultColor = Color.white; // Original color of the icon
    private Color dimmedColor = Color.gray;   // Dimmed color when screen is active

    [Inject]
    public void Construct(UIKnightManager uiKnightManager)
    {
        this.uiKnightManager = uiKnightManager;
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        readyScreen.SetActive(false);
        exitGame.gameObject.SetActive(true);
    }

    private void Start()
    {
        playGameButton.onClick.AddListener(HandlePlayGameClick);
        ready.onClick.AddListener(HandleReadyClick);
        exitGame.onClick.AddListener(HandleExitClick);
    }

    private void Initialize()
    {
        playGameButton = transform.Find("PlayGame")?.GetComponent<Button>();
        exitGame = transform.Find("ExitGame")?.GetComponent<Button>();
        readyScreen = transform.Find("ReadyScreen").gameObject;
        ready = transform.Find("ReadyScreen/ButtonReady")?.GetComponent<Button>();
        timer = transform.Find("ReadyScreen/Bar/Timer")?.GetComponent<TextMeshProUGUI>();
        yourIcon = transform.Find("ReadyScreen/Bar/You/Icon")?.GetComponent<Image>();
    }

    private void HandleReadyClick()
    {
        // Restore the icon color
        SetYourIconColor(defaultColor);

        // Wait 1 second before executing the main logic
        StartCoroutine(DelayedReadyLogic());
    }

    private void HandlePlayGameClick()
    {
        // Show the ready screen
        exitGame.gameObject.SetActive(false);
        readyScreen.SetActive(true);

        // Dim the icon color
        SetYourIconColor(dimmedColor);

        // Start countdown
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        int remainingTime = countdownTime;

        while (remainingTime > 0)
        {
            timer.text = remainingTime.ToString(); // Update UI with remaining time
            yield return new WaitForSeconds(1f);  // Wait for 1 second
            remainingTime--;
        }

        timer.text = "0"; // Display 0 when time is up

        // Trigger logic when countdown finishes
        OnCountdownFinished();
    }

    private void OnCountdownFinished()
    {
        // Countdown ends
        Debug.Log("Countdown finished!");

        // Check if the player has not pressed the ready button
        if (readyScreen.activeSelf)
        {
            Debug.Log("Player did not press Ready. Hiding the ready screen.");
            readyScreen.SetActive(false);
        }
    }

    private IEnumerator DelayedReadyLogic()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Change screen states and start gameplay
        uiKnightManager.ChangeStateStartScreen(false);
        uiKnightManager.ChangeStatePlayScreen(true);
        uiKnightManager.CreateGamePlay1();

        // Stop countdown if still running
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    private void SetYourIconColor(Color color)
    {
        if (yourIcon != null)
        {
            yourIcon.color = color;
        }
    }

    private void HandleExitClick()
    {
        SceneManager.LoadScene("Lounge");
    }
}
