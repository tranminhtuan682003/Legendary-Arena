using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AttackButtonAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ButtonPlayKnightManager buttonKnightManager;
    private float cooldown;
    private TypeSkill typeSkill;

    protected virtual void Awake() { }

    protected void InitLize(float cooldown, TypeSkill typeSkill)
    {
        this.cooldown = cooldown;
        this.typeSkill = typeSkill;
    }

    public void SetButtonKnightManager(ButtonPlayKnightManager manager)
    {
        buttonKnightManager = manager;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonKnightManager != null)
        {
            buttonKnightManager.OnButtonAttackPressed(typeSkill);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonKnightManager != null)
        {
            buttonKnightManager.OnButtonAttackReleased();
        }
    }

}


