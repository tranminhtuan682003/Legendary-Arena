using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButtonAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ButtonPlayKnightManager moveKnightManager;
    public string action; // Tên action (Right, Left, Up, Down)

    public void SetButtonKnightManager(ButtonPlayKnightManager manager)
    {
        moveKnightManager = manager;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (moveKnightManager != null)
        {
            moveKnightManager.OnButtonMovePressed(action);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (moveKnightManager != null)
        {
            moveKnightManager.OnButtonMoveReleased(action);
        }
    }
}
