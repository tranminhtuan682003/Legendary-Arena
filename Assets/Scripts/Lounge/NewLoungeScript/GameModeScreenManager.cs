using System.Collections;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameModeScreenManager : MonoBehaviour
{
    [Inject] private UILoungeManager uILoungeManager;
    [Inject] private LoungeManager loungeManager;

    [Header("Button Manager")]
    private Button game1vs1;
    private Button gameFlappyBird;
    private Button gameCandy;
    private Button gameSicbo;
    private Button gameKnight;
    private Button gameNone;
    private Image backgroundGameMode;

    [Header("Button Ready")]
    private Button readyButton;

    [Header("TextButton Manager")]
    private TextMeshProUGUI nameGame1v1;
    private TextMeshProUGUI nameGameFlappyBird;
    private TextMeshProUGUI nameGameCandy;
    private TextMeshProUGUI nameGameSicbo;
    private TextMeshProUGUI nameGameKnight;
    private TextMeshProUGUI nameGameNone;

    private void Awake()
    {
        InitiaLize();
    }

    void Update()
    {

    }

    private void InitiaLize()
    {
        FindButtons();
        FindButtonReady();
        FindNameButtons();
    }

    private void FindButtons()
    {
        game1vs1 = FindButtonByName("1vs1");
        gameFlappyBird = FindButtonByName("FlappyBird");
        gameCandy = FindButtonByName("Candy");
        gameSicbo = FindButtonByName("Sicbo");
        gameKnight = FindButtonByName("Knight");
        gameNone = FindButtonByName("None");
        backgroundGameMode = transform.Find("BackgroundGameMode").GetComponent<Image>();

        game1vs1.onClick.AddListener(() => HandleGameModeClick("1vs1", TypeGame.OneVsOne));
        gameFlappyBird.onClick.AddListener(() => HandleGameModeClick("FlappyBird", TypeGame.FlappyBird));
        gameCandy.onClick.AddListener(() => HandleGameModeClick("Candy", TypeGame.Candy));
        gameSicbo.onClick.AddListener(() => HandleGameModeClick("Sicbo", TypeGame.Sicbo));
        gameKnight.onClick.AddListener(() => HandleGameModeClick("Knight", TypeGame.Knight));
        gameNone.onClick.AddListener(() => HandleGameModeClick("None", TypeGame.None));
    }

    private void FindNameButtons()
    {
        nameGame1v1 = game1vs1.GetComponentInChildren<TextMeshProUGUI>();
        nameGameCandy = gameCandy.GetComponentInChildren<TextMeshProUGUI>();
        nameGameFlappyBird = gameFlappyBird.GetComponentInChildren<TextMeshProUGUI>();
        nameGameKnight = gameKnight.GetComponentInChildren<TextMeshProUGUI>();
        nameGameSicbo = gameSicbo.GetComponentInChildren<TextMeshProUGUI>();
        nameGameNone = gameNone.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void FindButtonReady()
    {
        readyButton = transform.Find("GameMode/RightBar/FriendArea/ReadyButton")?.GetComponent<Button>();
        readyButton.onClick.AddListener(() => HandleReadyClick());
    }

    private Button FindButtonByName(string buttonName)
    {
        Button button = transform.Find($"LeftBar/Modes/Mode/Area/Scroll View/Viewport/Content/{buttonName}")?.GetComponent<Button>();
        return button;
    }

    private void HandleGameModeClick(string nameGame, TypeGame typeGame)
    {
        ChangeBackground(nameGame);
        loungeManager.typeGame = typeGame;
    }

    private void ChangeBackground(string nameGame)
    {
        backgroundGameMode.sprite = Resources.Load<Sprite>("UI/BackgroundGameMode/" + nameGame);
    }

    private void HandleReadyClick()
    {
        string sceneGame = loungeManager.typeGame.ToString();
        if (loungeManager.typeGame == TypeGame.None)
        {
            return;
        }
        if (loungeManager.typeGame == TypeGame.OneVsOne)
        {
            uILoungeManager.StateNavigation(false);
            uILoungeManager.ShowScreen("Ready");
        }
        else
        {
            LoadScene(sceneGame);
        }
    }

    private void LoadScene(string sceneGame)
    {
        SceneManager.LoadScene(sceneGame);
    }

}
