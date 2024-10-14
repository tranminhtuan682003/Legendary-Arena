using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BannerAutoScroll : MonoBehaviour
{
    private ScrollRect scrollRect;
    private int totalBanners = 6, currentBanner = 0, direction = 1;
    private float waitTime = 2f, smoothTime = 0.3f, velocity = 0f;

    void Start() => StartCoroutine(AutoScroll());

    IEnumerator AutoScroll()
    {
        scrollRect = GetComponent<ScrollRect>();
        while (true)
        {
            float targetPos = (float)currentBanner / (totalBanners - 1);
            while (Mathf.Abs(scrollRect.horizontalNormalizedPosition - targetPos) > 0.001f)
            {
                scrollRect.horizontalNormalizedPosition = Mathf.SmoothDamp(scrollRect.horizontalNormalizedPosition, targetPos, ref velocity, smoothTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);

            currentBanner += direction;
            if (currentBanner == totalBanners - 1 || currentBanner == 0) direction *= -1;
        }
    }
}
