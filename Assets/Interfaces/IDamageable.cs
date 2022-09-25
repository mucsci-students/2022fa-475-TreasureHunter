using System;
using UnityEngine;

public interface IDamageable
{

    public float GetHealth();

    public float GetHealthPercentage();

    public void ApplyDamage(GameObject DamageCauser, float damage);

    public event EventHandler OnDestroyed;
    public event EventHandler<GameObject> OnAnyDamage;

}
