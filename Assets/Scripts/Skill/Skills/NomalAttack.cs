using UnityEngine.EventSystems;

public class NomalAttack : AttackBase, IPointerDownHandler
{
    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
}
