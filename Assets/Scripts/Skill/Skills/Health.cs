using System.Reflection.Emit;
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

    protected override void SetLabelVisibility(bool isVisible)
    {
        base.SetLabelVisibility(isVisible);
        nameLabel.text = "Health";
    }
}
