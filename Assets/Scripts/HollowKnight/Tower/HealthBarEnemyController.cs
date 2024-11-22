using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemyController : MonoBehaviour
{
    private IEnemy enemy;
    private Slider healthSlider;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetEnemy(IEnemy assignedEnemy)
    {
        enemy = assignedEnemy;
        UpdateHealthBar();
    }

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

    private void UpdateHealthBar()
    {
        if (enemy != null)
        {
            UpdateHealth(enemy.GetCurrentHealth());
        }
    }
}
