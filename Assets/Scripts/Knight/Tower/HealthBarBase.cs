using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBarBase : MonoBehaviour
{
    private Slider healthSlider;

    protected virtual void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    public abstract void SetParrent(object target);

    public void UpdateHealth(float currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
}
