using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class SkillKnightBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ButtonControlManager buttonKnightManager;
    private float cooldown;
    private TypeSkill typeSkill;
    private bool isCooldownActive;

    protected virtual void Awake() { }

    protected void InitLize(float cooldown, TypeSkill typeSkill)
    {
        this.cooldown = cooldown;
        this.typeSkill = typeSkill;
    }

    public void SetButtonKnightManager(ButtonControlManager manager)
    {
        buttonKnightManager = manager;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonKnightManager != null && !isCooldownActive)
        {
            buttonKnightManager.OnButtonAttackPressed(typeSkill, cooldown);
            StartCoroutine(CooldownRoutine());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonKnightManager != null)
        {
            buttonKnightManager.OnButtonAttackReleased();
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(cooldown); // Chờ thời gian cooldown
        isCooldownActive = false;
    }
}
