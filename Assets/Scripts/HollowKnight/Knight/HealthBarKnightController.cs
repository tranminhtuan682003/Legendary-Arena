using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarKnightController : MonoBehaviour
{
    private KnightController knightController;
    private Slider healthSlider;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        KnightEventManager.OnHealthBarSet += HandleKnightOnEnable;
        KnightEventManager.OnHealthBarUpdated += HandleKnightUpdateHealth;
    }

    private void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateColor(maxHealth / maxHealth);
    }

    private void SetHealth(int currentHealth)
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

    private void HandleKnightOnEnable(KnightController knightController)
    {
        this.knightController = knightController;
        SetMaxHealth(knightController.GetMaxHealth());
    }

    private void HandleKnightUpdateHealth(KnightController knightController)
    {
        this.knightController = knightController;
        SetHealth(knightController.GetHealth());
    }

    private void OnDisable()
    {
        KnightEventManager.OnHealthBarSet -= HandleKnightOnEnable;
        KnightEventManager.OnHealthBarUpdated -= HandleKnightUpdateHealth;
    }
}
