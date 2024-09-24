using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public RectTransform Body;
    public Button menu;
    public float moveDistance = 100f; // Khoảng cách dịch chuyển theo trục X
    public float moveDuration = 0.5f; // Thời gian dịch chuyển (giây)

    private bool isMenuOpen = false;
    private Vector2 originalPosition;

    void Start()
    {
        menu.onClick.AddListener(() => OpenMenu());
        originalPosition = Body.anchoredPosition; // Lưu lại vị trí ban đầu
    }

    private void OpenMenu()
    {
        isMenuOpen = !isMenuOpen;
        float targetX = isMenuOpen ? originalPosition.x + moveDistance : originalPosition.x;

        // Bắt đầu Coroutine để dịch chuyển từ từ
        StartCoroutine(MoveBody(targetX));
    }

    private IEnumerator MoveBody(float targetX)
    {
        float elapsedTime = 0f;
        float startingX = Body.anchoredPosition.x;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float newX = Mathf.Lerp(startingX, targetX, elapsedTime / moveDuration);
            Body.anchoredPosition = new Vector2(newX, Body.anchoredPosition.y);
            yield return null;
        }

        // Đảm bảo vị trí cuối cùng chính xác
        Body.anchoredPosition = new Vector2(targetX, Body.anchoredPosition.y);
    }
}
