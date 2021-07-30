using UnityEngine;

/// <summary>
/// <para>The class containing casting code and level stats.</para>
/// The code for the inherited class <b>must</b> implement the function <see cref="CastSpell">CastSpell</see>.<br/>
/// This class also is responsible for drawing the targeting, aka the hit boxes before casting the spell.
/// </summary>
public abstract class SpellBase : ScriptableObject
{
    // TODO: Cooldown support
    [Header("Basic stats")]
    [SerializeField] private float cooldown;

    public float Cooldown => cooldown;

    /// <summary>
    /// <para>Starts the rendering of the targeting.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    protected virtual void BeginAim()
    {

    }

    /// <summary>
    /// <para>Ends the rendering of the targeting.</para>
    /// <b>Warning: unimplemented.</b>
    /// </summary>
    protected virtual void EndAim()
    {

    }

    /// <summary>
    /// Casts the spell in a certain direction.
    /// </summary>
    /// <param name="caster">The caster of the spell. Used to track damage routes.</param>
    /// <param name="direction">The direction of the cast, is not normalized for non-directional spells.</param>
    public abstract void CastSpell(EntityBase caster, Vector3 direction);
}

