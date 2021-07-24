using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFireProjectile : SpellProjectileBase
{
    private Material material;

    public BlueFireProjectile(Material mat)
    {
        material = mat;
    }

    protected override void OnCollideEntity(Collider other, EntityBase entity)
    {
        entity.Damage(90, caster);

        var effect = new BurningEffect(material);
        effect.Initialize(caster, entity);
        entity.AddEffect(effect);

        Destroy(gameObject);
    }

    protected override void OnCollideEnvironment(Collider other)
    {
        Destroy(gameObject);
    }
}
