using System;
    
public class UnitHealth
{
    // Fields
    int _currentHealth;
    int _currentMaxHealth;

    // Event to notify UI when health changes
    public event Action<int, int> OnHealthChanged;

    // Properties
    public int Health => _currentHealth;
    public int MaxHealth => _currentMaxHealth;

    // Constructor
    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;

        // 
        OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth);
    }

    // Methods
    public void DmgUnit(int dmgAmount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= dmgAmount;
        if (_currentHealth < 0)
            _currentHealth = 0;

        OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth);
    }

    public void HealUnit(int healAmount)
    {
        if (_currentHealth >= _currentMaxHealth) return;

        _currentHealth += healAmount;
        if (_currentHealth > _currentMaxHealth)
            _currentHealth = _currentMaxHealth;

        OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth);
    }
}
