using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public UnitHealth Health;

    public HealthBar healthBar;

    public int maxHealth = 200;

    void Awake()
    {
        Health = new UnitHealth(maxHealth, maxHealth);

        Health.OnHealthChanged += UpdateHealthUI;

        UpdateHealthUI(Health.Health, Health.MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        Health.DmgUnit(damage);

        if (Health.Health <= 0)
            Die();
    }

    void UpdateHealthUI(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(max);
            healthBar.SetHealth(current);
        }
    }

    void Die()
    {
    Destroy(gameObject);

    EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
    if (enemies.Length <= 1)
    {
        GameStateManager.Instance.WinGame();
    }
    }


    private void OnDestroy()
    {
        if (Health != null)
            Health.OnHealthChanged -= UpdateHealthUI;
    }
}
