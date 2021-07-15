using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFireProjectile : SpellProjectileBase
{
    protected override void OnCollideEntity(Collider other, EntityBase entity)
    {
        entity.Damage(90, caster);
        Destroy(gameObject);
    }

    protected override void OnCollideEnvironment(Collider other)
    {
        Destroy(gameObject);
    }
}
