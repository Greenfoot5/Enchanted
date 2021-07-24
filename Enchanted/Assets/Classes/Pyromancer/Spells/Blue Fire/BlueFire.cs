using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Pyromancer/BlueFire")]
public class BlueFire : SpellBase
{
    [Header("Projectile")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private Material material;

    public override void CastSpell(Vector3 direction, EntityBase caster)
    {
        GameObject projectile = Instantiate(prefab, _caster.transform.TransformPoint(Vector3.zero), Quaternion.Euler(direction));
        projectile.GetComponent<SpellProjectileBase>().Initialize(direction, speed, lifeTime, caster);
    }
}
