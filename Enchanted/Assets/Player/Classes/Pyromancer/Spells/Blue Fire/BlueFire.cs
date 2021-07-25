using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Pyromancer/BlueFire")]
public class BlueFire : SpellBase
{
    [Header("Projectile")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private Material material;

    // Read-only public values to reference for the projectile.
    public float Speed => speed;
    public float LifeTime => lifeTime;

    public override void CastSpell(EntityBase caster, Vector3 direction)
    {
        // Normalize the direction, so it has a constant speed no matter what.
        direction.Normalize();

        // Spawn the projectile prefab.
        GameObject projectile = Instantiate(prefab, caster.transform.position, Quaternion.Euler(direction));

        // Get the script of the prefab and set it up. I NEED TO USE SOs FOR PROJECTILES
        var script = projectile.GetComponent<SpellProjectileBase>();
        script.lifeTime = LifeTime;
        script.speed = Speed;
        script.Initialize(caster, direction);
    }
}
