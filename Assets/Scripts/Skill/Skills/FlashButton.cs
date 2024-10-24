using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlashButton : SkillBase
{
    private GameObject supplymentary;
    protected override void Start()
    {
        base.Start();
        radius = 115f;
        cooldownTime = 100f;
        UIManager.Instance.InitSupplymentary(hero.transform, "Explosive", OnSupplymentaryInitialized);
    }

    private void OnSupplymentaryInitialized()
    {
        supplymentary = UIManager.Instance.GetSup("Explosive");
    }

    protected override void SetLabelVisibility(bool isVisible)
    {
        base.SetLabelVisibility(isVisible);
        nameLabel.text = "Flash";
    }

    protected override void FuncitionInOnPointerDown()
    {
        base.FuncitionInOnPointerDown();
        Debug.Log(supplymentary);
        supplymentary.SetActive(true);
    }
    protected override void FuncitionInOnPointerUp()
    {
        base.FuncitionInOnPointerUp();
        supplymentary.SetActive(false);
        TelePlayer();
    }
    protected override void FuncitionInOnPointerUpCancel()
    {
        base.FuncitionInOnPointerUpCancel();
        supplymentary.SetActive(false);
    }

    protected override void FuncitionInOnDrag()
    {
        base.FuncitionInOnDrag();
        SetRotationForSupplymentary(currentPointerPosition);

    }

    private void SetRotationForSupplymentary(Vector2 currentPointerPosition)
    {
        Vector2 direction = currentPointerPosition - centerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1 + 90f;
        supplymentary.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void TelePlayer()
    {
        hero.transform.position = supplymentary.transform.position + supplymentary.transform.forward * 2f;
        hero.transform.rotation = supplymentary.transform.rotation;
    }
}