using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BannerAutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    private int totalBanners = 6;
    private float scrollSpeed = 1f; // Tốc độ mượt hơn
    private float waitTime = 3.0f;

    private int currentBanner = 0;
    private float smoothTime = 0.3f; // Thời gian để cuộn mượt mà hơn
    private float velocity = 0.0f; // Tốc độ di chuyển mượt

    private Coroutine scrollCoroutine; // Lưu coroutine để có thể dừng khi reset

    void Start()
    {
        scrollCoroutine = StartCoroutine(AutoScroll());
    }

    IEnumerator AutoScroll()
    {
        while (true)
        {
            float targetPos = (float)currentBanner / (totalBanners - 1);

            while (Mathf.Abs(scrollRect.horizontalNormalizedPosition - targetPos) > 0.001f)
            {
                // Sử dụng SmoothDamp để chuyển động mượt mà
                scrollRect.horizontalNormalizedPosition = Mathf.SmoothDamp(
                    scrollRect.horizontalNormalizedPosition,
                    targetPos,
                    ref velocity,
                    smoothTime,
                    scrollSpeed
                );
                yield return null;
            }

            scrollRect.horizontalNormalizedPosition = targetPos;

            yield return new WaitForSeconds(waitTime);

            currentBanner++;

            if (currentBanner >= totalBanners)
            {
                currentBanner = 0;
            }
        }
    }

    // Hàm reset để đưa về banner đầu tiên
    public void ResetScroll()
    {
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine); // Dừng coroutine hiện tại
        }

        currentBanner = 0; // Đặt lại banner hiện tại là banner đầu tiên
        velocity = 0.0f; // Reset tốc độ

        // Đặt vị trí cuộn về đầu tiên ngay lập tức
        scrollRect.horizontalNormalizedPosition = 0.0f;

        // Bắt đầu lại việc tự động cuộn
        scrollCoroutine = StartCoroutine(AutoScroll());
    }
}