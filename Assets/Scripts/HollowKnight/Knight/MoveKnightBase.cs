using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MoveKnightBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TypeMove typeMove;

    protected virtual void Awake() { }

    protected void InitLize(TypeMove typeMove)
    {
        this.typeMove = typeMove;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        KnightEventManager.InvokeUpdateMove(typeMove);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        KnightEventManager.InvokeUpdateMove(TypeMove.None);
    }
}
