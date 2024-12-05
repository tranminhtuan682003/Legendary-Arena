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
        KnightEventManager.OnHealthBarUpdated += HandleKnightUpdateHealth;
        healthSlider.value = 1f;
    }

    private void SetHealth(float currentHealth)
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

    private void HandleKnightUpdateHealth(KnightController knightController)
    {
        this.knightController = knightController;
        SetHealth(knightController.GetCurrentHealth());
    }

    private void OnDisable()
    {
        KnightEventManager.OnHealthBarUpdated -= HandleKnightUpdateHealth;
    }
}
