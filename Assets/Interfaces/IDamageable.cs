using System;
using UnityEngine;

public interface IDamageable
{

    public float GetHealth();

    public float GetHealthPercentage();

    public void ApplyDamage(float damage);

    public event EventHandler OnDestroyed;

}
