using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    private float _health;

    public float Health => _health;

    protected SpellEffectManager _spellEffectManager;

    public virtual void Start()
    {
        _spellEffectManager = gameObject.AddComponent<SpellEffectManager>();
    }

    public virtual void AddEffect(SpellEffectBase effect) => _spellEffectManager.AddEffect(effect);

    public virtual void Heal(float amount)
    {
        _health += amount;
    }

    public virtual void Damage(float amount, EntityBase caster)
    {
        _health -= amount;
        if (caster is PlayerBase)
            caster.DamageFeedback(amount, transform.position);
        if (this is PlayerBase)
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
