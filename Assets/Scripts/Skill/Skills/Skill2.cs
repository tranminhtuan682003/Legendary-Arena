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

    protected override void SetLabelVisibility(bool isVisible)
    {
        base.SetLabelVisibility(isVisible);
        nameLabel.text = "Slow";
    }
}
