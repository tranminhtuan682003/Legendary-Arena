using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarKnightController : MonoBehaviour
{
    private Slider healthSlider;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateColor(maxHealth / maxHealth);
    }

    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    private void UpdateColor(int normalizedValue)
    {
        if (fillImage != null && healthGradient != null)
        {
            fillImage.color = healthGradient.Evaluate(normalizedValue);
        }
    }
}
