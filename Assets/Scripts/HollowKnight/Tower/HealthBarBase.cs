using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBarBase : MonoBehaviour
{
    private Slider healthSlider;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillImage;

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
        UpdateColor(currentHealth);
    }

    private void UpdateColor(float currentHealth)
    {
        if (fillImage != null && healthGradient != null)
        {
            fillImage.color = healthGradient.Evaluate(currentHealth);
        }
    }
}
