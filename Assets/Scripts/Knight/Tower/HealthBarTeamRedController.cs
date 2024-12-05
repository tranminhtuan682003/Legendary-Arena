using UnityEngine;

public class HealthBarTeamRedController : HealthBarBase
{
    private ITeamMember componentParrent;

    public override void SetParrent(object target)
    {
        if (target is ITeamMember)
        {
            componentParrent = (ITeamMember)target;
            UpdateHealthBar();
        }
        else
        {
            Debug.LogError("Invalid target assigned to HealthBarTeamRedController. Expected ITeamRed.");
        }
    }

    private void UpdateHealthBar()
    {
        if (componentParrent != null)
        {
            UpdateHealth(componentParrent.GetCurrentHealth());
        }
        else
        {
            Debug.LogWarning("No valid ITeamRed assigned.");
        }
    }
}
