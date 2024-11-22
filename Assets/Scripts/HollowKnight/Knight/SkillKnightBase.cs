using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public abstract class SkillKnightBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float executionTime; // Thời gian thực thi kỹ năng
    private float cooldown; // Thời gian hồi chiêu
    private TypeSkill typeSkill; // Loại kỹ năng
    private string pathImage; // Đường dẫn ảnh
    private Button button; // Nút kỹ năng
    private Image childImage; // Hình ảnh biểu tượng kỹ năng
    private TextMeshProUGUI cooldownText; // Text hiển thị thời gian hồi chiêu

    private bool isCooldown = false; // Trạng thái cooldown

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSkillButtonClicked); // Gắn sự kiện onClick

        // Tìm Image con của Button
        childImage = transform.Find("Background/Icon").GetComponent<Image>();

        // Tìm TextCooldown
        cooldownText = transform.Find("Cooldown").GetComponent<TextMeshProUGUI>();

        if (cooldownText != null)
        {
            cooldownText.gameObject.SetActive(false); // Ẩn TextCooldown ban đầu
        }
    }

    protected void InitLize(float cooldown, TypeSkill typeSkill, float executionTime, string pathImage)
    {
        this.cooldown = cooldown;
        this.typeSkill = typeSkill;
        this.executionTime = executionTime; // Gán thời gian thực thi kỹ năng
        this.pathImage = pathImage;

        Sprite skillIcon = Resources.Load<Sprite>(pathImage);
        if (skillIcon != null && childImage != null)
        {
            childImage.sprite = skillIcon;
        }

        if (cooldownText != null && cooldown <= 1f)
        {
            cooldownText.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Làm tối màu icon khi nhấn giữ
        if (childImage != null && !isCooldown)
        {
            childImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Tối màu
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Trả lại màu cũ khi thả nút
        if (childImage != null && !isCooldown)
        {
            childImage.color = Color.white; // Màu ban đầu
        }
    }

    private void OnSkillButtonClicked()
    {
        if (isCooldown) return; // Không cho phép kích hoạt khi đang cooldown

        // Xử lý logic kích hoạt kỹ năng
        KnightEventManager.InvokeUpdateSkill(typeSkill, cooldown, executionTime);

        // Bắt đầu cooldown
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        isCooldown = true;
        button.interactable = false;
        float remainingTime = cooldown;

        // Làm tối icon khi bắt đầu cooldown
        if (childImage != null)
        {
            childImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Tối màu
        }

        if (cooldownText != null && cooldown > 1f)
        {
            cooldownText.gameObject.SetActive(true);
        }

        while (remainingTime > 0)
        {
            if (cooldownText != null && cooldown > 1f)
            {
                if (remainingTime < 1f)
                {
                    cooldownText.text = remainingTime.ToString("F2"); // Hiển thị phần thập phân
                }
                else
                {
                    cooldownText.text = Mathf.Ceil(remainingTime).ToString(); // Hiển thị số nguyên
                }
            }

            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        // Kết thúc cooldown
        isCooldown = false;
        button.interactable = true;

        // Trả lại màu icon khi cooldown kết thúc
        if (childImage != null)
        {
            childImage.color = Color.white;
        }

        if (cooldownText != null && cooldown > 1f)
        {
            cooldownText.gameObject.SetActive(false);
        }
    }
}
