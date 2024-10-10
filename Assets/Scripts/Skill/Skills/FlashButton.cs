using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlashButton : SkillBase
{
    protected override void Start()
    {
        base.Start();
        radius = 115f;
        cooldownTime = 100f;
        hero.heroParameter.supplymentary.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        hero.heroParameter.supplymentary.SetActive(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        TelePlayer();
        hero.heroParameter.supplymentary.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        SetRotationForSupplymentary(currentPointerPosition);
    }

    private void SetRotationForSupplymentary(Vector2 currentPointerPosition)
    {
        Vector2 direction = currentPointerPosition - centerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1 + 90f;
        hero.heroParameter.supplymentary.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void TelePlayer()
    {
        hero.transform.position = hero.heroParameter.supplymentary.transform.position + hero.heroParameter.supplymentary.transform.forward * 2f;
        hero.transform.rotation = hero.heroParameter.supplymentary.transform.rotation;

    }
}
