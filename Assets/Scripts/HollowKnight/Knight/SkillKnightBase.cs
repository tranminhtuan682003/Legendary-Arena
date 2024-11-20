using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class SkillKnightBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float executionTime;
    private float cooldown;
    private TypeSkill typeSkill;

    protected virtual void Awake() { }

    protected void InitLize(float cooldown, TypeSkill typeSkill, float executionTime)
    {
        this.cooldown = cooldown;
        this.typeSkill = typeSkill;
        this.executionTime = executionTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        KnightEventManager.InvokeUpdateSkill(typeSkill, cooldown, executionTime);
    }
}
