using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SkillBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    #region Variables

    // UI Elements
    protected RectTransform moveableRect;
    protected RectTransform background;
    protected RectTransform buttonRect;
    protected TextMeshProUGUI nameLabel;
    protected TextMeshProUGUI cooldownLabel;
    protected Button actionButton;
    protected RectTransform cencelSkill;

    // Supplementary Table Addressables
    protected string supplementaryTableAddress = "Assets/Scripts/Skill/ScriptTableObject/SupplementaryTable.asset";
    protected SupplementaryTable supplementaryTable;

    // Supplementary Item
    protected GameObject supplymentary;
    protected string nameSupplymentary;

    // Skill and Dragging Data
    [SerializeField] protected Vector2 currentPointerPosition;
    protected float radius;
    protected bool isDragging;
    protected Vector2 centerPosition;
    protected HeroBase hero;

    // Cooldown Data
    protected bool isCooldownActive;
    protected float cooldownTime;

    #endregion

    #region Initialization

    protected virtual void Start()
    {
        InitializeComponents();
        DeactivateButton();
        SetLabelVisibility(true);
        Addressables.LoadAssetAsync<SupplementaryTable>(supplementaryTableAddress).Completed += OnSupplementaryTableLoaded;
    }

    protected virtual void InitializeComponents()
    {
        hero = FindObjectOfType<HeroBase>();
        buttonRect = GetComponent<RectTransform>();
        actionButton = GetComponent<Button>();

        cencelSkill = GameObject.Find("CencelSkill")?.GetComponent<RectTransform>();
        moveableRect = buttonRect.Find("MoveableRect")?.GetComponent<RectTransform>();
        background = buttonRect.Find("Background")?.GetComponent<RectTransform>();
        nameLabel = buttonRect.Find("Name")?.GetComponent<TextMeshProUGUI>();
        cooldownLabel = buttonRect.Find("CoolDown")?.GetComponent<TextMeshProUGUI>();
    }

    #endregion

    #region Supplementary Table Handling

    protected virtual void OnSupplementaryTableLoaded(AsyncOperationHandle<SupplementaryTable> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            supplementaryTable = handle.Result;
            InitSupplementary();
        }
    }

    protected void InitSupplementary()
    {
        foreach (var item in supplementaryTable.supplementarys)
        {
            if (item.name == nameSupplymentary)
            {
                supplymentary = Instantiate(item, hero.transform);
                supplymentary.SetActive(false);
                break;
            }
        }
    }

    #endregion

    #region Button Activation/Deactivation

    protected virtual void DeactivateButton()
    {
        SetActiveState(false);
    }

    protected virtual void ActivateButton()
    {
        SetActiveState(true);
    }

    protected virtual void SetActiveState(bool isActive)
    {
        if (moveableRect != null) moveableRect.gameObject.SetActive(isActive);
        if (background != null) background.gameObject.SetActive(isActive);
    }

    protected virtual void SetLabelVisibility(bool isVisible)
    {
        if (nameLabel != null) nameLabel.gameObject.SetActive(isVisible);
        if (cooldownLabel != null) cooldownLabel.gameObject.SetActive(isVisible);
    }

    #endregion

    #region Cooldown Management

    protected virtual void StartCooldown()
    {
        if (!isCooldownActive)
        {
            StartCoroutine(CooldownRoutine());
        }
    }

    protected IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        actionButton.interactable = false;

        float remainingTime = cooldownTime;
        while (remainingTime > 0)
        {
            UpdateCooldownLabel(remainingTime);
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        ResetCooldown();
    }

    private void UpdateCooldownLabel(float remainingTime)
    {
        if (cooldownLabel != null)
        {
            cooldownLabel.text = remainingTime > 1 ? Mathf.Floor(remainingTime).ToString() : remainingTime.ToString("F2");
        }
    }

    private void ResetCooldown()
    {
        if (cooldownLabel != null) cooldownLabel.text = string.Empty;
        isCooldownActive = false;
        actionButton.interactable = true;
    }

    #endregion

    #region Drag Handling

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (isCooldownActive) return;
        FuncitionInOnPointerDown();
        ActivateButton();
        centerPosition = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, buttonRect.position);
        isDragging = true;
        if (cencelSkill != null) cencelSkill.gameObject.SetActive(true);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (isCooldownActive) return;

        if (IsPointerOverRectTransform(cencelSkill, eventData))
        {
            CancelDrag();
            if (cencelSkill != null) cencelSkill.gameObject.SetActive(false);
            FuncitionInOnPointerUpCencel();
            return;
        }
        cencelSkill.gameObject.SetActive(false);
        FuncitionInOnPointerUp();
        DeactivateButton();
        isDragging = false;
        StartCooldown();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (isCooldownActive || !isDragging) return;
        FuncitionInOnDrag();
        UpdateDragPosition(eventData);
    }

    private void UpdateDragPosition(PointerEventData eventData)
    {
        currentPointerPosition = eventData.position;
        Vector2 direction = currentPointerPosition - centerPosition;
        Vector2 constrainedPosition = direction.normalized * radius;
        Vector2 finalPosition = centerPosition + constrainedPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            moveableRect.parent as RectTransform, finalPosition, eventData.pressEventCamera, out Vector2 localPosition
        );

        moveableRect.anchoredPosition = localPosition;
    }

    protected void CancelDrag()
    {
        isDragging = false;
        DeactivateButton();
        Debug.Log("Kéo đã bị hủy!");
    }

    protected bool IsPointerOverRectTransform(RectTransform rectTransform, PointerEventData eventData)
    {
        if (rectTransform == null) return false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition);

        return rectTransform.rect.Contains(localPointerPosition);
    }

    #endregion

    #region Virtual Methods (For Override in Subclasses)

    protected virtual void FuncitionInOnPointerDown() { }
    protected virtual void FuncitionInOnPointerUp() { }
    protected virtual void FuncitionInOnPointerUpCencel() { }
    protected virtual void FuncitionInOnDrag() { }

    #endregion
}
