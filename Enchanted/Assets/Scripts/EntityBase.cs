using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float _maxHealth;
    private float _health;

    public float MaxHealth => _maxHealth;
    public float Health => _health;

    protected SpellEffectManager _spellEffectManager;

    protected Renderer _modelRenderer;

    public virtual void Start()
    {
        _spellEffectManager = gameObject.AddComponent<SpellEffectManager>();
        _modelRenderer = GetComponentInChildren<Renderer>();
    }

    public virtual void AddEffect(SpellEffectBase effect) => _spellEffectManager.AddEffect(effect);

    //public virtual void AddMaterial(Material material)
    //{
    //    Material[] oldMats = _modelRenderer.materials;
    //    int matCount = oldMats.Length;
    //    Material[] newMats = new Material[matCount + 1];

    //    for (int i = 0; i < matCount - 1; i++)
    //        newMats[i] = oldMats[i];

    //    //newMats[matCount] = material;
    //    _modelRenderer.materials = newMats;
    //}

    //public virtual void RemoveMaterial(Material material)
    //{
    //    Material[] oldMats = _modelRenderer.materials;
    //    Material[] newMats = new Material[oldMats.Length - 1];

    //    for (int i = 0; i < oldMats.Length - 1; i++)
    //    {
    //        if (oldMats[i] == material)
    //        {
    //            i--;
    //            continue;
    //        }
    //        newMats[i] = oldMats[i];
    //    }

    //    _modelRenderer.materials = newMats;
    //}

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
