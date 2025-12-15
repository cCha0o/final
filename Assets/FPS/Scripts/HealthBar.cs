using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthslider;

    private void Awake()
    {
        _healthslider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        _healthslider.maxValue = maxHealth;
        _healthslider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        _healthslider.value = health;
    }
}
