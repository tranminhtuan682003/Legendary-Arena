using UnityEngine;
using UnityEngine.EventSystems;

public class BowlManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isDragging = false;
    private Vector2 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;

        // Đăng ký sự kiện viewResult thay đổi
        SichBoManager.Instance.OnViewResultChanged += HandleViewResultChanged;
    }

    private void HandleViewResultChanged(bool newViewResult)
    {
        // Trở về vị trí ban đầu nếu viewResult là false
        if (!newViewResult)
        {
            rectTransform.anchoredPosition = originalPosition;
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
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
        if (SichBoManager.Instance != null)
        {
            SichBoManager.Instance.OnViewResultChanged -= HandleViewResultChanged;
        }
    }
}
