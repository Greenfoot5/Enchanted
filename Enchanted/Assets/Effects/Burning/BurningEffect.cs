using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The "on fire" effect.
/// </summary>
public class BurningEffect : SpellEffectBase
{
    // ALL HARDCODED STATS
    // I WILL USE SOs, BUT I DIDN'T DO IT FOR EFFECTS YET, ONLY SPELLS
    private int _stacks = 3;

    protected override bool CanStack => true;
    protected override int Stacks => _stacks;

    protected override EffectTickType TickType => EffectTickType.Running;
    protected override float TickSize => .5f;

    public override bool NeedsRemoval => _stacks == 0;

    // Burning material (REALLY BUGGY)
    private Material material;

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
        receiver.Damage(10, sender);
        _stacks -= 1;
    }

    public override void OnEnd()
    {
        //receiver.RemoveMaterial(material);
    }
}
