using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    public IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float fromAlpha, float toAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsedTime / duration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = toAlpha;
    }

    public void SetCanvasGroupAlpha(CanvasGroup canvasGroup, float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public IEnumerator FadeAndResetCanvasGroups(
        Dictionary<string, CanvasGroup> textCanvasGroups,
        Dictionary<string, CanvasGroup> buttonCanvasGroups,
        string textName,
        string buttonName,
        float fadeDuration)
    {
        ResetCanvasGroups(textCanvasGroups, buttonCanvasGroups);

        if (textCanvasGroups.TryGetValue(textName, out CanvasGroup textGroup) &&
            buttonCanvasGroups.TryGetValue(buttonName, out CanvasGroup buttonGroup))
        {
            // Hiện Text
            yield return StartCoroutine(FadeCanvasGroup(textGroup, 0f, 1f, fadeDuration));

            // Mờ Button
            yield return StartCoroutine(FadeCanvasGroup(buttonGroup, 1f, 0f, fadeDuration));

            // Đợi thêm 1 giây
            yield return new WaitForSeconds(1.5f);

            // Làm mờ Text và kích hoạt lại Button
            yield return StartCoroutine(FadeCanvasGroup(textGroup, 1f, 0f, fadeDuration));
            yield return StartCoroutine(FadeCanvasGroup(buttonGroup, 0f, 1f, fadeDuration));
        }
    }

    private void ResetCanvasGroups(Dictionary<string, CanvasGroup> textCanvasGroups, Dictionary<string, CanvasGroup> buttonCanvasGroups)
    {
        foreach (var canvasGroup in textCanvasGroups.Values)
        {
            SetCanvasGroupAlpha(canvasGroup, 0f);
        }

        foreach (var canvasGroup in buttonCanvasGroups.Values)
        {
            SetCanvasGroupAlpha(canvasGroup, 1f);
        }
    }
}
