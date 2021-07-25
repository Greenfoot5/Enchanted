using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>The entity base.</para>
/// Responsible for treating the stats, effects, materials, etc. of the entity.
/// </summary>
public abstract class EntityBase : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float _maxHealth;
    private float _health;

    // Read-only
    public float MaxHealth => _maxHealth;
    public float Health => _health;

    protected SpellEffectManager _spellEffectManager;

    protected Renderer _modelRenderer;

    /// <summary>
    /// Assigns the spell effects and model renderer for later use.
    /// </summary>
    public virtual void Start()
    {
        _spellEffectManager = gameObject.AddComponent<SpellEffectManager>();
        _modelRenderer = GetComponentInChildren<Renderer>();
    }

    /// <summary>
    /// Adds a new spell effect onto the entity.
    /// </summary>
    /// <param name="effect">The spell effect instance.</param>
    public virtual void AddEffect(SpellEffectBase effect) => _spellEffectManager.AddEffect(effect);

    //
    // CHANGING MATERIALS MID-RUNTIME IS REALLY WANKY AND WEIRD
    // TODO: FIND A BETTER METHOD
    /*

        public virtual void AddMaterial(Material material)
        {
            Material[] oldMats = _modelRenderer.materials;
            int matCount = oldMats.Length;
            Material[] newMats = new Material[matCount + 1];

            for (int i = 0; i < matCount - 1; i++)
                newMats[i] = oldMats[i];

            //newMats[matCount] = material;
            _modelRenderer.materials = newMats;
        }

        public virtual void RemoveMaterial(Material material)
        {
            Material[] oldMats = _modelRenderer.materials;
            Material[] newMats = new Material[oldMats.Length - 1];

            for (int i = 0; i < oldMats.Length - 1; i++)
            {
                if (oldMats[i] == material)
                {
                    i--;
                    continue;
                }
                newMats[i] = oldMats[i];
            }

            _modelRenderer.materials = newMats;
        }

    */

    /// <summary>
    /// Heals the entity for a certain amount.
    /// </summary>
    /// <param name="amount">Amount healed.</param>
    public virtual void Heal(float amount)
    {
        // TODO: track who healed and display in numbers.
        _health += amount;
    }

    /// <summary>
    /// Damages the entity for a certain amount.
    /// </summary>
    /// <param name="amount">The amount of damage dealt.</param>
    /// <param name="caster">The source of the damage.</param>
    public virtual void Damage(float amount, EntityBase caster)
    {
        _health -= amount;
        
        // Shows the damage number of the damaged entity for the player.
        if (caster is PlayerBase)
            caster.DamageFeedback(amount, transform.position);

        // Shows the damage number of the damager player for themselves.
        if (this is PlayerBase)
            DamageFeedback(amount);
    }

    /// <summary>
    /// Creates the damage number to give visual feedback to the player.
    /// </summary>
    /// <param name="amount">The damage value.</param>
    /// <param name="position">The world position of the damage. Can be null if the damage is on the player itself.</param>
    public virtual void DamageFeedback(float amount, Vector3? position = null)
    {
        AssetManager.Instance.InstantiateDamageNumber(
            amount,
            position.GetValueOrDefault(transform.position)
        );
    }
}
