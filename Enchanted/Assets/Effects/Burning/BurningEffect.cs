using UnityEngine;

/// <summary>
/// The "on fire" effect.
/// </summary>
public class BurningEffect : SpellEffectBase
{
    public override bool NeedsRemoval => stacks == 0;

    // Stats
    private float damagePerTick;

    // Burning material (REALLY BUGGY)
    private Material material;

    /// <summary>
    /// Effect-custom method to set a specific amount of damage per tick.
    /// </summary>
    /// <param name="damage">Amount of damage per tick</param>
    public void SetData(float damagePerTick)
    {
        this.damagePerTick = damagePerTick;
    }

    public BurningEffect(Material mat)
    {
        material = mat;
    }

    public override void OnBegin()
    {
        //receiver.AddMaterial(material);
    }

    /// <summary>
    /// Deal damage every tick.
    /// </summary>
    public override void OnTick()
    {
        receiver.Damage(damagePerTick, sender);
        stacks -= 1;
    }

    public override void OnEnd()
    {
        //receiver.RemoveMaterial(material);
    }
}
