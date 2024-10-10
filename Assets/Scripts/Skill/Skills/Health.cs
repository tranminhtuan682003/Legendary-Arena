using UnityEngine;
using UnityEngine.EventSystems;
public class Health : SkillBase
{
    protected override void Start()
    {
        base.Start();
        cooldownTime = 30f;
        radius = 115f;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
}
