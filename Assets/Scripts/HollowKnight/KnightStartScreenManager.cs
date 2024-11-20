using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections;

public class KnightStartScreenManager : MonoBehaviour
{
    private Button playGameButton;

    // Injected Managers
    private UIKnightManager uiKnightManager;

    [Inject]
    public void Construct(UIKnightManager uiKnightManager)
    {
        this.uiKnightManager = uiKnightManager;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        playGameButton.onClick.AddListener(HandlePlayGameClick);
    }

    private void Initialize()
    {
        playGameButton = transform.Find("PlayGame")?.GetComponent<Button>();
    }

    private void HandlePlayGameClick()
    {
        Debug.Log("PlayGame Button clicked, transitioning screens...");
        uiKnightManager.ChangeStateStartScreen(false);
        uiKnightManager.ChangeStatePlayScreen(true);
        uiKnightManager.CreateGamePlay1();
    }
}
