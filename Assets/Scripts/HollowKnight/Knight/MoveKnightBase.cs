using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MoveKnightBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ButtonControlManager moveKnightManager;
    public TypeMove typeMove;

    protected virtual void Awake() { }

    protected void InitLize(TypeMove typeMove)
    {
        this.typeMove = typeMove;
    }

    public void SetButtonKnightManager(ButtonControlManager manager)
    {
        moveKnightManager = manager;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (moveKnightManager != null)
        {
            moveKnightManager.OnButtonMovePressed(typeMove);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (moveKnightManager != null)
        {
            moveKnightManager.OnButtonMoveReleased();
        }
    }
}
