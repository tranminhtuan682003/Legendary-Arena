using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    private Dictionary<string, CanvasGroup> textCanvasGroups = new Dictionary<string, CanvasGroup>();
    private Dictionary<string, CanvasGroup> buttonCanvasGroups = new Dictionary<string, CanvasGroup>();

    private RectTransform Select;
    private float moveSpeed = 5f;
    private float fadeDuration = 0.25f;

    private UIEffectManager effectManager;

    #region Unity Callbacks

    private void Awake()
    {
        effectManager = GetComponent<UIEffectManager>();
        FindSelect();
        InitializeButtons();
        InitializePanels();
        InitializeTextCanvasGroups();
        InitializeButtonCanvasGroups();
        InitSelectPosition();
        RegisterButtonEvents();

        // Set initial alpha values
        SetInitialAlphaValues();
    }

    #endregion

    #region Initialization Methods

    private void FindSelect()
    {
        GameObject selectObject = GameObject.Find("Select");
        if (selectObject != null)
        {
            Select = selectObject.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Select not found!");
        }
    }

    private void InitializeButtons()
    {
        AddButton("HomeButton");
        AddButton("ShopButton");
        AddButton("HeroButton");
        AddButton("BagButton");
        AddButton("PlayButton");
    }

    private void AddButton(string buttonName)
    {
        Button button = GameObject.Find(buttonName)?.GetComponent<Button>();
        if (button != null)
        {
            buttons.Add(buttonName, button);
        }
    }

    private void InitializePanels()
    {
        AddPanel("Home");
        AddPanel("Shop");
        AddPanel("Hero");
        AddPanel("Bag");
        AddPanel("Game");
    }

    private void AddPanel(string panelName)
    {
        GameObject panel = GameObject.Find(panelName);
        if (panel != null)
        {
            panels.Add(panelName, panel);
        }
    }

    private void InitializeTextCanvasGroups()
    {
        AddTextCanvasGroup("HomeButtonText");
        AddTextCanvasGroup("ShopButtonText");
        AddTextCanvasGroup("HeroButtonText");
        AddTextCanvasGroup("BagButtonText");
        AddTextCanvasGroup("PlayButtonText");
    }

    private void AddTextCanvasGroup(string textName)
    {
        TextMeshProUGUI text = GameObject.Find(textName)?.GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            CanvasGroup canvasGroup = text.gameObject.AddComponent<CanvasGroup>();
            textCanvasGroups.Add(textName, canvasGroup);
        }
    }

    private void InitializeButtonCanvasGroups()
    {
        foreach (var buttonName in buttons.Keys)
        {
            Button button = buttons[buttonName];
            CanvasGroup canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
            buttonCanvasGroups.Add(buttonName, canvasGroup);
        }
    }

    private void SetInitialAlphaValues()
    {
        foreach (var canvasGroup in textCanvasGroups.Values)
        {
            effectManager.SetCanvasGroupAlpha(canvasGroup, 0f);
        }

        foreach (var canvasGroup in buttonCanvasGroups.Values)
        {
            effectManager.SetCanvasGroupAlpha(canvasGroup, 1f);
        }
    }

    #endregion

    #region Button Events

    private void RegisterButtonEvents()
    {
        RegisterButtonEvent("HomeButton", "Home", "HomeButtonText");
        RegisterButtonEvent("ShopButton", "Shop", "ShopButtonText");
        RegisterButtonEvent("HeroButton", "Hero", "HeroButtonText");
        RegisterButtonEvent("BagButton", "Bag", "BagButtonText");
        RegisterButtonEvent("PlayButton", "Game", "PlayButtonText");
    }

    private void RegisterButtonEvent(string buttonName, string panelName, string textName)
    {
        if (buttons.TryGetValue(buttonName, out Button button))
        {
            button.onClick.AddListener(() => OpenPanelMoveAndShowText(panelName, button.GetComponent<RectTransform>(), buttonName, textName));
        }
    }

    private void OpenPanelMoveAndShowText(string panelName, RectTransform buttonRect, string buttonName, string textName)
    {
        OpenPanel(panelName);
        MoveIndicatorToButton(buttonRect);
        StartCoroutine(effectManager.FadeAndResetCanvasGroups(textCanvasGroups, buttonCanvasGroups, textName, buttonName, fadeDuration));
    }

    private void OpenPanel(string panelName)
    {
        foreach (var panel in panels.Values)
        {
            panel.SetActive(false);
        }

        if (panels.ContainsKey(panelName))
        {
            panels[panelName].SetActive(true);

            if (panelName == "Home")
            {
                BannerAutoScroll bannerAutoScroll = panels[panelName].GetComponentInChildren<BannerAutoScroll>();
                if (bannerAutoScroll != null)
                {
                    bannerAutoScroll.ResetScroll();
                }
            }
        }
    }


    private void MoveIndicatorToButton(RectTransform buttonRect)
    {
        if (Select != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveIndicatorCoroutine(buttonRect.position));
        }
    }

    private IEnumerator MoveIndicatorCoroutine(Vector3 targetPosition)
    {
        Vector3 startPosition = Select.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            Select.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        Select.position = targetPosition;
    }

    #endregion

    #region Initialization Methods

    private void InitSelectPosition()
    {
        if (buttons.TryGetValue("HomeButton", out Button homeButton))
        {
            RectTransform homeButtonRect = homeButton.GetComponent<RectTransform>();
            if (Select != null && homeButtonRect != null)
            {
                Select.position = homeButtonRect.position;
            }
        }
    }

    #endregion
}
