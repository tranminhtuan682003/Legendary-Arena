using UnityEngine;

public class HealthBarTeamBlueController : HealthBarBase
{
    private ITeamMember componentParent;

    public override void SetParrent(object target)
    {
        if (target is ITeamMember)
        {
            componentParent = (ITeamMember)target;
            UpdateHealthBar();
        }
        else
        {
            Debug.LogError("Invalid target assigned to HealthBarTeamBlueController. Expected ITeamBlue.");
        }
    }

    private void UpdateHealthBar()
    {
        if (componentParent != null)
        {
            UpdateHealth(componentParent.GetCurrentHealth());
        }
        else
        {
            Debug.LogWarning("No valid ITeamBlue assigned.");
        }
    }
}
