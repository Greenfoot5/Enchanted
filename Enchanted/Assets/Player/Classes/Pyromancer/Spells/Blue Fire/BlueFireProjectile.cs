using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFireProjectile : SpellProjectileBase
{
    private Material material;
    private BlueFire data;

    /// <summary>
    /// A BlueFireProjectile unique method to carry data.
    /// </summary>
    /// <param name="data">Spell data, such as balancing.</param>
    public void SetSpellData(BlueFire data) => this.data = data;

    public BlueFireProjectile(Material mat)
    {
        // Material assignment (still fixing this buggy feature)
        material = mat;
    }

    protected override void OnCollideEnemyEntity(Collider other, EntityBase entity)
    {
        // Damage.
        entity.Damage(data.OnHitDamage, caster);

        // Create and add the burning effect to the enemy entity.
        var effect = new BurningEffect(material);
        effect.Initialize(caster, data, entity);
        effect.SetData(data.OnTickDamage);
        entity.AddEffect(effect);

        // Destroy the projectile.
        Destroy(gameObject);
    }

    protected override void OnCollideEnvironment(Collider other)
    {
        // Destroy the projectile.
        Destroy(gameObject);
    }
}
