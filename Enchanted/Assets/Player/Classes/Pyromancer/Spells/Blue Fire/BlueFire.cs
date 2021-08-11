using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Pyromancer/BlueFire")]
public class BlueFire : SpellBase, IProjectileData, IEffectData
{
    [Header("Stats")]
    [SerializeField] private float onHitDamage;
    [SerializeField] private float onTickDamage;
    [SerializeField] private int amountOfTicks;

    [Header("Projectile configuration")]
    [Space(20)]
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    [Header("Effect configuration")]
    [SerializeField] private bool canStack;
    [SerializeField] private bool isBeneficial;
    [SerializeField] private EffectTickType tickType;
    [SerializeField] private float tickSize;

    // IProjectileData
    public float Speed => speed;
    public float LifeTime => lifeTime;

    // IEffectData
    public bool CanStack => CanStack;
    public int Stacks => amountOfTicks;
    public bool IsBeneficial => isBeneficial;
    public EffectTickType TickType => tickType;
    public float TickSize => tickSize;

    // Read-only stats
    public float OnHitDamage => onHitDamage;
    public float OnTickDamage => onTickDamage;

    public override void CastSpell(EntityBase caster, Vector3 direction)
    {
        // Normalize the direction, so it has a constant speed no matter what.
        direction.Normalize();
        // Spawn the projectile prefab.
        var projectile = Instantiate(prefab, caster.transform.position + prefab.transform.position,
            Quaternion.Euler(direction));

        // Get the script of the prefab and set it up.
        var script = projectile.GetComponent<BlueFireProjectile>();
        script.Initialize(caster, this, direction);
        script.SetSpellData(this);
    }
}
