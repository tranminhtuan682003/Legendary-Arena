using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavigationManager : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI nameButton;
    private Image iconImage;

    private float fadeDuration = 0.25f;
    private float delayBeforeFadeBack = 0.5f;

    private void Start()
    {
        button = GetComponent<Button>();
        nameButton = GetComponentInChildren<TextMeshProUGUI>();
        iconImage = GetComponentInChildren<Image>();
        SetAlpha(nameButton, 0);
        button.onClick.AddListener(() =>
        {
            if (!UIManager.Instance.isAnyButtonProcessing)
            {
                StartCoroutine(HandleClick());
            }
        });
    }

    private IEnumerator HandleClick()
    {
        UIManager.Instance.isAnyButtonProcessing = true;
        UIManager.Instance.ShowScreen(gameObject.name);
        UIManager.Instance.MoveSelectScreen(0.5f, button.GetComponent<RectTransform>());

        yield return Fade(iconImage, 1, 0, fadeDuration);
        yield return Fade(nameButton, 0, 1, fadeDuration);

        yield return new WaitForSeconds(delayBeforeFadeBack);

        yield return Fade(nameButton, 1, 0, fadeDuration);
        yield return Fade(iconImage, 0, 1, fadeDuration);

        UIManager.Instance.isAnyButtonProcessing = false;
    }

    private IEnumerator Fade(Graphic uiElement, float from, float to, float duration)
    {
        float elapsedTime = 0;
        Color color = uiElement.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            uiElement.color = new Color(color.r, color.g, color.b, Mathf.Lerp(from, to, t));
            yield return null;
        }

        uiElement.color = new Color(color.r, color.g, color.b, to);
    }

    private void SetAlpha(Graphic uiElement, float alpha)
    {
        Color color = uiElement.color;
        uiElement.color = new Color(color.r, color.g, color.b, alpha);
    }
}
