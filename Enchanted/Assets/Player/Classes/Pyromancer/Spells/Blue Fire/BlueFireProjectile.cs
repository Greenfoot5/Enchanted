using UnityEngine;

public class BlueFireProjectile : SpellProjectileBase
{
    private Material _material;
    private BlueFire _data;

    /// <summary>
    /// A BlueFireProjectile unique method to carry data.
    /// </summary>
    /// <param name="data">Spell data, such as balancing.</param>
    public void SetSpellData(BlueFire data) => _data = data;

    public ParticleSystem ps;
    public GameObject hitEffect;

    public BlueFireProjectile(Material mat)
    {
        // Material assignment (still fixing this buggy feature)
        _material = mat;
    }

    protected override void OnCollideEnemyEntity(Collider other, EntityBase entity)
    {
        // Damage.
        entity.Damage(_data.OnHitDamage, caster);

        // Create and add the burning effect to the enemy entity.
        var effect = new BurningEffect(_material);
        effect.Initialize(caster, _data, entity);
        effect.SetData(_data.OnTickDamage);
        entity.AddEffect(effect);
        
        // TODO - Stop the projectile from hitting later objects
        // Destroy the projectile
        ps.Stop();
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject, 2f);
    }

    protected override void OnCollideEnvironment(Collider other)
    {
        ps.Stop();
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
        // Destroy the projectile.
        Destroy(gameObject, 2f);
    }

    /// <summary>
    /// <para>Used to clean up the projectile when it's lifetime reached 0</para>
    /// Stops the particles and destroys the object after the particles finish
    /// </summary>
    protected override void OnExpired()
    {
        ps.Stop();
        Destroy(gameObject, 2f);
    }
}
