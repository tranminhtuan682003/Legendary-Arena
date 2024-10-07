using UnityEngine;
using UnityEngine.EventSystems;
public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("References")]
    public RectTransform moveableRect;
    public RectTransform background;
    public RectTransform button;

    [Header("Settings")]
    [SerializeField]
    private float radius = 115f;

    private bool isDragging = false;
    private Vector2 centerPosition;
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        moveableRect.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        playerController.supplymentary.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveableRect.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        playerController.supplymentary.SetActive(true);
        centerPosition = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, button.position);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TelePlayer();
        moveableRect.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        playerController.supplymentary.SetActive(false);
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 currentPointerPosition = eventData.position;
        Vector2 direction = currentPointerPosition - centerPosition;
        Vector2 constrainedPosition = direction.normalized * radius;
        Vector2 finalPosition = centerPosition + constrainedPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            moveableRect.parent as RectTransform, finalPosition, eventData.pressEventCamera, out Vector2 localPosition
        );

        moveableRect.anchoredPosition = localPosition;
        SetRotationForSupplymentary(currentPointerPosition);
    }

    private void SetRotationForSupplymentary(Vector2 currentPointerPosition)
    {
        // Tính toán vector hướng từ trung tâm đến vị trí ngón tay
        Vector2 direction = currentPointerPosition - centerPosition;

        // Tính toán góc giữa direction theo chiều Y và X và thêm 90 độ để điều chỉnh sự lệch
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1 + 90f;

        // Xoay đối tượng supplymentary theo trục Y với góc tính được
        playerController.supplymentary.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void TelePlayer()
    {
        playerController.transform.position = playerController.supplymentary.transform.position + playerController.supplymentary.transform.forward * 2f;
        playerController.transform.rotation = playerController.supplymentary.transform.rotation;

    }




}
