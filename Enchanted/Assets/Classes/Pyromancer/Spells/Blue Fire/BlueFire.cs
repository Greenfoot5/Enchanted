using UnityEngine;

public class BlueFire : SpellBase
{
    [Header("Projectile")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    public override void CastSpell(Vector3 direction, EntityBase caster)
    {
        GameObject projectile = Instantiate(prefab, _caster.transform.position, Quaternion.Euler(direction));
        projectile.GetComponent<SpellProjectileBase>().Initialize(direction, speed, lifeTime, caster);
    }
}
