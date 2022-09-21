using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{

    public float BaseHealth = 100;
    public float MaxHealth = 100;
    public event EventHandler OnDestroyed;

    private float _currentHealth;

    void Start() => _currentHealth = BaseHealth;

    public float GetHealth()
        => BaseHealth;

    public float GetHealthPercentage()
        => _currentHealth / MaxHealth;  

    public void ApplyDamage(float damage)
    {

        _currentHealth = Math.Clamp((_currentHealth - damage), 0, MaxHealth);
        if (_currentHealth <= 0) { OnDestroyed?.Invoke(this, EventArgs.Empty); }

    }

}
