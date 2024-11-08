using UnityEngine;
using UnityEngine.EventSystems;

public class BowlManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isDragging = false;
    private Vector2 originalPosition;
    private bool hasTriggeredEvent = false; // Cờ kiểm soát để tránh kích hoạt lại sự kiện

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;

        // Đăng ký sự kiện viewResult thay đổi
        GameSBEventManager.Instance.OnViewResultChanged += HandleViewResultChanged;
    }

    private void HandleViewResultChanged(bool newViewResult)
    {
        // Trở về vị trí ban đầu nếu viewResult là false
        if (!newViewResult)
        {
            rectTransform.anchoredPosition = originalPosition;
            hasTriggeredEvent = false; // Reset cờ khi trạng thái thay đổi
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanDrag())
        {
            isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && CanDrag() && SichBoManager.Instance.ViewResult)
        {
            if (canvas != null)
            {
                UISBManager.Instance.ChangeAnimationBowl(false, "Run", "Idle", false);
                UISBManager.Instance.ActiveOverButton(false);
                UISBManager.Instance.ActiveUnderButton(false);
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

                // Tính khoảng cách Euclid từ vị trí ban đầu
                float distance = Vector2.Distance(originalPosition, rectTransform.anchoredPosition);

                // Kiểm tra nếu khoảng cách nằm trong khoảng 640 - 650px và kích hoạt sự kiện chỉ một lần
                if (distance >= 640f && !hasTriggeredEvent)
                {
                    GameSBEventManager.Instance.TriggerDragDistanceReached();
                    hasTriggeredEvent = true; // Đặt cờ để tránh kích hoạt lại
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private bool CanDrag()
    {
        return true;
    }

    private void OnDestroy()
    {
        if (GameSBEventManager.Instance != null)
        {
            GameSBEventManager.Instance.OnViewResultChanged -= HandleViewResultChanged;
        }
    }
}
