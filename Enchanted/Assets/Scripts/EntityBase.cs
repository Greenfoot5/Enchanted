using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    public float Health { get { return _health; } }

    private float _health;

    public virtual void Heal(float amount)
    {
        _health += amount;
    }

    public virtual void Damage(float amount, EntityBase caster)
    {
        _health -= amount;
        if (caster is Player)
            caster.DamageFeedback(amount, transform.position);
        if (this is Player)
            DamageFeedback(amount);
    }

    public virtual void DamageFeedback(float amount, Vector3? position = null)
    {
        AssetManager.Instance.InstantiateDamageNumber(
            amount,
            position.GetValueOrDefault(transform.position)
        );
    }
}
