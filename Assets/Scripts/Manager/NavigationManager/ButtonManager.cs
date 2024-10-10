using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI nameButton;
    private Image iconImage;
    private bool isProcessing = false;

    public float fadeDuration = 0.5f;
    public float delayBeforeFadeBack = 1f;

    void Start()
    {
        button = GetComponent<Button>();
        nameButton = GetComponentInChildren<TextMeshProUGUI>();
        iconImage = GetComponentInChildren<Image>();
        SetAlpha(nameButton, 0);  // Ẩn text ban đầu
        button.onClick.AddListener(() => { if (!isProcessing) StartCoroutine(HandleClick()); });
    }

    IEnumerator HandleClick()
    {
        isProcessing = true;
        UIManager.Instance.ShowScreen(gameObject.name);  // Chuyển màn hình

        // Ẩn icon, hiện text
        yield return Fade(iconImage, 1, 0, fadeDuration);
        yield return Fade(nameButton, 0, 1, fadeDuration);

        // Đợi trước khi quay lại trạng thái ban đầu
        yield return new WaitForSeconds(delayBeforeFadeBack);

        // Quay lại trạng thái ban đầu
        yield return Fade(nameButton, 1, 0, fadeDuration);
        yield return Fade(iconImage, 0, 1, fadeDuration);

        isProcessing = false;
    }

    IEnumerator Fade(Graphic uiElement, float from, float to, float duration)
    {
        float t = 0;
        Color color = uiElement.color;
        while (t < duration)
        {
            t += Time.deltaTime;
            uiElement.color = new Color(color.r, color.g, color.b, Mathf.Lerp(from, to, t / duration));
            yield return null;
        }
        uiElement.color = new Color(color.r, color.g, color.b, to);
    }

    void SetAlpha(Graphic uiElement, float alpha)
    {
        Color color = uiElement.color;
        uiElement.color = new Color(color.r, color.g, color.b, alpha);
    }
}
