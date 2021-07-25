using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFireProjectile : SpellProjectileBase
{
    private Material material;

    public BlueFireProjectile(Material mat)
    {
        // Material assignment (still fixing this buggy feature)
        material = mat;
    }

    protected override void OnCollideEnemyEntity(Collider other, EntityBase entity)
    {
        // Damage 90 (TEMP HARDCODED)
        entity.Damage(90, caster);

        // Create and add the burning effect to the enemy entity.
        var effect = new BurningEffect(material);
        effect.Initialize(caster, entity);
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
