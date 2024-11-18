using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayKnightManager
{
    private readonly Dictionary<string, Vector2> moveDirections = new Dictionary<string, Vector2>
    {
        { "Right", Vector2.right },
        { "Left", Vector2.left },
        { "Up", Vector2.up },
        { "Down", Vector2.down }
    };

    //movement
    public Vector2 CurrentMoveDirection { get; private set; } = Vector2.zero;

    public void OnButtonMovePressed(string action)
    {
        if (moveDirections.TryGetValue(action, out var direction))
        {
            CurrentMoveDirection = direction;
        }
    }

    public void OnButtonMoveReleased(string action)
    {
        if (action == "Up" || action == "Down" || action == "Left" || action == "Right")
        {
            CurrentMoveDirection = Vector2.zero;
        }
    }

    //attack
    public TypeSkill typeSkill;

    public void OnButtonAttackPressed(TypeSkill typeSkill)
    {
        this.typeSkill = typeSkill;
    }

    public void OnButtonAttackReleased()
    {
        typeSkill = TypeSkill.None;
    }
}
