using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
public class Skill3TelAnas : SkillBase
{
    protected override void Start()
    {
        base.Start();
        cooldownTime = 30f;
        radius = 155f;
        cancelSkill.gameObject.SetActive(false);
    }

    protected override void SetLabelVisibility(bool isVisible)
    {
        base.SetLabelVisibility(isVisible);
        nameLabel.text = "Sturned";
    }
}
