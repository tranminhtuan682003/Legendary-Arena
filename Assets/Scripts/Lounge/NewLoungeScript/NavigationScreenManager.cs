using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections;

public class NavigationScreenManager : MonoBehaviour
{
    [Inject] private UILoungeManager uILoungeManager;

    private Button homeButton;
    private Button heroButton;
    private Button shopButton;
    private Button bagButton;
    private Button gameButton;
    private Image SelectButton;

    private TextMeshProUGUI homeButtonText;
    private TextMeshProUGUI heroButtonText;
    private TextMeshProUGUI shopButtonText;
    private TextMeshProUGUI bagButtonText;
    private TextMeshProUGUI gameButtonText;

    private float moveDuration = 0.5f;

    private void Awake()
    {
        InitiaLize();
    }

    private void OnEnable()
    {
        // Khi bật màn hình, chỉ mờ văn bản (TextMeshProUGUI)
        FadeOutTextOnly();
    }

    private void InitiaLize()
    {
        homeButton = FindButtonByName("Home", "HomeButton");
        heroButton = FindButtonByName("Hero", "HeroButton");
        shopButton = FindButtonByName("Shop", "ShopButton");
        bagButton = FindButtonByName("Bag", "BagButton");
        gameButton = FindButtonByName("Play", "GameButton");

        homeButtonText = homeButton.GetComponentInChildren<TextMeshProUGUI>();
        heroButtonText = heroButton.GetComponentInChildren<TextMeshProUGUI>();
        shopButtonText = shopButton.GetComponentInChildren<TextMeshProUGUI>();
        bagButtonText = bagButton.GetComponentInChildren<TextMeshProUGUI>();
        gameButtonText = gameButton.GetComponentInChildren<TextMeshProUGUI>();

        SelectButton = transform.Find("Bacground/SelectButton").GetComponent<Image>();

        homeButton.onClick.AddListener(() => OnButtonClick(homeButton, homeButtonText));
        heroButton.onClick.AddListener(() => OnButtonClick(heroButton, heroButtonText));
        shopButton.onClick.AddListener(() => OnButtonClick(shopButton, shopButtonText));
        bagButton.onClick.AddListener(() => OnButtonClick(bagButton, bagButtonText));
        gameButton.onClick.AddListener(() => OnButtonClick(gameButton, gameButtonText));
    }

    private Button FindButtonByName(string parentName, string buttonName)
    {
        Button button = transform.Find($"Bacground/Content/{parentName}/{buttonName}")?.GetComponent<Button>();
        return button;
    }

    private void OnButtonClick(Button button, TextMeshProUGUI buttonText)
    {
        StartCoroutine(MoveSelectButton(button.transform.position));
        StartCoroutine(FadeButton(button, buttonText));

        switch (button.name)
        {
            case "HomeButton":
                uILoungeManager.ShowScreen("HomeScreen");
                break;
            case "HeroButton":
                uILoungeManager.ShowScreen("HeroScreen");
                break;
            case "ShopButton":
                uILoungeManager.ShowScreen("ShopScreen");
                break;
            case "BagButton":
                uILoungeManager.ShowScreen("BagScreen");
                break;
            case "GameButton":
                uILoungeManager.ShowScreen("GameScreen");
                break;
        }
    }

    private IEnumerator MoveSelectButton(Vector3 targetPosition)
    {
        Vector3 startPosition = SelectButton.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            SelectButton.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SelectButton.transform.position = targetPosition;
    }

    private IEnumerator FadeButton(Button button, TextMeshProUGUI buttonText)
    {
        Image buttonImage = button.GetComponent<Image>();  // Lấy Component Image của Button

        // Đảm bảo Image component không null
        if (buttonImage != null)
        {
            // Mờ hình ảnh trong 0.5s và làm văn bản hiện lên ngay lập tức
            buttonText.CrossFadeAlpha(1f, 0.5f, false); // Lập tức làm cho văn bản hiện lên
            buttonImage.CrossFadeAlpha(0f, 0.5f, false); // Mờ hình ảnh trong 0.5s

            // Đợi 1 giây trước khi làm ngược lại
            yield return new WaitForSeconds(1f);

            // Làm mờ văn bản và làm hình ảnh hiện lại
            buttonText.CrossFadeAlpha(0f, 0.5f, false); // Mờ văn bản trong 0.5s
            buttonImage.CrossFadeAlpha(1f, 0.5f, false); // Làm hình ảnh hiện lên trong 0.5s
        }
        else
        {
            Debug.LogWarning("Button does not have an Image component.");
        }
    }




    private void FadeOutTextOnly()
    {
        // Làm mờ văn bản (TextMeshProUGUI) nhưng để hình ảnh hiện lên
        FadeOutText(homeButton, homeButtonText);
        FadeOutText(heroButton, heroButtonText);
        FadeOutText(shopButton, shopButtonText);
        FadeOutText(bagButton, bagButtonText);
        FadeOutText(gameButton, gameButtonText);
    }

    private void FadeOutText(Button button, TextMeshProUGUI buttonText)
    {
        // Làm mờ chỉ văn bản (TextMeshProUGUI)
        buttonText.CrossFadeAlpha(0f, 0f, false); // Lập tức làm mờ văn bản
    }
}