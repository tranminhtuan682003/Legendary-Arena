using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
public class Skill2 : SkillBase
{
    protected override void Start()
    {
        base.Start();
        cooldownTime = 10f;
        radius = 155f;
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
