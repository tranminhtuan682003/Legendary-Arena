using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public abstract class SkillBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("References")]
    private RectTransform moveableRect;
    private RectTransform background;
    private RectTransform button;
    private TextMeshProUGUI nameButton;
    private TextMeshProUGUI coolDown;

    [Header("Settings")]
    [SerializeField]
    protected Vector2 currentPointerPosition;
    protected float radius;
    protected bool isDragging = false;
    protected Vector2 centerPosition;
    protected HeroBase hero;

    protected bool isCooldownActive = false;
    protected float cooldownTime;

    protected virtual void Start()
    {
        hero = FindObjectOfType<HeroBase>(); // Tìm đối tượng HeroBase trong scene
        button = GetComponent<RectTransform>(); // Lấy RectTransform của button

        // Tìm các đối tượng con
        moveableRect = button.Find("MoveableRect").GetComponent<RectTransform>();
        background = button.Find("Background").GetComponent<RectTransform>();
        nameButton = button.Find("Name").GetComponent<TextMeshProUGUI>();
        coolDown = button.Find("CoolDown").GetComponent<TextMeshProUGUI>();

        // Vô hiệu hóa các đối tượng ngay từ đầu
        ButtonDeactive();
        nameButton.gameObject.SetActive(true); // Hiển thị tên nút ban đầu
        coolDown.gameObject.SetActive(true);
    }

    protected virtual void ButtonDeactive()
    {
        moveableRect.gameObject.SetActive(false); // Tắt di chuyển
        background.gameObject.SetActive(false);   // Tắt nền
    }

    protected virtual void ButtonActive()
    {
        moveableRect.gameObject.SetActive(true);  // Kích hoạt di chuyển
        background.gameObject.SetActive(true);    // Kích hoạt nền
    }

    // Bắt đầu quá trình cooldown sau khi thả nút
    protected virtual void StartCooldown()
    {
        if (!isCooldownActive) // Kiểm tra xem cooldown đã kích hoạt chưa
        {
            StartCoroutine(CooldownRoutine()); // Bắt đầu Coroutine
        }
    }

    // Coroutine để xử lý đếm ngược cooldown
    protected IEnumerator CooldownRoutine()
    {
        isCooldownActive = true; // Đánh dấu cooldown đang hoạt động
        float remainingTime = cooldownTime;

        // Vòng lặp để thực hiện đếm ngược
        while (remainingTime > 0)
        {
            if (remainingTime > 1)
            {
                // Hiển thị thời gian nguyên (số giây còn lại)
                coolDown.text = Mathf.Floor(remainingTime).ToString();
            }
            else
            {
                // Hiển thị thời gian với phần thập phân khi còn dưới 1 giây
                coolDown.text = remainingTime.ToString("F2");
            }

            // Tạm dừng 0.1 giây trước khi cập nhật lại thời gian
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        // Khi cooldown kết thúc
        coolDown.text = "";
        isCooldownActive = false;
    }

    // Xử lý khi nhấn nút
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        ButtonActive(); // Kích hoạt các thành phần khi nhấn
        centerPosition = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, button.position);
        isDragging = true;
    }

    // Xử lý khi nhả nút (bắt đầu cooldown tại đây)
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ButtonDeactive(); // Vô hiệu hóa các thành phần
        isDragging = false;

        // Bắt đầu cooldown khi thả nút
        StartCooldown();
    }

    // Xử lý khi kéo (drag)
    public virtual void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        currentPointerPosition = eventData.position;
        Vector2 direction = currentPointerPosition - centerPosition;
        Vector2 constrainedPosition = direction.normalized * radius;
        Vector2 finalPosition = centerPosition + constrainedPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            moveableRect.parent as RectTransform, finalPosition, eventData.pressEventCamera, out Vector2 localPosition
        );

        moveableRect.anchoredPosition = localPosition;
    }
}
