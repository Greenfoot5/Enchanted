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

        // Destroy the projectile.
        Destroy(gameObject);
    }

    protected override void OnCollideEnvironment(Collider other)
    {
        // Destroy the projectile.
        Destroy(gameObject);
    }
}
