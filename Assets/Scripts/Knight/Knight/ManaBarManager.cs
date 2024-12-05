using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarManager : MonoBehaviour
{
    private KnightController knightController;
    private Slider manaSlider;
    [SerializeField] private Gradient manaGradient;
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        manaSlider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        KnightEventManager.OnManaUpdated += HandleKnightUpdateMana;
        manaSlider.value = 1f;
    }

    private void SetHealth(float currentHealth)
    {
        manaSlider.value = currentHealth;
    }

    private void UpdateColor(int normalizedValue)
    {
        if (fillImage != null && manaGradient != null)
        {
            fillImage.color = manaGradient.Evaluate(normalizedValue);
        }
    }

    private void HandleKnightUpdateMana(KnightController knightController)
    {
        this.knightController = knightController;
        SetHealth(knightController.GetMana());
    }

    private void OnDisable()
    {
        KnightEventManager.OnManaUpdated -= HandleKnightUpdateMana;
    }
}
